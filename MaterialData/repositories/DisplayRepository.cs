using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class DisplayRepository : BaseRepository<display>, IMaterialRepository
    {
        public DisplayRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.display
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void IsValid(display item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.make))
                errList.Add("𝗠𝗮𝗿𝗸𝗲");

            if (string.IsNullOrEmpty(item.model))
                errList.Add("𝗠𝗼𝗱𝗲𝗹𝗹");

            var existingItem = Entities.Set<display>().FirstOrDefault(x => x.serial_number == item.serial_number);
            if (existingItem != null && item.id != existingItem.id)
                throw new DuplcateEntryException($"Seriennummer \"{item.serial_number}\" in Datenbank bereits vorhanden! \n({existingItem.make} {existingItem.model})");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }

            if (item.serial_number != null)
                item.quantity = 1;

            if (item.quantity < 1)
                throw new InvalidInputException("Anzahl darf nicht kleiner als 1 sein!");
        }

        public override display SetLocation(display item)
        {
            if (item.location_id == null)
                item.location_id = defaultLocation;
            return item;
        }
    }
}