using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeIntervalController
    {
        [HttpGet("details")]
        public TimeInterval Get(int timeIntervalNumber)
        {
            try
            {
                return new ZodiacDbConnection().GetTimeInterval((TimeIntervalType) timeIntervalNumber);
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