using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cart.Models
{
    public class CartHeader
    {
        [Key]
        public Guid CartHeaderId { get; set; }

        [Required]
        public Guid UserId { get; set; }


        public string? CouponCode { get; set; } = string.Empty;

        //Not referenced to the database 
        [NotMapped]
        public int Discount { get; set; }

        [NotMapped]
        public int CartTotal { get; set; }
    }
}