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
    public class MouseKeyboardController : ControllerBase
    {
        MouseKeyboardRepository mouseKeyboardRepo = new MouseKeyboardRepository();

        // GET: api/<mouseKeyboardController>
        [HttpGet]
        public IEnumerable<mouseKeyboard> Get()
        {
            List<mouseKeyboard> mouseKeyboards = null;
            try
            {
                mouseKeyboards = (List<mouseKeyboard>)mouseKeyboardRepo.GetAll();
                return mouseKeyboards;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }

            return mouseKeyboards;
        }

        // GET api/<mouseKeyboardController>/5
        [HttpGet("{id}")]
        public mouseKeyboard Get(int id)
        {
            mouseKeyboard mouseKeyboard = null;
            try
            {
                mouseKeyboard = mouseKeyboardRepo.GetAny(id);
                return mouseKeyboard;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
            return mouseKeyboard;
        }

        // POST api/<mouseKeyboardController>
        [HttpPost]
        public void Post([FromBody] mouseKeyboard mouseKeyboard)
        {
            try
            {
                mouseKeyboardRepo.Save(mouseKeyboard);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // PUT api/<mouseKeyboardController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] mouseKeyboard mouseKeyboard)
        {
            try
            {
                mouseKeyboardRepo.Update(mouseKeyboard);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        // DELETE api/<mouseKeyboardController>/5
        [HttpDelete("{id}")]
        public void Delete(mouseKeyboard mouseKeyboard)
        {
            try
            {
                mouseKeyboardRepo.Delete(mouseKeyboard);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }
    }
}
