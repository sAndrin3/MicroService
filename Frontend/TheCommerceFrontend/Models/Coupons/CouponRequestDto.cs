using System.ComponentModel.DataAnnotations;
namespace TheCommerceFrontend.Models.Coupons{
    public class CouponRequestDto{
        [Required]
        public string CouponCode {get; set; } = string.Empty;
        [Required]
        [Range(100, 100000)]
        public int CouponAmount {get; set; }
        [Required]
        public int CouponMinAmount {get; set; }
    }
}