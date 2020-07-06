using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class EquipmentRepository : IMaterialRepository<equipment>
    {
        DcvEntities entities = new DcvEntities();

        public void GetRelation()
        {
            entities.equipment
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
        public void Delete(equipment equipment)
        {
            entities.equipment.Remove(equipment);
            entities.equipment.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public IEnumerable<equipment> GetAll()
        {
            GetRelation();
            return entities.equipment.ToList();
        }

        public equipment GetAny(int id)
        {
            GetRelation();
            var equipment = entities.equipment.FirstOrDefault(x => x.id == id);

            return equipment;
        }

       
        public void Save(equipment equipment)
        {
            entities.equipment.Add(equipment);
            entities.SaveChanges();
        }

        public void Update(equipment equipment)
        {
            var existingequipment = entities.equipment.FirstOrDefault(x => x.id == equipment.id);

            if (existingequipment != null)
            {
                existingequipment.type = equipment.type;
                existingequipment.make = equipment.make;
                existingequipment.model = equipment.model;
                existingequipment.person_id = equipment.person_id;
                existingequipment.location_id = equipment.location_id;
                existingequipment.quantity = equipment.quantity;

                entities.SaveChanges();
            }
        }
    }
}
