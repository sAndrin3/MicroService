using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheCommerceFrontend;
using TheCommerceFrontend.Services.Auth;
using TheCommerceFrontend.Services.Product;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<IAuthInterface, AuthService>();
await builder.Build().RunAsync();
