using System.Collections.Generic;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;

namespace TryToWebApi.Controllers
{
    public class GetHaircutsController : Controller
    {
        [HttpGet("details")]
        [Route("[controller]")]
        public List<Haircut> Get()
        {
            return new ZodiacDbConnection().GetHaircuts();
        }
    }
}