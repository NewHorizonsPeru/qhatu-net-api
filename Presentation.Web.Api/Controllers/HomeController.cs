using System.Linq;
using Infrastructure.Data.Core.Context;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly QhatuContext _context;

        public HomeController(QhatuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getProducts")]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
    }
}
