using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cart.Models.Dtos;
using Cart.Services.IService;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartService _cartService;
        private readonly ResponseDto _responseDto;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _responseDto = new ResponseDto();
        }

         [HttpPost]
        public async Task<ActionResult<ResponseDto>> CartUpsert(CartDto cartDto)
        {
            try
            {
                if (cartDto == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "cartDto is null.";
                    return BadRequest(_responseDto);
                }

                var response = await _cartService.CartUpsert(cartDto);

                if (response)
                {
                    _responseDto.Result = response;
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException?.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }


        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart([FromBody] Guid cartDetailsId)
        {
            try
            {
                var response = await _cartService.RemoveFromCart(cartDetailsId);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var response = await _cartService.ApplyCoupons(cartDto);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUserCart(Guid userId)
        {
            try
            {
                var response = await _cartService.GetUserCart(userId);
                _responseDto.Result = response;
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