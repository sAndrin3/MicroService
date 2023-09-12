using AutoMapper;
using Cart.Models;
using Cart.Models.Dtos;

namespace Cart.Profiles{
    public class CartProfile : Profile{
        public CartProfile(){
            CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            CreateMap<CartDetailsDto, CartDetails>().ReverseMap();
        } 
    }
}