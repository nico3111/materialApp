using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialData;
using MaterialData.models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaterialREST.Controllers
{
    [Route("[controller]/Notebook/")]
    [ApiController]
    public class MaterialController : ControllerBase
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
    }
}