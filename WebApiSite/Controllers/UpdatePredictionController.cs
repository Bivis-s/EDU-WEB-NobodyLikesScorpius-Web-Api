using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class UpdatePredictionController : Controller
    {
        [Route("[controller]")]
        [HttpPost]
        public bool Post([FromBody] UpdatePredictionRequest prediction)
        {
            Console.WriteLine("Update Prediction " + prediction);
            if (Hook.IsAdminSessionRegistered(prediction.SessionToken))
            {
                var dbConnection = new ZodiacDbConnection();
                var predictionToSave = new Prediction(
                    dbConnection.GetZodiac((ZodiacType) Convert.ToInt32(prediction.Zodiac)),
                    dbConnection.GetTimeInterval((TimeIntervalType) Convert.ToInt32(prediction.TimeInterval)),
                    prediction.Text);
                dbConnection.SaveOrUpdate(predictionToSave);
                return true;
            }

            Console.WriteLine($"Illegal access, {prediction.SessionToken} isn't registered!");
            return false;
        }
    }
}