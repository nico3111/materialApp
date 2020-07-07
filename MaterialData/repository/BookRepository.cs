using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData.repository
{
    public class BookRepository : BaseRepository <book>, IMaterialRepository
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

        public override void Update(int id, book book)
        {
            var existingItem = Entities.Find<book>(id);

            if (existingItem != null)
            {
                existingItem.isbn = book.isbn;
                existingItem.title = book.title;
                existingItem.person_id = book.person_id;
                existingItem.location_id = book.location_id;

                Entities.SaveChanges();
            }
        }        
    }
}
