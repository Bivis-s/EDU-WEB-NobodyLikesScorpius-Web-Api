using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictionController
    {
        [HttpGet("details")]
        public Prediction Get(int zodiacNumber, int timeInterval)
        {
            try
            {
                return new ZodiacDbConnection().GetPrediction((ZodiacType) zodiacNumber,
                    (TimeIntervalType) timeInterval);
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