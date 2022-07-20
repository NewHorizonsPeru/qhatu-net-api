using Microsoft.AspNetCore.Mvc;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Web.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            return BadRequest(_productService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ProductDto productDto)
        {
            _productService.Add(productDto);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(int id, ProductDto productDto)
        {
            _productService.Update(id, productDto);
            return Ok();
        }

        [HttpDelete]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            _productService.Remove(id);
            return Ok();
        }
    }
}
