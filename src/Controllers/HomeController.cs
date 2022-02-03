using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("helth-check")]
        public IActionResult Get() => Ok();

        [HttpGet("helth-check-api")]
        [ApiKey]
        public IActionResult GetHelthApi() => Ok();
    }
}
