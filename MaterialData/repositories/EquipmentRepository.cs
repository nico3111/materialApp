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

        public override void IsValid(equipment item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.type))
                errList.Add("𝗔𝗿𝘁");

            if (item.quantity == null)
                errList.Add("𝗔𝗻𝘇𝗮𝗵𝗹");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }
            AddIfExisting(item);
        }

        private void AddIfExisting(equipment item)
        {
            var existingEquipment = Entities.Set<equipment>().FirstOrDefault(x => x.type == item.type && x.make == item.make && x.model == item.model);
            if (existingEquipment != null && existingEquipment.id != item.id)
            {
                existingEquipment.quantity += item.quantity;
                Entities.equipment.Update(existingEquipment);
                Entities.SaveChanges();
                throw new NotAddedButUpdatedException($"{existingEquipment.type} {existingEquipment.make} {existingEquipment.model} bereits vorhanden, {item.quantity} Stück hinzugefügt.");
            }
        }

        public override equipment SetLocation(equipment item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;
            return item;
        }
    }
}