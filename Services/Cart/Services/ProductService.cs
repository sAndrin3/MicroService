using Newtonsoft.Json;
using Cart.Models.Dtos;
using Cart.Services.IService;

namespace Cart.Services
{
    public class ProductService : IProductInterface
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProductService(IHttpClientFactory clientFactory)
        {

            _clientFactory = clientFactory;

        }
        public async Task<IEnumerable<ProductDto>> GetProductAsync(){
            // Create a client
            var client = _clientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/Product");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess){
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}