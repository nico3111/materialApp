using MaterialData;
using MaterialData.exceptions;
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
                Response.WriteAsync(e.Message);
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
                Response.WriteAsync(e.Message);
            }
            return item;
        }

        [HttpPost]
        public async void Post([FromBody] T item)
        {
            try
            {
                Repo.IsValid(item);
                if (item != null)
                {
                    var item1 = Repo.SetLocation(item);
                    await Repo.Save(item1);
                    Response.StatusCode = 201;
                }
            }
            catch (InvalidInputException e)
            {
                Response.StatusCode = 403;
                await Response.WriteAsync(e.Message);
            }
            catch (DuplicateEntryException e)
            {
                Response.StatusCode = 409;
                await Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                await Response.WriteAsync(e.Message);
            }
        }

        [HttpPut]
        public async void Put([FromBody] T item)
        {
            try
            {
                Repo.IsValid(item);
                item = Repo.SetLocation(item);
                await Repo.UpdateAsync(item);
                Response.StatusCode = 200;
            }
            catch (DuplicateEntryException e)
            {
                Response.StatusCode = 409;
                await Response.WriteAsync(e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                Response.StatusCode = 418;
                await Response.WriteAsync(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Response.StatusCode = 500;
                await Response.WriteAsync(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            try
            {
                await Repo.Delete(id);
                Response.StatusCode = 200;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                await Response.WriteAsync(e.Message);
            }
        }
    }
}