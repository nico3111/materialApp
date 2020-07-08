using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
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
            string err = "Bitte mindestens ";
            if (string.IsNullOrEmpty(item.make))
                err += "Marke ";
            if (string.IsNullOrEmpty(item.model))
                err += "Modell ";
            if (item.location_id.Equals(null))
                err += "Standort ";

            err += "angeben!";
            throw new InvalidInputException(err);
        }
    }
}