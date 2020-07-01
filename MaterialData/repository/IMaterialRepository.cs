using System.Collections.Generic;

namespace MaterialData.repository
{
    public interface IMaterialRepository<T>
    {
        IEnumerable<T> GetAll();
        void Save(T t);

        T GetAny(int id);
    }
}
