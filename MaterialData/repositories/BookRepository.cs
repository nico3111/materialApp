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

            var existingBook = Entities.Set<book>().FirstOrDefault(x => x.title == item.title && x.isbn == item.isbn);
            if (existingBook != null)
                throw new DuplicateEntryException("Buch bereits vorhanden!");

            var existingIsbn = Entities.Set<book>().FirstOrDefault(x => x.title != item.title && x.isbn == item.isbn);
            if (existingIsbn != null)
                throw new DuplicateEntryException("Buch mit selben ISBN und anderem Titel bereits vorhanden!");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }
        }

        public override book SetDefaultLocation(book item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;
            return item;
        }
    }
}