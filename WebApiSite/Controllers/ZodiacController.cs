using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZodiacController
    {
        [HttpGet("details")]
        public Zodiac Get(int zodiacNumber)
        {
            try
            {
                return new ZodiacDbConnection().GetZodiac((ZodiacType) zodiacNumber);
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                return null;
            }
        }
    }
}