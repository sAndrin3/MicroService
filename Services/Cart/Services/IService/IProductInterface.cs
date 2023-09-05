using Cart.Models.Dtos;

namespace Cart.Services.Iservice
{
    public interface IProductInterface
    {
        Task<IEnumerable<ProductDto>> GetProductaAsync();
    }
}