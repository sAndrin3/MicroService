using Cart.Models.Dtos;

namespace Cart.Services.IService
{
    public interface ICouponService
    {

        Task<CouponDto> GetCouponData(string CouponCode);
    }
}