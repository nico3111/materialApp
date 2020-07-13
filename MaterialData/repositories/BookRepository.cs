using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class BookRepository : BaseRepository<book>, IMaterialRepository
    {
        public BookRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.book
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void IsValid(book item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.title))
                errList.Add("𝗧𝗶𝘁𝗲𝗹");

            if (string.IsNullOrEmpty(item.isbn))
                errList.Add("𝗜𝗦𝗕𝗡");

            if (item.quantity == null)
                errList.Add("𝗔𝗻𝘇𝗮𝗵𝗹");

            var existingBook = Entities.Set<book>().FirstOrDefault(x => x.title == item.title && x.isbn == item.isbn);
            if (existingBook != null && existingBook.id != item.id)
            {
                existingBook.quantity += item.quantity;
                Entities.book.Update(existingBook);
                Entities.SaveChanges();
                throw new DuplicateEntryException($"Buch {existingBook.title} bereits vorhanden, {item.quantity} Stück wurden hinzugefügt.");
            }

            var existingIsbn = Entities.Set<book>().FirstOrDefault(x => x.title != item.title && x.isbn == item.isbn);
            if (existingIsbn != null && existingIsbn.id != item.id)
                throw new DuplicateEntryException($"Buch mit selben ISBN unter dem Titel \"{existingIsbn.title}\" bereits vorhanden!");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }
        }

        public override book SetLocation(book item)
        {
            if (item.location_id != null)
                item = ReturnBook(item);

            if (item.person_id != null)
                item = RentBook(item);

            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;

            return item;
        }

        private book ReturnBook(book book)
        {
            book existingBook = Entities.book.FirstOrDefault(x => x.isbn == book.isbn && x.location_id == book.location_id);
            if (existingBook != null)
            {
                existingBook.quantity++;
                Entities.book.Remove(book);
                Entities.SaveChanges();
            }
            return existingBook;
        }

        private book RentBook(book book)
        {
            book existingBook = Entities.book.FirstOrDefault(x => x.id == book.id);

            book.quantity = 1;
            book.id = 0;
            book.location_id = null;

            Entities.book.Add(book);
            Entities.SaveChanges();

            existingBook.quantity--;
            return existingBook;
        }
    }
}