using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Models.Dto;

namespace orders.Services.IService
{
    public interface IOrderService
    {
          Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);
    }
}