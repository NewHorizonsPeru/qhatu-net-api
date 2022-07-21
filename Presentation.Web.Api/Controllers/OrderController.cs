using Application.MainModule.DTO;
using Application.MainModule.IServices;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Api.Filters;

namespace Presentation.Web.Api.Controllers
{
    [CustomAuthorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public IActionResult GetById(int id)
        {
            return BadRequest(_orderService.GetById(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(OrderDto orderDto)
        {
            _orderService.Add(orderDto);
            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(int id, OrderDto orderDto)
        {
            _orderService.Update(id, orderDto);
            return Ok();
        }

        [HttpDelete]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            _orderService.Remove(id);
            return Ok();
        }
    }
}