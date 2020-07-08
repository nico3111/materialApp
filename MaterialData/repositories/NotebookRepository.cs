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
                errList.Add("Marke ");
            if (string.IsNullOrEmpty(item.model))
                errList.Add("Modell ");
            if (string.IsNullOrEmpty(item.serial_number))
                errList.Add("Seriennummer ");

            string err = "Bitte ";
            for (int i = 0; i < errList.Count; i++)
            {
                err += $"{errList[i]} ";
            }
            {
            }
            err += "angeben!";
            throw new InvalidInputException(err);
        }
    }
}