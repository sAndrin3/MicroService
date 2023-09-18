using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.Extensions;
using orders.Services.IService;
using orders.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Service
builder.Services.AddScoped<IOrderService, OrderService>();




//custom builder services
builder.AddAppAuthentication();
builder.AddSwaggenGenExtension();

var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = "sk_test_51NrhO5G2Lky7lJV4c0OCnwwlapNtPvUawnsz0EpL7Y8Pk7DWNWtP5v6KAdRYRPWnZky6jFWPA7VzTJEae2x25LBY00ux2gqBcs";
// builder.Configuration.GetSection("stripe:Key").Get<string>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

