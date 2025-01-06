using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{

    [Route("")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> printversion()
        {
            
            return Ok(new { Message = "application web api v1.0.0" });
        }

    }
}
