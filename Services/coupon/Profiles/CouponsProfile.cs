using AutoMapper;
using Coupons.Models;
using Coupons.Models.Dtos;

namespace Coupons.Profiles{
    public class CouponsProfile:Profile{
        public CouponsProfile(){
            CreateMap<CouponRequestDto,Coupon>().ReverseMap();
        }
    }
}