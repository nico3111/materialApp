using MaterialData.models;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public abstract class BaseRepository<T> where T : class
    {
        public DcvEntities Entities;
        public BaseRepository(DcvEntities entities)
        {
            Entities = entities;
        }

        public abstract void GetRelation();
        public void Delete(int id)
        {
            var t = Entities.Find<T>(id);
            Entities.Remove(t);
            Entities.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            GetRelation();
            return Entities.Set<T>().ToList();
        }

        public T GetAny(int id)         
        {
            GetRelation();
            var item = Entities.Find<T>(id);
            return item;            
        }

        public void Save(T t)
        {
            Entities.Add(t);
            Entities.SaveChanges();
        }

        /*public void Update(T t)
        {
            var existingItem = Entites.FirstOrDefault(x => x.id == t.id);

            if (existingItem != null)
            {
                existingItem.isbn = t.isbn;
                existingItem.title = t.title;
                existingItem.person_id = t.person_id;
                existingItem.location_id = t.location_id;

                entities.SaveChanges();
            }
        }*/
    }
}
