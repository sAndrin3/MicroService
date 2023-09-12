using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cart.Data;
using Cart.Models;
using Cart.Models.Dtos;
using Cart.Services.IService;
using Cart.Services;
using System.Net.Http.Headers;


namespace Cart.Services{
    public class CartService : ICartService{
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IProductInterface _productInterface;
        private readonly ICouponService _couponService;
        public CartService(AppDbContext db, IMapper mapper, IProductInterface productInterface, ICouponService couponService){
            _appDbContext = db;
            _mapper = mapper;
            _productInterface = productInterface;
            _couponService = couponService; 
        }
        public async Task<bool> ApplyCoupons(CartDto cartDto){
            // Get Header
            CartHeader cartHeaderFromDb = await _appDbContext.CartHeaders.FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
            cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
            _appDbContext.CartHeaders.Update(cartHeaderFromDb);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CartUpsert(CartDto cartDto){
             CartHeader CartHeaderFromDb = await _appDbContext.CartHeaders.FirstOrDefaultAsync(x => x.UserId ==
            cartDto.CartHeader.UserId
            );
           
       
            if (CartHeaderFromDb == null)
            {
                 System.Console.WriteLine("xghxg"); 
                var newCartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                _appDbContext.CartHeaders.Add(newCartHeader);
                await _appDbContext.SaveChangesAsync();

                // Use the Id above for CartDetails
                // Assign CartHeaderId
                cartDto.CartDetails.First().CartHeaderId = newCartHeader.CartHeaderId;
                var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
                _appDbContext.CartDetails.Add(cartDetails);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            // else {
            //     // Either Adding a new item or Updating the count of an Existing item
            //     CartDetails cartDetailsFromDb = await _appDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(x=>x.ProductId==
            //     cartDto.CartDetails.First().ProductId && x.CartHeaderId==CartHeaderFromDb.CartHeaderId);

            //     if(cartDetailsFromDb == null){
            //         // Different product
            //         cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
            //         var cartDetails = _mapper.Map<CartDetails>(cartDto.CartDetails.First());
            //         _appDbContext.CartDetails.Add(cartDetails);
            //         await _appDbContext.SaveChangesAsync();
            //     }
            //     else{
            //         cartDetailsFromDb.Count += cartDto.CartDetails.First().Count;
            //         _appDbContext.CartDetails.Update(cartDetailsFromDb);
            //         await _appDbContext.SaveChangesAsync();
            //     }
            //     return true;
            // }
            return false;
        }

        public async Task<CartDto> GetUserCart(Guid userId){
            var cartHeader = await _appDbContext.CartHeaders.FirstOrDefaultAsync(x=>x.UserId == userId);
            var cartDetails = _appDbContext.CartDetails.Where(x=>x.CartHeaderId==cartHeader.CartHeaderId).ToList();
            CartDto cart = new CartDto(){
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails)
            };
              //Calculate Cart Total
            var products =await _productInterface.GetProductAsync();
            System.Console.WriteLine(products.Count());

            foreach (var item in cart.CartDetails)
            {
                System.Console.WriteLine(item.ProductId);
                item.Product= products.FirstOrDefault(x=>x.ProductId==item.ProductId);
                cart.CartHeader.CartTotal += (int) (item.Count * item.Product.Price);
            }
             //Apply Coupon
            // if (!string.IsNullOrWhiteSpace(cart.CartHeader.CouponCode))
            // {
            //     //there is a coupon
            //     var coupon = await _couponService.GetCouponData(cart.CartHeader.CouponCode);
            //     if(coupon != null && cart.CartHeader.CartTotal> coupon.CouponMinAmont) 
            //     {
            //         cart.CartHeader.CartTotal -= coupon.CouponAmount;
            //         cart.CartHeader.Discount=coupon.CouponAmount;
            //     }
            // }
            return cart;
        }
         public async Task<bool> RemoveFromCart(Guid CartDetailId)
        {

            CartDetails cartDetails = await _appDbContext.CartDetails.FirstOrDefaultAsync(x => x.CartDetailsId == CartDetailId);

            // is this the last item the user is deleting 
            var itemsCount = _appDbContext.CartDetails.Where(c=>c.CartHeaderId==cartDetails.CartHeaderId).Count();
            
            _appDbContext.CartDetails.Remove(cartDetails);

            if(itemsCount ==1) 
            {
                _appDbContext.CartHeaders.Remove(_appDbContext.CartHeaders.FirstOrDefault(x => x.CartHeaderId == cartDetails.CartHeaderId));
            }

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}