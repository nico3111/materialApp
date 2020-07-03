using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    public class DisplayRepository : IMaterialRepository<display>
    {
        DcvEntities entities = new DcvEntities();

        public void getRelation()
        {           
            entities.display
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();                        
        }

        public void Delete(display display)
        {
            entities.display.Remove(display);
            entities.display.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public IEnumerable<display> GetAll()
        {
            getRelation();
            return entities.display.ToList();
        }

        public display GetAny(int id)
        {
            getRelation();
            //var per = entities.person.ToList();
            var display = entities.display.FirstOrDefault(x => x.id == id);

            return display;
        }

        public void Save(display display)
        {
            entities.display.Add(display);
            entities.SaveChanges();
        }

        public void Update(display display)
        {
            var existingDisplay = entities.display.FirstOrDefault(x => x.id == display.id);

            if (existingDisplay != null)
            {
                existingDisplay.serial_number = display.serial_number;
                existingDisplay.make = display.make;
                existingDisplay.model = display.model;
                existingDisplay.location_id = display.location_id;

                entities.SaveChanges();
            }
        }
    }
}
