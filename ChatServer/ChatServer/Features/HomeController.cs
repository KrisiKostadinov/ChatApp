using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    public class HomeController : ApiController
    {
        public HomeController()
        {
        }

        [Authorize]
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return Ok("Works");
        }
    }
}
