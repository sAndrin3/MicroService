using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Models.Dto;
using Order.Services.Iservice;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly ResponseDto _responseDto;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddOrder(CartDto cartDto)
        {
            try
            {
               var response =await _orderService.CreateOrderHeader(cartDto);
                _responseDto.Result=response;   
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}