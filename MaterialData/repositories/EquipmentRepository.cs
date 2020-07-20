using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        

        /*public override equipment SetLocation(equipment item)
        {
            bool done = false;
            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;

            done = AddIfExisting(item);
            if (done)
                item = null;

            if (!done)
            {
                RebookItem(item);
            }

            return item;
        }

        private equipment RebookItem(equipment item)
        {
           var existingItem = Entities.Set<equipment>().FirstOrDefault(x => x.id == item.id);

            if (existingItem != null)
            {
                existingItem.quantity -= item.quantity;

                if (existingItem.quantity <= 0)
                    Entities.equipment.Remove(existingItem);
                else
                    Entities.equipment.Update(existingItem);

                item.id = 0;
                Entities.equipment.Add(item);
                Entities.SaveChanges();
                return null;
            }
            return item;
        }

        private bool AddIfExisting(equipment item)
        {
            var existingEquipment = Entities.Set<equipment>().FirstOrDefault(x => x.type == item.type && x.make == item.make && x.model == item.model);
            var sameItemInb = Entities.Set<equipment>().FirstOrDefault(x => x.id == item.id);
            if (existingEquipment != null && existingEquipment.id != item.id)
            {
                existingEquipment.quantity += item.quantity;
                sameItemInb.quantity -= item.quantity;
                Entities.equipment.Update(existingEquipment);
                if (sameItemInb.quantity <= 0)
                    Entities.equipment.Remove(sameItemInb);
                else
                    Entities.equipment.Update(sameItemInb);

                Entities.SaveChanges();
                return true;
            }
            return false;
        }*/
    }
}