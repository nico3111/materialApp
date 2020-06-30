using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialData;
using MaterialData.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaterialREST.Controllers
{
    [Route("[controller]/Notebook")]
    [ApiController]
    public class MaterialController : ControllerBase
    {

        NotebookRepository notebookRepo = new NotebookRepository();

        [HttpGet]
        public List<notebook> get()
        {
            return (List<notebook>)notebookRepo.GetAll();
        }

        [HttpPost]
        public void post(notebook notebook)
        {
            notebookRepo.Save(notebook);
        }
    }
}
