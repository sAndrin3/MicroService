using AutoMapper;
using Order.Models;
using Order.Models.Dto;
using Order.Models.Dtos;
using orders.Services.IService;
using Order.Data;
using Stripe.Checkout;
using Microsoft.EntityFrameworkCore;

namespace orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(IMapper mapper, AppDbContext  appDbContext)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto)
        {
              OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
            orderHeaderDto.Status = "Pending";
            orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
            orderHeaderDto.OrderTotal= Math.Round(orderHeaderDto.OrderTotal,2);
            OrderHeader newOrder = _mapper.Map<OrderHeader>(orderHeaderDto);
            //await Console.Out.WriteLineAsync(newOrder.UserId);
            var item =_context.OrderHeaders.Add(newOrder).Entity;
            _context.OrderHeaders.Add(_mapper.Map<OrderHeader>(orderHeaderDto));
            await _context.SaveChangesAsync();


            orderHeaderDto.OrderHeaderId= item.OrderHeaderId;
            return orderHeaderDto;
        }

        public async Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto)
        {
            var options = new SessionCreateOptions(){
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>(),
                PaymentMethodTypes = new List<string>(){"card"}
            };

            foreach(var item in stripeRequestDto.OrderHeader.OrderDetails){
                var SessionLineItem = new SessionLineItemOptions(){
                    PriceData = new SessionLineItemPriceDataOptions(){
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "kes",
                        ProductData = new SessionLineItemPriceDataProductDataOptions(){
                            Name = item.ProductName
                        },
                        
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(SessionLineItem);
            }

              var DiscountObj = new List<SessionDiscountOptions>()
           {
               new SessionDiscountOptions()
               {
                   Coupon= stripeRequestDto.OrderHeader.CouponCode
               }
           };

            if (stripeRequestDto.OrderHeader.Discount > 0)
            {
                options.Discounts = DiscountObj;
            }

            var service = new SessionService();
            Session session = service.Create(options);

            stripeRequestDto.StripeSessionId = session.Id;
            stripeRequestDto.StripeSessionUrl = session.Url;

            OrderHeader order = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);

            order.StripeSessionId = session.Id;
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }

        Task<bool> IOrderService.ValidatePayment(Guid OrderId)
        {
            throw new NotImplementedException();
        }
    } 
}

