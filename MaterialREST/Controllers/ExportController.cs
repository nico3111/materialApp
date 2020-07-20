using MaterialData.models;
using MaterialData.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/export")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private ExportRepository export;

        public ExportController(ExportRepository export)
        {
            this.export = export;
        }

        [HttpPost]
        public void Post([FromBody] Dictionary<string, List<Material>> materials)
        {
            export.Export(materials);
        }
    }
}