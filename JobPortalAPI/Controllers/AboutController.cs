using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Controllers
{
    [ApiController]
    [Route("api/about")]
    public class AboutController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("WELCOME TO JOB PORTAL API !");
        }
    }
}
