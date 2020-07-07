using MaterialData.models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/equipment")]
    [ApiController]
    public class EquipmentController : RestControllerBase<equipment>
    {
    }
}