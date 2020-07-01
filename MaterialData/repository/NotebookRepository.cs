using MaterialData.models;
using MaterialData.repository;
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
            entities.SaveChanges();
        }
    }
}
