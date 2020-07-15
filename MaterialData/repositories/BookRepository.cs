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

            //AddIfExisting(item);

            var existingIsbn = Entities.Set<book>().FirstOrDefault(x => x.title != item.title && x.isbn == item.isbn);
            if (existingIsbn != null && existingIsbn.id != item.id)
                throw new DuplicateEntryException($"Buch mit selben ISBN unter dem Titel \"{existingIsbn.title}\" bereits vorhanden!");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }

            if (item.quantity < 1)
                throw new InvalidInputException("Anzahl darf nicht kleiner als 1 sein!");
        }

        /*private void AddIfExisting(book item)
        {
            var existingBook = Entities.Set<book>().FirstOrDefault(x => x.title == item.title && x.isbn == item.isbn);
            if (existingBook != null && existingBook.id != item.id)
            {
                existingBook.quantity += item.quantity;
                Entities.book.Update(existingBook);
                Entities.SaveChanges();
                throw new NotAddedButUpdatedException($"Buch {existingBook.title} bereits vorhanden, {item.quantity} Stück hinzugefügt.");
            }
        }*/

        public override book SetLocation(book item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;

            if (item.location_id != null && item.person_id != null)
                throw new DuplicateEntryException("Bitte Buch einer Person ODER einem Standort zuweisen!");

            if (item.location_id != null && item.id > 0)
                return ReturnBook(item);

            if (item.person_id != null)
            {
                item = RentBook(item);
                return item;
            }

            if (item.id == 0)
            {
                var existingBook = Entities.Set<book>().FirstOrDefault(x => x.title == item.title && x.isbn == item.isbn);
                if (existingBook != null && existingBook.id != item.id && existingBook.location_id == item.location_id)
                {
                    existingBook.quantity += item.quantity;
                    Entities.book.Update(existingBook);
                    Entities.SaveChanges();
                    return null;
                }
            }

            return item;
        }

        private book ReturnBook(book book)
        {
            book existingBook = Entities.book.FirstOrDefault(x => x.isbn == book.isbn && x.title == book.title);
            book sameBookInDb = Entities.book.FirstOrDefault(x => x.id == book.id);

            if (existingBook != null && existingBook.id == book.id)
                return book;

            if (sameBookInDb.quantity < book.quantity)
                throw new InvalidInputException("Es können nicht mehr Bücher zurückgegeben werden als ausgeliehen wurden!");

            if (existingBook != null && existingBook.location_id == book.location_id)
            {
                existingBook.quantity += book.quantity;
                Entities.book.Remove(sameBookInDb);
                Entities.SaveChanges();
            }
            else
            {
                book newBook = new book { id = 0, title = book.title, isbn = book.isbn, quantity = book.quantity, location_id = book.location_id }; //maybe now redundant
                Entities.book.Remove(sameBookInDb);
                Entities.book.Add(newBook);
                Entities.SaveChanges();
                return null;
            }

            return existingBook;
        }

        private book RentBook(book book)
        {
            book existingBook = Entities.book.FirstOrDefault(x => x.id == book.id);
            book alreadyBorrowedBook = Entities.book.FirstOrDefault(x => x.person_id == book.person_id);

            if (alreadyBorrowedBook != null)
                throw new InvalidInputException($"Buch \"{book.title}\" wurde bereits an diese Person verliehen!");

            if (existingBook != null)
            {
                if (existingBook.quantity < book.quantity)
                    throw new InvalidInputException($"Die Anzahl ausgeliehender Bücher darf nicht den Lagerbestand überschreiten!");
                else
                {
                    book.quantity = 1;
                    book.id = 0;
                    book.location_id = null;

                    Entities.book.Add(book);

                    existingBook.quantity -= book.quantity;
                }

                if (existingBook.quantity <= 0)
                {
                    Entities.book.Remove(existingBook);
                    existingBook = null;
                }

                Entities.SaveChanges();

                return existingBook;
            }
            else
                return book;
        }
    }
}