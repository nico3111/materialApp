using MaterialData;
using MaterialData.models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
            return (List<notebook>)notebookRepo.GetAll();
        }

        [HttpGet("{id}")]
        public notebook get(int id)
        {
            return notebookRepo.GetAny(id);
        }


        [HttpPost]
        public void post(notebook notebook)
        {
            notebookRepo.Save(notebook);
        }

        [HttpDelete]
        public void delete(notebook notebook)
        {
            notebookRepo.Delete(notebook);
        }

        [HttpPut]
        public void put(notebook notebook)
        {
            notebookRepo.Update(notebook);
        }
    }
}