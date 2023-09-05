using System.ComponentModel.DataAnnotations;
namespace Coupons.Models{
    public class Coupon{
        public Guid CouponId {get; set; }
        public string CouponCode {get; set; } = string.Empty;
        public int CouponAmount {get; set; }
        public int CouponMinAmount {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
    }
}