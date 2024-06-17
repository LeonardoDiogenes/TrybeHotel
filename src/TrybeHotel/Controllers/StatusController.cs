using Microsoft.AspNetCore.Mvc;


namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("/")]
    public class StatusController : Controller
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { message = "online" });
        }
    }
}
