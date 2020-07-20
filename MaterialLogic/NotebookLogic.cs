using MaterialData.exceptions;
using MaterialData.interfaces;
using MaterialData.models;
using MaterialData.repository;
using System.Collections.Generic;
using System.Linq;

namespace MaterialLogic
{
    public class NotebookLogic : BaseLogic<notebook>, IMaterialLogic
    {
        public NotebookLogic(BaseRepository<notebook> Repo) : base(Repo)
        {
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

            var existingItem = Repo.Entities.Set<notebook>().FirstOrDefault(x => x.serial_number == item.serial_number);
            if (existingItem != null && item.id != existingItem.id)
                throw new DuplicateEntryException($"Seriennummer \"{item.serial_number}\" in Datenbank bereits vorhanden! \n({existingItem.make} {existingItem.model})");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }
        }

        public override notebook SetLocation(notebook item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = Repo.defaultLocation;

            if (item.location_id != null && item.person_id != null)
                throw new DuplicateEntryException("Bitte Notebook einer Person ODER einem Standort zuweisen!");

            return item;
        }
    }
}