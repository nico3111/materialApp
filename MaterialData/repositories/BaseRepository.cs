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

        /// <summary>
        /// Sets the location to storage if not entered
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //public abstract T SetLocation(T item);

        /// <summary>
        /// Throws Exception if Entry is Invalid
        /// </summary>
        /// <param name="item"></param>
        ///
        //public abstract void IsValid(T item);

        /// <summary>
        /// Builds a errormessage thats returned to frontend
        /// </summary>
        /// <param name="errList"></param>
        /// <returns></returns>
        /*public string BuildErrorMessage(List<string> errList)
        {
            string err = "Bitte mindestens ";
            for (int i = 0; i < errList.Count; i++)
            {
                err += $"{errList[i]}";
                if (i + 2 < errList.Count)
                    err += ", ";
                else if (i + 1 < errList.Count)
                    err += " & ";
            }

            err += " angeben!";
            return err;
        }*/

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