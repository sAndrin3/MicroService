using Cart.Models.Dtos;

namespace Cart.Services.IService
{
    public interface IProductInterface
    {
        Task<IEnumerable<ProductDto>> GetProductAsync();
    }
}