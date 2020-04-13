using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
