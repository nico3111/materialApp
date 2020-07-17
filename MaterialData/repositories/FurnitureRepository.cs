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
            Entities.furniture
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

            if (item.quantity < 1)
                throw new InvalidInputException("Anzahl darf nicht kleiner als 1 sein!");
        }

        public override furniture SetLocation(furniture item)
        {
            if (item.location_id == null)
                item.location_id = defaultLocation;

            /*item = RebookItem(item);
            if (item != null)*/
            bool isAdded = AddIfExisting(item);
            if (!isAdded)
                RebookItem(item);

            return item;
        }

        private bool AddIfExisting(furniture item)
        {
            furniture existingFurniture = Entities.Set<furniture>().FirstOrDefault(x => x.type == item.type && x.location_id == item.location_id);
            furniture sameFurnitureInDb = Entities.Set<furniture>().FirstOrDefault(x => x.id == item.id);
            if (existingFurniture != null)
            {
                GetRelation();
                existingFurniture.quantity += item.quantity;

                if (sameFurnitureInDb != null)
                {
                    sameFurnitureInDb.quantity -= item.quantity;
                    if (sameFurnitureInDb.quantity <= 0)
                        Entities.furniture.Remove(sameFurnitureInDb);
                    else
                        Entities.furniture.Update(sameFurnitureInDb);
                }

                Entities.furniture.Update(existingFurniture);
                //furniture sameFurnitureInDb = Entities.Set<furniture>().FirstOrDefault(x => x.id == item.id);

                //Entities.furniture.Remove(sameFurnitureInDb);

                Entities.SaveChanges();
                throw new NotAddedButUpdatedException($"{item.type} wurde bestehendem Bestand in \n\"{existingFurniture.classroom.addressloc.classroom.room}, " +
                                                      $"{existingFurniture.classroom.addressloc.address.street}, {existingFurniture.classroom.addressloc.address.place}\"\n hinzugefügt!");
            }
            return false;
            /* else
             {
                 furniture sameFurnitureInDb = Entities.Set<furniture>().FirstOrDefault(x => x.id == item.id);
                 sameFurnitureInDb.quantity -= item.quantity;
                 if (sameFurnitureInDb.quantity <= 0)
                     Entities.furniture.Remove(sameFurnitureInDb);
                 else
                     Entities.furniture.Update(sameFurnitureInDb);

                 item.id = 0;
                 Entities.furniture.Add(item);
                 Entities.SaveChanges();
                 return null;
             }*/
            //return item;
        }

        private furniture RebookItem(furniture item)
        {
            var existingFurniture = Entities.Set<furniture>().FirstOrDefault(x => x.id == item.id);
            if (existingFurniture != null)
            {
                existingFurniture.quantity -= item.quantity;

                if (existingFurniture.quantity <= 0)
                    Entities.furniture.Remove(existingFurniture);
                else
                    Entities.furniture.Update(existingFurniture);

                item.id = 0;
                Entities.furniture.Add(item);
                Entities.SaveChanges();
                return null;
            }
            return item;
        }
    }
}