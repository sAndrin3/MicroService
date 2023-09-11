using AutoMapper;
using Order.Models;
using Order.Models.Dto;
using Order.Models.Dtos;
using orders.Services.IService;
using Order.Data;

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
    }
}

