using System.Collections.Generic;

namespace MaterialData.repository
{
    public interface IMaterialRepository<T>
    {
        void GetRelation();
        IEnumerable<T> GetAll();
        void Save(T t);
        T GetAny(int id);
        void Delete(T t);
        void Update(T t);
    }
}
