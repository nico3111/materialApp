using MaterialData.models;
using MaterialData.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData
{
    public class NotebookRepository : IMaterialRepository<notebook>
    {

        MaterialEntities entities = new MaterialEntities();
        public IEnumerable<notebook> GetAll()
        {
            return entities.notebook.ToList();
        }

        public notebook GetAny(int id)
        {
            return entities.notebook.FirstOrDefault(x => x.id == id);
        }

        public void Save(notebook notebook)
        {
            using (var materialEntities = new MaterialEntities())
            {
                materialEntities.notebook.Add(notebook);
                materialEntities.SaveChanges();
            }
        }

        public void Delete(notebook notebook)
        {
            entities.notebook.Remove(notebook);
            entities.notebook.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public void Update(notebook notebook)
        {
            var existingNotebook = entities.notebook.FirstOrDefault(x => x.id == notebook.id);

            if (existingNotebook != null)
            {
                existingNotebook.serial_number = notebook.serial_number;
                existingNotebook.make = notebook.make;
                existingNotebook.model = notebook.model;
                existingNotebook.person_id = notebook.person_id;
                existingNotebook.location_id = notebook.location_id;

                entities.SaveChanges();
            }
        }
    }
}
