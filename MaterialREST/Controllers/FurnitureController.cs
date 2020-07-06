using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialData.models;
using MaterialData.repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialREST.Controllers
{
    [Route("material/furniture")]
    [ApiController]

    public class FurnitureController : RestControllerBase<furniture>
    {
    }
}
