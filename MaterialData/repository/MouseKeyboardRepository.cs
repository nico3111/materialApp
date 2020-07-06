using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class MouseKeyboardRepository : IMaterialRepository<mouseKeyboard>
    {
        DcvEntities entities = new DcvEntities();

        public void GetRelation()
        {
            entities.mouseKeyboard
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
        public void Delete(mouseKeyboard mouseKeyboard)
        {
            entities.mouseKeyboard.Remove(mouseKeyboard);
            entities.mouseKeyboard.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public IEnumerable<mouseKeyboard> GetAll()
        {
            GetRelation();
            return entities.mouseKeyboard.ToList();
        }

        public mouseKeyboard GetAny(int id)
        {
            GetRelation();
            var mouseKeyboard = entities.mouseKeyboard.FirstOrDefault(x => x.id == id);

            return mouseKeyboard;
        }

       
        public void Save(mouseKeyboard mouseKeyboard)
        {
            entities.mouseKeyboard.Add(mouseKeyboard);
            entities.SaveChanges();
        }

        public void Update(mouseKeyboard mouseKeyboard)
        {
            var existingmouseKeyboard = entities.mouseKeyboard.FirstOrDefault(x => x.id == mouseKeyboard.id);

            if (existingmouseKeyboard != null)
            {
                existingmouseKeyboard.make = mouseKeyboard.make;
                existingmouseKeyboard.model = mouseKeyboard.model;
                existingmouseKeyboard.location_id = mouseKeyboard.location_id;
                existingmouseKeyboard.quantity = mouseKeyboard.quantity;

                entities.SaveChanges();
            }
        }
    }
}
