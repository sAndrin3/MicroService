﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Models.Dto;
using Order.Models.Dtos;
using orders.Services.IService;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
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


        [HttpPost("StripePayment")]
        public async Task<ActionResult<ResponseDto>> StripePayment(StripeRequestDto stripeRequestDto)
        {
            try
            {
               var response =await _orderService.StripePayment(stripeRequestDto);
                _responseDto.Result=response;   
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

         [HttpPost("ValidatePayment")]
        public async Task<ActionResult<ResponseDto>> ValidatePayment(Guid orderId)
        {
            try
            {
               var response =await _orderService.ValidatePayment(orderId);
                _responseDto.Result=response;   
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}