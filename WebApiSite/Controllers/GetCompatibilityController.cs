using System.Linq;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class GetCompatibilityController : Controller
    {
        [HttpGet("details")]
        [Route("[controller]")]
        public Compatibility Get(int zodiacType1, int zodiacType2)
        {
            var compatibility = from i in new ZodiacDbConnection().GetCompatibilities()
                where i.Zodiac1.Type.Equals((ZodiacType) zodiacType1) && i.Zodiac2.Type.Equals((ZodiacType) zodiacType2)
                select i;
            return compatibility.ToList()[0];
        }
    }
}