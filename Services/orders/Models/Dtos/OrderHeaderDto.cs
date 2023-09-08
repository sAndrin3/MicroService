using Order.Models.Dtos;

namespace Order.Models.Dto
{
    public class OrderHeaderDto
    {
        public Guid OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double OrderTotal { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string? Status { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}