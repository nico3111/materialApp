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
                errList.Add("𝗔𝗿𝘁");

            if (item.quantity == null)
                errList.Add("𝗔𝗻𝘇𝗮𝗵𝗹");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }

            var existingFurniture = Entities.Set<furniture>().FirstOrDefault(x => x.type == item.type);
            if (existingFurniture != null && existingFurniture.id != item.id)
            {
                existingFurniture.quantity += item.quantity;
                Entities.furniture.Update(existingFurniture);
                Entities.SaveChanges();
                throw new NotAddedButUpdatedException($"{existingFurniture.type} bereits vorhanden, {item.quantity} Stück hinzugefügt.");
            }
        }

        public override furniture SetLocation(furniture item)
        {
            if (item.location_id == null)
                item.location_id = defaultLocation;
            return item;
        }
    }
}