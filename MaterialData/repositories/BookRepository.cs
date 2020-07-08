using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
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
            string err = "Bitte mindestens ";
            if (string.IsNullOrEmpty(item.title))
                err += "Titel ";
            if (string.IsNullOrEmpty(item.isbn))
                err += "ISBN ";

            err += "angeben!";
            throw new InvalidInputException(err);
        }
    }
}