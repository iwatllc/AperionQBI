using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AperionQB.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get() {
            return Ok("AperionQBI is running and reachable.");
        }
    }
}
