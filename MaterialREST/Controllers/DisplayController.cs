using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/display")]
    [ApiController]
    public class DisplayController : ControllerBase
    {

        DisplayRepository displayRepo = new DisplayRepository();



        [HttpGet]
        public List<display> Get()
        {
            List<display> displays = null;
            try
            {
                displays = (List<display>)displayRepo.GetAll();
                return displays;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }
            return displays;
        }

        [HttpGet("{id}")]
        public display Get(int id)
        {
            display display = null;
            try
            {
                display = displayRepo.GetAny(id);
                return display;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
            return display;
        }

        [HttpPost]
        public void Post([FromBody] display display)
        {
            try
            {
                displayRepo.Save(display);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] display display)
        {
            try
            {
                displayRepo.Update(display);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(display display)
        {
            try
            {
                displayRepo.Delete(display);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
