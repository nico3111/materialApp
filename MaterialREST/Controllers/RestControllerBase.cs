using MaterialData;
using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MaterialREST.Controllers
{
    public class RestControllerBase<T> : ControllerBase where T : class
    {
        public DcvEntities Entities { get; set; }

        public BaseRepository<T> Repo { get; set; }

        public RestControllerBase()
        {
            Entities = new DcvEntities(Properties.Resources.ResourceManager.GetString("ProductionConnection"));
            Repo = GetRepository<T>() as BaseRepository<T>;
        }

        private IMaterialRepository GetRepository<T>()  
        {
            if (typeof(T) == typeof(book))
                return new BookRepository(Entities);

            if (typeof(T) == typeof(notebook))
                return new NotebookRepository(Entities);

            return null;
        }

        [HttpGet]
        public List<T> Get()
        
        {
            List<T> T = null;
            try
            {
                T = (List<T>)Repo.GetAll();
                return T;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                Response.StatusCode = 500;
            }
            return T;
        }

        [HttpGet("{id}")]
        public T Get(int id)
        {
            T t = null;
            try
            {
                t = Repo.GetAny(id);
                return t;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                Response.StatusCode = 500;
            }
            return t;
        }

        [HttpPost]
        public void Post([FromBody] T t)
        {
            try
            {
                Repo.Save(t);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] T t)
        {
            try
            {
                //Repo.Update(t);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                Repo.Delete(id);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
