using System;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class UpdateCompatibilityController : Controller
    {
        [Route("[controller]")]
        [HttpPost]
        public bool Post([FromBody] UpdateCompatibilityRequest compatibilityRequest)
        {
            Console.WriteLine("Update Compatibility " + compatibilityRequest);
            if (Hook.IsAdminSessionRegistered(compatibilityRequest.SessionToken))
            {
                var dbConnection = new ZodiacDbConnection();
                var compatibilityToSave = new Compatibility(
                    dbConnection.GetCompatibility((ZodiacType) compatibilityRequest.ZodiacType1,
                        (ZodiacType) compatibilityRequest.ZodiacType2).Id,
                    dbConnection.GetZodiac((ZodiacType) Convert.ToInt32(compatibilityRequest.ZodiacType1)),
                    dbConnection.GetZodiac((ZodiacType) Convert.ToInt32(compatibilityRequest.ZodiacType2)),
                    compatibilityRequest.CompatibilityValue, compatibilityRequest.Text);
                dbConnection.SaveOrUpdate(compatibilityToSave);
                return true;
            }

            Console.WriteLine($"Illegal access, token '{compatibilityRequest.SessionToken}' isn't registered!");
            return false;
        }
    }
}