using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData.repository
{
    public class EquipmentRepository : BaseRepository<equipment>, IMaterialRepository
    {
        public EquipmentRepository(DcvEntities entities) : base(entities)
        {
        }       
        public override void GetRelation()
        {
            Entities.equipment
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void Update(int id, equipment equipment)
        {
            var existingItem = Entities.Find<equipment>(id);

            if (existingItem != null)
            {
                existingItem.type = equipment.type;
                existingItem.make = equipment.make;
                existingItem.model = equipment.model;
                existingItem.person_id = equipment.person_id;
                existingItem.location_id = equipment.location_id;
                existingItem.quantity = equipment.quantity;

                Entities.SaveChanges();
            }
        }
    }
}
