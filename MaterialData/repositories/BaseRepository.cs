using MaterialData.models;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public abstract class BaseRepository<T> where T : Material
    {
        public DcvEntities Entities;
        public int defaultLocation = 4; //id in DB

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
        public abstract T SetDefaultLocation(T item);

        /// <summary>
        /// Throws Exception if Entry is Invalid
        /// </summary>
        /// <param name="item"></param>
        ///
        public abstract void IsValid(T item);

        /// <summary>
        /// Builds message returned to frontend
        /// </summary>
        /// <param name="errList"></param>
        /// <returns></returns>
        public string BuildErrorMessage(List<string> errList)
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
        }

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

        public void Update(T t)
        {
            Entities.Update(t);
            Entities.SaveChanges();
        }
    }
}