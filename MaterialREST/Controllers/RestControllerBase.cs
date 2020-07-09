using MaterialData;
using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MaterialREST.Controllers
{
    public class RestControllerBase<T> : ControllerBase where T : Material
    {
        public DcvEntities Entities { get; set; }

        public BaseRepository<T> Repo { get; set; }

        public RestControllerBase()
        {
            Entities = new DcvEntities(Properties.Resources.ResourceManager.GetString("ProductionConnection"));
            Repo = GetRepository<T>() as BaseRepository<T>;
        }

        private IMaterialRepository GetRepository<U>()
        {
            if (typeof(U) == typeof(book))
                return new BookRepository(Entities);

            if (typeof(U) == typeof(notebook))
                return new NotebookRepository(Entities);

            if (typeof(U) == typeof(display))
                return new DisplayRepository(Entities);

            if (typeof(U) == typeof(equipment))
                return new EquipmentRepository(Entities);

            if (typeof(U) == typeof(furniture))
                return new FurnitureRepository(Entities);

            return null;
        }

        [HttpGet]
        public List<T> Get()

        {
            List<T> items = null;
            try
            {
                items = (List<T>)Repo.GetAll();
                return items;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Response.StatusCode = 500;
            }
            return items;
        }

        [HttpGet("{id}")]
        public T Get(int id)
        {
            T item = null;
            try
            {
                item = Repo.GetAny(id);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Response.StatusCode = 500;
            }
            return item;
        }

        [HttpPost]
        public void Post([FromBody] T item)
        {
            try
            {
                Repo.IsValid(item);
                var item1 = Repo.SetDefaultLocation(item);
                Repo.Save(item1);
                if (!item1.Equals(item))
                {
                    Response.WriteAsync($"Lagerplatz auf Standard geändert");
                }
                Response.StatusCode = 201;
            }
            catch (MaterialData.exceptions.InvalidInputException e)
            {
                Response.StatusCode = 403;
                Response.WriteAsync(e.Message);
            }
            catch (MaterialData.exceptions.DuplicateEntryException e)
            {
                Response.StatusCode = 409;
                Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Response.StatusCode = 500;
                Response.WriteAsync(e.Message);
            }
        }

        [HttpPut]
        public void Put([FromBody] T item)
        {
            try
            {
                Repo.Update(item);
                Response.StatusCode = 200;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            catch (Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}