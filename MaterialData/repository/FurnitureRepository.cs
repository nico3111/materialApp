using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MaterialData.repository
{
    public class FurnitureRepository : BaseRepository<furniture>, IMaterialRepository
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

        public override void Update(int id, furniture furniture)
        {
            var existingItem = Entities.Find<furniture>(id);

            if (existingItem != null)
            {
                existingItem.type = furniture.type;
                existingItem.quantity = furniture.quantity;
                existingItem.location_id = furniture.location_id;

                Entities.SaveChanges();
            }
        }
    }
}