using Microsoft.EntityFrameworkCore;
using Coupons.Data;
using Coupons.Models;
using Coupons.Services.IService;

namespace Coupons.Services
{
    public class CouponService : ICouponInterface
    {
        private readonly AppDbContext _context;

        public CouponService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCouponByIdAsync(Guid id)
        {
            return await _context.Coupons.FirstOrDefaultAsync(x => x.CouponId == id);
        }

        public async Task<Coupon> GetCouponByNameAsync(string couponCode)
        {
            return await _context.Coupons.FirstOrDefaultAsync(x => x.CouponCode.ToLower() == couponCode.ToLower());
        }

        public async Task<string> AddCouponAsync(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Added Successfully";
        }

        public async Task<string> UpdateCouponAsync(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Updated Successfully";
        }

        public async Task<string> DeleteCouponAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Deleted Successfully";
        }
    }
}
