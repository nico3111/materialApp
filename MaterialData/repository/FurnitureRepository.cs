using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    public class FurnitureRepository : IMaterialRepository<furniture>
    {
        DcvEntities entities = new DcvEntities();
        public void getRelation()
        {
            entities.notebook
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
        public void Delete(furniture furniture)
        {
            entities.furniture.Remove(furniture);
            entities.furniture.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public IEnumerable<furniture> GetAll()
        {
            getRelation();
            return entities.furniture.ToList();
        }

        public furniture GetAny(int id)
        {
            getRelation();
            var furniture = entities.furniture.FirstOrDefault(x => x.id == id);

            return furniture;
        }

        public void Save(furniture furniture)
        {
            entities.furniture.Add(furniture);
            entities.SaveChanges();
        }

        public void Update(furniture furniture)
        {
            var existingFurniture = entities.furniture.FirstOrDefault(x => x.id == furniture.id);

            if (existingFurniture != null)
            {
                existingFurniture.type = furniture.type;
                existingFurniture.quantity = furniture.quantity;
                existingFurniture.location_id = furniture.location_id;

                entities.SaveChanges();
            }
        }
    }
}
