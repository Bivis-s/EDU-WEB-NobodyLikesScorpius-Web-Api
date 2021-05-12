using System.Collections.Generic;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;

namespace TryToWebApi.Controllers
{
    public class GetZodiacsController : Controller
    {
        [HttpGet("details")]
        [Route("[controller]")]
        public List<Zodiac> Get()
        {
            return new ZodiacDbConnection().GetZodiacs();
        }
    }
}