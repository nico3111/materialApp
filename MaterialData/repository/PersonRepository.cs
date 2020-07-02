using MaterialData.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    class PersonRepository
    {
        DcvEntities entities = new DcvEntities();

        public List<person> GetAll()
        {
            return entities.person.ToList();
        }
    }
}
