using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    public interface IMaterialRepository<T>
    {
        IEnumerable<T> GetAll();
        void Save(T t);
    }
}
