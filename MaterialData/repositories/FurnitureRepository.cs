using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
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
            string err = "Bitte mindestens ";
            if (string.IsNullOrEmpty(item.type))
                err += "Art ";
            if (item.quantity.Equals(null))
                err += "Anzahl ";
            if (item.location_id.Equals(null))
                err += "Standort ";

            err += "angeben!";
            throw new InvalidInputException(err);
        }
    }
}