using Order.Models.Dto;
using Order.Models.Dtos;

namespace orders.Services.IService
{
    public interface IOrderService
    {
          Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);
          Task <StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto);
          Task <bool> ValidatePayment(Guid OrderId);
    }
}