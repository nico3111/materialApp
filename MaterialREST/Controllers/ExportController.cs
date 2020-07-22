using ClosedXML.Excel;
using ClosedXML.Extensions;
using MaterialData.models;
using MaterialData.models.material;
using MaterialData.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace MaterialREST.Controllers
{
    [Route("material/export")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private ExportRepository export;

        public ExportController()
        {
            export = new ExportRepository(new DcvEntities(Properties.Resources.ResourceManager.GetString("ProductionConnection")));
        }

        [HttpPost]
        public ActionResult Post([FromBody] search materials)
        {
            try
            {
                var file = export.Export(materials);
                MemoryStream stream = GetStream(file);
                return File(stream,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Material.xlsx");                
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public MemoryStream GetStream(XLWorkbook file)
        {
            MemoryStream stream = new MemoryStream();
            file.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}