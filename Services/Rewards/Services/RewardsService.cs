using Microsoft.EntityFrameworkCore;
using Rewards.Data;
using Rewards.Models;
using Rewards.Models.Dto;

namespace Rewards.Services
{
    public class RewardsService
    {
        private DbContextOptions<AppDBContext> options;

        public RewardsService(DbContextOptions<AppDBContext> options)
        {
            this.options = options;
        }


        public async Task  AddRewards (RewardsDto dto)
        {
            var reward = new JituRewards()
            {
                UserId = dto.UserId,
                RewardsAmount = (dto.TotalAmount / 100)
            };

            var _db = new AppDBContext(options);
            await _db.AddAsync(reward);
           await _db.SaveChangesAsync();
        }
    }
}