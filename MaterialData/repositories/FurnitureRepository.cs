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
            if (string.IsNullOrEmpty(item.type))
                throw new InvalidInputException("Bitte Art angeben, Pappnase!");

            if (item.quantity.Equals(null))
                throw new InvalidInputException("Bitte Menge angeben!");

            if (item.location_id.Equals(null))
                throw new InvalidInputException("Bitte Ort angeben!");
        }
    }
}