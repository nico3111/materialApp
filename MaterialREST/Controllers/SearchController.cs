using MaterialData.exceptions;
using MaterialData.models;
using MaterialData.models.material;
using MaterialData.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public List<Material> Post([FromBody] search search)
        {
            try
            {
                Response.StatusCode = 200;
                return searchRepo.GetResult(search);
            }
            catch(NotFoundException e)
            {
                Response.WriteAsync(e.Message);
                return null;
            }
        }        
    }
}