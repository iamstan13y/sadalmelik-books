using Microsoft.AspNetCore.Mvc;

namespace sadalmelik_books.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("SHit 1");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult GetV12()
        {
            return Ok("SHit 1.2");
        }
    }
}
