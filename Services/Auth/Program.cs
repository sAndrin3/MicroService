using Auth.Data;
using Auth.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Auth.Models;
using Auth.Services.IService;
using Auth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Db Connection
builder.Services.AddDbContext<ApplicationDbContext>(options => {options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));});

// Register identityFramework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//RegisterServices
builder.Services.AddScoped<IUserInterface, UserService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Run any pending Migrations
app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
