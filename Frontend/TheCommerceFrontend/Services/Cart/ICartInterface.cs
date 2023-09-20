using TheCommerceFrontend.Models;
using TheCommerceFrontend.Models.Cart;

namespace TheCommerceFrontend.Services.Cart
{
    public interface ICartInterface
    {

        Task<ResponseDto> AddToCart(CartDto cartDto);

        Task<CartDto> GetCartByUserId(Guid userId);

        Task<ResponseDto> ApplyCoupons(CartDto cartDto);


        Task<ResponseDto> RemoveFromCart(Guid cartDetailId);


    }
}