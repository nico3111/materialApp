using MaterialData;
using MaterialData.models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MaterialREST.Controllers
{
    [Route("material/notebook")]
    [ApiController]
    public class NotebookController : ControllerBase
    {

        NotebookRepository notebookRepo = new NotebookRepository();

        [EnableCors("Policy1")]

        [HttpGet]
        public List<notebook> get()
        {
            //HttpStatusCode.OK;
            List<notebook> notebooks = null;
            try
            {
                notebooks = (List<notebook>)notebookRepo.GetAll();
                return notebooks;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);

                Response.StatusCode = 500;
            }

            return notebooks;
        }

        [HttpGet("{id}")]
        public notebook get(int id)
        {
            notebook notebook = null;
            try
            {
                notebook = notebookRepo.GetAny(id);
                return notebook;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;               
            }
            return notebook;
        }

        [HttpPost]
        public void post([FromBody]notebook notebook)
        {
            try
            {
                notebookRepo.Save(notebook);
                Response.StatusCode = 201;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }
        }

        [HttpDelete]
        public void delete(notebook notebook)
        {
            try
            {
                notebookRepo.Delete(notebook);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;                
            }
            
        }

        [HttpPut]
        public void put(notebook notebook)
        {
            try
            {
                notebookRepo.Update(notebook);
                Response.StatusCode = 200;
            }
            catch (System.Exception)
            {
                Response.StatusCode = 500;
            }           
        } 
    }
}