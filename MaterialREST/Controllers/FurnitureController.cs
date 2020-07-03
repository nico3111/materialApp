using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/furniture")]
    [ApiController]

    public class FurnitureController : ControllerBase
    {
        FurnitureRepository furnitureRepo = new FurnitureRepository();

        // GET: api/<FurnitureController>
        [HttpGet]
        public List<furniture> Get()
        {
            List<furniture> furnitures = null;
            try
            {
                furnitures = (List<furniture>)furnitureRepo.GetAll();
                return furnitures;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }
            return furnitures;
        }

        // GET api/<FurnitureController>/5
        [HttpGet("{id}")]
        public furniture Get(int id)
        {
        furniture furniture = null;
        try
        {
            furniture = furnitureRepo.GetAny(id);
            return furniture;
        }
        catch (System.Exception)
        {
            Response.StatusCode = 500;
        }
        return furniture;
    }

        // POST api/<FurnitureController>
        [HttpPost]
        public void Post([FromBody] furniture furniture)
        {
            try
            {
                furnitureRepo.Save(furniture);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // PUT api/<FurnitureController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] furniture furniture)
        {
            try
            {
                furnitureRepo.Update(furniture);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // DELETE api/<FurnitureController>/5
        [HttpDelete("{id}")]
        public void Delete(furniture furniture)
        {
            try
            {
                furnitureRepo.Delete(furniture);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
