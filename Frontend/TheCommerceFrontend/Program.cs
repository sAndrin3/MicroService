using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheCommerceFrontend;
using TheCommerceFrontend.Services.Auth;
using TheCommerceFrontend.Services.Coupons;
using TheCommerceFrontend.Services.Product;
using TheCommerceFrontend.Services.Cart;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TheCommerceFrontend.Services.AuthProvider;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// localstorage registration
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<ICouponInterface, CouponService>();
builder.Services.AddScoped<ICartInterface, CartService>();

// Authprovider configuration
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvideService>();

await builder.Build().RunAsync();
