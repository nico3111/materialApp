using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MaterialData
{
    public class NotebookRepository : IMaterialRepository<notebook>
    {
        
        
        DcvEntities entities = new DcvEntities();

        public void getNote()
        {
            entities.notebook.Include(x => x.people).ToList();

            /*
            entities.person.Include(x => x.notebook)
                .ThenInclude(x => x.people).ToList();*/
        }
        public IEnumerable<notebook> GetAll()
        {
            getNote();
            var x = entities.notebook.ToList();
            return x;
        }

        public notebook GetAny(int id)
        {
            getNote();
            var per = entities.person.ToList();
            var notebook = entities.notebook.FirstOrDefault(x => x.id == id);

            return notebook;
        }

        public HttpStatusCode Save(notebook notebook)
        {
            using (var materialEntities = new DcvEntities())
            {
                materialEntities.notebook.Add(notebook);
                materialEntities.SaveChanges();
            }
            return HttpStatusCode.Created;
        }

        public HttpStatusCode Delete(notebook notebook)
        {
            entities.notebook.Remove(notebook);
            entities.notebook.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();

            return HttpStatusCode.OK;
        }

        public HttpStatusCode Update(notebook notebook)
        {
            var existingNotebook = entities.notebook.FirstOrDefault(x => x.id == notebook.id);

            if (existingNotebook != null)
            {
                existingNotebook.serial_number = notebook.serial_number;
                existingNotebook.make = notebook.make;
                existingNotebook.model = notebook.model;
                //existingNotebook.person = notebook.person;
                existingNotebook.person_id = notebook.person_id;
                existingNotebook.location_id = notebook.location_id;

                entities.SaveChanges();

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }
    }
}
