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
                errList.Add("-Marke-");

            if (string.IsNullOrEmpty(item.model))
                errList.Add("-Modell-");

            if (string.IsNullOrEmpty(item.serial_number))
                errList.Add("-Seriennummer-");

            var existingItem = Entities.Set<notebook>().FirstOrDefault(x => x.serial_number == item.serial_number);
            if (existingItem != null)
            {
                throw new DuplicateEntryException("Seriennummer in DB bereits vorhanden!");
            }

            if (errList.Count > 0)
            {
                string err = "Bitte mindestens\n";
                foreach (string s in errList)
                {
                    err += $"{s}\n";
                }
                err += "angeben!";

                throw new InvalidInputException(err);
            }
        }
    }
}