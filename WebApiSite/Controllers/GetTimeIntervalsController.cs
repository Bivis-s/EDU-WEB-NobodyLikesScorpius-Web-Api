using System.Collections.Generic;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;

namespace TryToWebApi.Controllers
{
    public class GetTimeIntervalsController : Controller
    {
        [HttpGet("details")]
        [Route("[controller]")]
        public List<TimeInterval> Get()
        {
            return new ZodiacDbConnection().GetTimeIntervals();
        }
    }
}