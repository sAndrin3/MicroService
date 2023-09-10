using System.ComponentModel.DataAnnotations;

namespace Rewards.Models
{
    public class JituRewards
    {
        [Key]
        public Guid RewardsId { get; set; }

        [Required]
        public string UserId { get; set; }=string.Empty;    

        [Required]
        public int RewardsAmount { get;set; }
    }
}