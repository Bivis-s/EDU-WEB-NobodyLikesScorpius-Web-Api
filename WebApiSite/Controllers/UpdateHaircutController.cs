using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class UpdateHaircutController : Controller
    {
        [Route("[controller]")]
        [HttpPost]
        public bool Post([FromBody] UpdateHaircutRequest request)
        {
            Console.WriteLine("Update Haircut " + request);
            if (Hook.IsAdminSessionRegistered(request.SessionToken))
            {
                var dbConnection = new ZodiacDbConnection();
                dbConnection.SaveOrUpdate(new Haircut(
                    request.Id,
                    dbConnection.GetZodiac(request.ZodiacId),
                    request.MoonDay,
                    request.MoonPhase,
                    request.Prediction,
                    request.IsPositive));
                return true;
            }

            Console.WriteLine($"Illegal access, {request.SessionToken} isn't registered!");
            return false;
        }
    }
}