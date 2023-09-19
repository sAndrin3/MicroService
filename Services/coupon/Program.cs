using Microsoft.EntityFrameworkCore;
using Coupons.Data;
using Coupons.Extensions;
using Coupons.Services;
using Coupons.Services.IService;

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

builder.Services.AddCors(options => options.AddPolicy("policy1", build =>{
    build.WithOrigins("http://localhost:5120");
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}
));

// Register Service
builder.Services.AddScoped<ICouponInterface, CouponService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add custom service
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = "sk_test_51NrhO5G2Lky7lJV4c0OCnwwlapNtPvUawnsz0EpL7Y8Pk7DWNWtP5v6KAdRYRPWnZky6jFWPA7VzTJEae2x25LBY00ux2gqBcs";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("policy1");
app.MapControllers();

app.Run();
