using Order.Models.Dto;


namespace Order.Services.Iservice
{
    public interface IOrderService
    {
        Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);


        // Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto);

        // Task<bool> ValidatePayment(Guid OrderId);
    }
}