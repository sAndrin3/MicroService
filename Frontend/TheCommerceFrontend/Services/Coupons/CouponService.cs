using Newtonsoft.Json;
using System.Text;
using TheCommerceFrontend.Models;
using TheCommerceFrontend.Models.Coupons;

namespace TheCommerceFrontend.Services.Coupons
{
    public class CouponService : ICouponInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "http://localhost:7002";
        public CouponService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ResponseDto> AddCoupon(CouponRequestDto couponRequestDto)
        {
            var request = JsonConvert.SerializeObject(couponRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Coupon", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return results;
            }
            return new ResponseDto();
        }

        public async  Task<ResponseDto> DeleteCoupon(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Coupon?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;
            }
            return new ResponseDto();
        }

        public async  Task<Coupon> GetCouponAsync(string code)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Coupon/GetByName/{code}");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<Coupon>(results.Result.ToString());

            }
            return new Coupon();
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Coupon");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<Coupon>>(results.Result.ToString());

            }
            return new List<Coupon>();
        }

        public async Task<ResponseDto> UpdateCoupon(Guid id, CouponRequestDto couponRequestDto)
        {
            var request = JsonConvert.SerializeObject(couponRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Coupon?id={id}",bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return results;
            }
            return new ResponseDto();
        }
    }
}