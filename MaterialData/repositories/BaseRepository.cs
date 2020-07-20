using MaterialData.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    public abstract class BaseRepository<T> where T : Material
    {
        public DcvEntities Entities;
        public int defaultLocation = 6; // id in DB

        public BaseRepository(DcvEntities entities)
        {
            Entities = entities;
        }

        public abstract void GetRelation();

        public async Task Delete(int id)
        {
            var t = Entities.Find<T>(id);
            Entities.Remove(t);
            await Entities.SaveChangesAsync();
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

        public async Task Save(T t)
        {
            Entities.Add(t);
            await Entities.SaveChangesAsync();
        }

        public async Task UpdateAsync(T t)
        {
            var e = Entities.Set<T>().FirstOrDefault(x => x.id == t.id);
            Entities.Entry(e).CurrentValues.SetValues(t);
            Entities.Update(e);
            await Entities.SaveChangesAsync();
        }
    }
}