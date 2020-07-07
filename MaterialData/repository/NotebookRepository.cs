using MaterialData.models;
using MaterialData.repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData
{
    public class NotebookRepository :BaseRepository<notebook>, IMaterialRepository
    {

        public NotebookRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.notebook
                 .Include(x => x.person)
                 .Include(x => x.classroom)
                 .ThenInclude(x => x.addressloc)
                 .ThenInclude(x => x.address)
                 .ToList();
        }

        public override void Update(int id, notebook notebook)
        {
            var existingItem = Entities.Find<notebook>(id);

            if (existingItem != null)
            {
                existingItem.serial_number = notebook.serial_number;
                existingItem.make = notebook.make;
                existingItem.model = notebook.model;
                existingItem.location_id = notebook.location_id;
                existingItem.person_id = notebook.person_id;

                Entities.SaveChanges();
            }
        }
    }
}
