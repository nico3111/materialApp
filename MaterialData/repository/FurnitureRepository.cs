using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData.repository
{
    public class FurnitureRepository : BaseRepository<book>, IMaterialRepository
    {
        public FurnitureRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.notebook
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
    }
}