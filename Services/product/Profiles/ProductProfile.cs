using AutoMapper;
using JituProduct.Models;
using JituProduct.Models.Dtos;

namespace JituProduct.Profiles
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<ProductRequestDto,Product>().ReverseMap();
        }
    }
}