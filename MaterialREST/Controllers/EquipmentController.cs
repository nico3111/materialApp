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
    [Route("material/mousekeyboard")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        EquipmentRepository equipmentRepo = new EquipmentRepository();

        // GET: api/<mouseKeyboardController>
        [HttpGet]
        public IEnumerable<equipment> Get()
        {
            List<equipment> equipments = null;
            try
            {
                equipments = (List<equipment>)equipmentRepo.GetAll();
                return equipments;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }

            return equipments;
        }

        // GET api/<mouseKeyboardController>/5
        [HttpGet("{id}")]
        public equipment Get(int id)
        {
            equipment equipment = null;
            try
            {
                equipment = equipmentRepo.GetAny(id);
                return equipment;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
            return equipment;
        }

        // POST api/<mouseKeyboardController>
        [HttpPost]
        public void Post([FromBody] equipment equipment)
        {
            try
            {
                equipmentRepo.Save(equipment);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // PUT api/<mouseKeyboardController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] equipment equipment)
        {
            try
            {
                equipmentRepo.Update(equipment);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // DELETE api/<mouseKeyboardController>/5
        [HttpDelete("{id}")]
        public void Delete(equipment equipment)
        {
            try
            {
                equipmentRepo.Delete(equipment);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
