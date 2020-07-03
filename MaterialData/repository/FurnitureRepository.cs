using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.repository
{
    public class FurnitureRepository : IMaterialRepository<furniture>
    {
        DcvEntities entities = new DcvEntities();
        public void getRelation()
        {
            entities.notebook
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
        public void Delete(furniture t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<furniture> GetAll()
        {
            getRelation();
            return entities.furniture.ToList();
        }

        public furniture GetAny(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(furniture t)
        {
            throw new NotImplementedException();
        }

        public void Update(furniture t)
        {
            throw new NotImplementedException();
        }
    }
}
