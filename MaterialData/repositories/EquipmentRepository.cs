using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData.repository
{
    public class EquipmentRepository : BaseRepository<equipment>, IMaterialRepository
    {
        public EquipmentRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.equipment
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void IsValid(equipment item)
        {
            string err = "Bitte mindestens ";
            if (string.IsNullOrEmpty(item.type))
                err += "Art ";
            if (item.quantity.Equals(null))
                err += "Anzahl ";

            err += "angeben!";
            throw new InvalidInputException(err);
        }
    }
}