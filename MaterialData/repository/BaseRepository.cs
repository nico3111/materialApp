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

        public abstract void Update(int id, T t);        
    }
}
