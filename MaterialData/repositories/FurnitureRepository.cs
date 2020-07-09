using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class FurnitureRepository : BaseRepository<furniture>, IMaterialRepository
    {
        public FurnitureRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.notebook
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void IsValid(furniture item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.type))
                errList.Add("-Art-");

            if (item.quantity == null)
                errList.Add("-Anzahl-");

            if (item.location_id == (null))
                errList.Add("-Standort-");

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

        public override furniture SetDefaultLocation(furniture item)
        {
            if (item.location_id == null)
                item.location_id = defaultLocation;
            return item;
        }
    }
}