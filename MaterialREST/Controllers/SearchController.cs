using MaterialData.models;
using MaterialData.models.material;
using MaterialData.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MaterialREST.Controllers
{
    [Route("material/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        public SearchRepository searchRepo;

        public SearchController()
        {
            searchRepo = new SearchRepository(new DcvEntities(Properties.Resources.ResourceManager.GetString("ProductionConnection")));
        }

        [HttpPost]
        public Dictionary<string, List<Material>> Post([FromBody] search search)
        {
            try
            {
                Response.StatusCode = 200;
                return searchRepo.GetResult(search);
            }
            catch (Exception e)
            {
                Response.WriteAsync(e.Message);
                return null;
            }
        }
    }
}