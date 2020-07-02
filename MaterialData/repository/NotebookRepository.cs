﻿using MaterialData.models;
using MaterialData.repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData
{
    public class NotebookRepository : IMaterialRepository<notebook>
    {
        
        
        DcvEntities entities = new DcvEntities();

        public void getNote()
        {

            //notebook => person
            //notebook => classroom
            //entities.addressLocation.Include(x => x.classroom).Include(x => x.address).ToList();

            entities.notebook
                .Include(x => x.person)
                .Include(x => x.classroom)
                .ThenInclude(x => x.addressloc)
                .ThenInclude(x => x.address)
                .ToList();
            //entities.notebook.Include(x => x.classroom).ToList();
            

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

        public void Save(notebook notebook)
        {
           /* using (var materialEntities = new DcvEntities())
            {*/
                entities.notebook.Add(notebook);
                entities.SaveChanges();
            /*}*/
        }

        public void Delete(notebook notebook)
        {
            entities.notebook.Remove(notebook);
            entities.notebook.FromSqlRaw("ALTER TABLE notebook AUTO_INCREMENT = 1;");
            entities.SaveChanges();
        }

        public void Update(notebook notebook)
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
            }
        }
    }
}
