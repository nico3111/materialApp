using MaterialData.exceptions;
using MaterialData.models;
using MaterialData.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData
{
    public class NotebookRepository : BaseRepository<notebook>, IMaterialRepository
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

        public override void IsValid(notebook item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.make))
                errList.Add("𝗠𝗮𝗿𝗸𝗲");

            if (string.IsNullOrEmpty(item.model))
                errList.Add("𝗠𝗼𝗱𝗲𝗹𝗹");

            if (string.IsNullOrEmpty(item.serial_number))
                errList.Add("𝗦𝗲𝗿𝗶𝗲𝗻𝗻𝘂𝗺𝗺𝗲𝗿");

            var existingItem = Entities.Set<notebook>().FirstOrDefault(x => x.serial_number == item.serial_number);
            if (existingItem != null && item.id != existingItem.id)
                throw new DuplcateEntryException($"Seriennummer \"{item.serial_number}\" in Datenbank bereits vorhanden! \n({existingItem.make} {existingItem.model})");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }
        }

        public override notebook SetLocation(notebook item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = defaultLocation;

            if (item.location_id != null && item.person_id != null)
                throw new DuplcateEntryException("Bitte Notebook einer Person ODER einem Standort zuweisen!");

            return item;
        }
    }
}