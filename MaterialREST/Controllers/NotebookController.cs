using MaterialData.models;
using Microsoft.AspNetCore.Mvc;

namespace MaterialREST.Controllers
{
    [Route("material/notebook")]
    [ApiController]
    public class NotebookController : RestControllerBase<notebook>
    {
    }
}