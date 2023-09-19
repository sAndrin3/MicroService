using TheCommerceFrontend.Models;
using TheCommerceFrontend.Models.Coupons;

namespace TheCommerceFrontend.Services.Coupons{
    public interface ICouponInterface{
        Task<List<Coupon>> GetCouponsAsync();
        Task<Coupon> GetCouponAsync(string code);
        Task<ResponseDto> AddCoupon(CouponRequestDto couponRequestDto);
        Task<ResponseDto> DeleteCoupon(Guid id);
        Task<ResponseDto> UpdateCoupon(Guid id ,CouponRequestDto couponRequestDto);
    }
}