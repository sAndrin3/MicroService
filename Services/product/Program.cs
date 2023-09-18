using Microsoft.EntityFrameworkCore;
using JituProduct.Data;
using JituProduct.Extensions;
using JituProduct.Services;
using JituProduct.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Connection to DB

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddScoped<IProductInterface, ProductService>();

// setting cors policy
builder.Services.AddCors(options => options.AddPolicy("policy1", build =>{
    build.WithOrigins("http://localhost:5120");
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}
));

//custom builders

builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//migration
app.UseMigration();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("policy1");
app.MapControllers();

app.Run();