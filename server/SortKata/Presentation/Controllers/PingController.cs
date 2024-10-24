using Microsoft.AspNetCore.Mvc;

namespace SortKata.Presentation.Controllers {
    [ApiController]
    [Route("")]
    public class PingController : ControllerBase {
        [HttpGet("ping")]
        public ActionResult<string> Ping() {
            return Ok("pong");
        }
    }
}
