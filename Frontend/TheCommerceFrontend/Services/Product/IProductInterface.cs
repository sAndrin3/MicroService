using TheCommerceFrontend.Models;
using TheCommerceFrontend.Models.Products;

namespace TheCommerceFrontend.Services.Product
{
    public interface IProductInterface
    {

        Task<List<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid id);

        Task<ResponseDto> AddProduct(ProductRequestDto product);

        Task<ResponseDto> deleteProduct(Guid id);

        Task<ResponseDto> updateProduct(Guid id, ProductRequestDto productRequestDto);
    }
}