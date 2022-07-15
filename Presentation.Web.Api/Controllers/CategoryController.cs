using Application.MainModule.DTO;
using Application.MainModule.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            return BadRequest(_categoryService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(CategoryDto categoryDto)
        {
            _categoryService.Add(categoryDto);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(int id, CategoryDto categoryDto)
        {
            _categoryService.Update(id, categoryDto);
            return Ok();
        }

        [HttpDelete]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            _categoryService.Remove(id);
            return Ok();
        }
    }
}
