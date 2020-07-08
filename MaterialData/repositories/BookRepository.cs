using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MaterialData.repository
{
    public class BookRepository : BaseRepository<book>, IMaterialRepository
    {
        public BookRepository(DcvEntities entities) : base(entities)
        {
        }

        public override void GetRelation()
        {
            Entities.book
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }

        public override void IsValid(book item)
        {
            if (string.IsNullOrEmpty(item.isbn))
                throw new Exception("need a isbn!");

            if (string.IsNullOrEmpty(item.title))
                throw new Exception("need a title!");
        }
    }
}