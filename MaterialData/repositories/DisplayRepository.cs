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
                errList.Add("-Marke-");

            if (string.IsNullOrEmpty(item.model))
                errList.Add("-Modell-");

            if (item.location_id == (null))
                errList.Add("-Standort-");

            if (errList.Count > 0)
            {
                string err = "Bitte mindestens ";
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