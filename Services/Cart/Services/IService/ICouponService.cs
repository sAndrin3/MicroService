using Cart.Models.Dtos;

namespace Cart.Services.Iservice
{
    public interface ICouponService
    {

        Task<CouponDto> GetCouponData(string CouponCode);
    }
}