using MaterialData.models;
using MaterialData.repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData
{
    public class NotebookRepository : BaseRepository<notebook>, IMaterialRepository
    {
        public NotebookRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.notebook
                 .Include(x => x.person)
                 .Include(x => x.classroom)
                 .ThenInclude(x => x.addressloc)
                 .ThenInclude(x => x.address)
                 .ToList();
        }
    }
}