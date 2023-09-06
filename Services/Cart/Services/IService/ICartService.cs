using Cart.Models.Dtos;

namespace Cart.Services.IService
{
    public interface ICartService
    {

        Task<bool> CartUpsert(CartDto cartDto);   
        Task<CartDto> GetUserCart(Guid userId);

        Task<bool> ApplyCoupons(CartDto cartDto);

        Task<bool> RemoveFromCart(Guid CartDetailId);
    }
}