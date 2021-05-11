using Microsoft.AspNetCore.Mvc;

namespace TryToWebApi.Controllers
{
    public class AdminController : Controller
    {
        [Route("[controller]")]
        [HttpGet]
        public IActionResult Get()
        {
            return Redirect("pages/admin/admin_authorization.html");
        }
    }
}