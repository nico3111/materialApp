using MaterialData.models;
using MaterialData.repository;
using System.Collections.Generic;

namespace MaterialLogic
{
    public abstract class BaseLogic<T> where T : Material
    {
        public BaseRepository<T> Repo;

        public BaseLogic(BaseRepository<T> baseRepository)
        {
            Repo = baseRepository;
        }

        public abstract void IsValid(T item);

        public abstract T SetLocation(T item);

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
    }
}