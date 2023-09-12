using AutoMapper;
using Order.Models;
using Order.Models.Dto;
using Order.Models.Dtos;

namespace orders.Profiles{
    public class orderprofiles: Profile{
        public orderprofiles(){
            CreateMap<CartHeaderDto, OrderHeaderDto>().ForMember(dest => dest.OrderTotal, src => src.MapFrom(x=>x.CartTotal)).ReverseMap();
            CreateMap<CartDetailsDto, OrderDetailsDto>().ForMember(dest => dest.ProductName, src => src.MapFrom(x => x.Product.Name)).ForMember(dest => dest.Price, src => src.MapFrom(x => x.Product.Price));

            CreateMap<OrderDetailsDto, CartDetailsDto>();
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
        }
    }
}