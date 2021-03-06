using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    public class IsAdminAuthorizedController : Controller
    {
        [Route("[controller]")]
        [HttpGet]
        public bool Get(string sessionToken)
        {
            return Hook.IsAdminSessionRegistered(sessionToken);
        }
    }
}