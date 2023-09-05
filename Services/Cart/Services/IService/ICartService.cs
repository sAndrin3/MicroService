using Cart.Models.Dtos;

namespace Cart.Services.Iservice
{
    public interface ICartservice
    {

        Task<bool> CartU    
        Task<CartDto> GetUserCart(Guid userId);

        Task<bool> ApplyCoupons(CartDto cartDto);

        Task<bool> RemoveFromCart(Guid CartDetailId);
    }
}