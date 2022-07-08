using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("printMessage")]
        public IActionResult printMessage()
        {
            return Ok("Mensaje desde un controlador en .NET 5 👷‍♂️");
        }
    }
}
