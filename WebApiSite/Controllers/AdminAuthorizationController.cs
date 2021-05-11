using Bogus;
using DatabaseConnection;
using DatabaseConnection.entities;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class AdminAuthorizationController : Controller
    {
        [Route("[controller]")]
        [HttpGet]
        public IActionResult Get(string login, string password)
        {
            if (new ZodiacDbConnection().IsSuchAdminPresent(new Admin(login, password)))
                return Redirect($"pages/admin/admin_dashboard.html?sessionToken={CreateAndReturnAdminSession()}");

            return Redirect("pages/admin/admin_authorization.html?invalidAuth=true");
        }

        private string CreateAndReturnAdminSession()
        {
            var faker = new Faker();
            var sessionToken = faker.Random.String(12, 'a', 'z');
            Hook.SessionTokens.Add(sessionToken);
            return sessionToken;
        }
    }
}