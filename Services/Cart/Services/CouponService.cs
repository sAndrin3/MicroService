using Newtonsoft.Json;
using Cart.Models.Dtos;
using Cart.Services.IService;

namespace Cart.Services{
    public class CouponService : ICouponService{
        private readonly IHttpClientFactory _clientFactory;
        public CouponService(IHttpClientFactory clientFactory){
            _clientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCouponData(string CouponCode){
            var client = _clientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/Coupon/GetByName/{CouponCode}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

            if(responseDto.IsSuccess){
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return new CouponDto();
        }
    }
}