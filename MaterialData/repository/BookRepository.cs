using MaterialData.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public class BookRepository : IMaterialRepository<book>
    {
        DcvEntities entities = new DcvEntities();
        public void getRelation()
        {
            entities.book
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
        }
        public void Delete(book t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<book> GetAll()
        {
            getRelation();
            return entities.book.ToList();
        }

        public book GetAny(int id)
        {
            getRelation();
            var book = entities.book.FirstOrDefault(x => x.id == id);

            return book;
        }

        public void Save(book book)
        {
            entities.book.Add(book);
            entities.SaveChanges();
        }

        public void Update(book book)
        {
            var existingBook = entities.book.FirstOrDefault(x => x.id == book.id);

            if (existingBook != null)
            {
                existingBook.isbn = book.isbn;
                existingBook.title = book.title;
                existingBook.person_id = book.person_id;
                existingBook.location_id = book.location_id;

                entities.SaveChanges();
            }
        }
    }
}
