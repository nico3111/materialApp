using MaterialData.exceptions;
using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.title))
                errList.Add("-Titel-");

            if (string.IsNullOrEmpty(item.isbn))
                errList.Add("-ISBN-");

            if (errList.Count > 0)
            {
                string err = "Bitte mindestens\n";
                foreach (string s in errList)
                {
                    err += $"{s}\n";
                }
                err += "angeben!";

                throw new InvalidInputException(err);
            }
        }

        /// <summary>
        /// Sets the location to storage if not entered
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override book SetDefaultLocation(book item)
        {
            if (item.location_id == null && item.person_id == null)
                item.location_id = 4;
            return item;
        }
    }
}