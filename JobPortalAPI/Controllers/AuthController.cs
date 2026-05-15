using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
