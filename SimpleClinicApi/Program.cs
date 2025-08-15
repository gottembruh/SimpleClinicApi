using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleClinicApi.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var databaseProvider = builder.Configuration["DatabaseProvider"];

if (databaseProvider == "Postgres")
{
   builder.Services.AddDbContext<ClinicDbContext>(options =>
                                                     options.UseNpgsql(builder.Configuration
                                                                              .GetConnectionString("Postgres")));
}
else if (databaseProvider == "SQLite")
{
   builder.Services.AddDbContext<ClinicDbContext>(options =>
                                                     options.UseSqlite(builder.Configuration
                                                                              .GetConnectionString("SQLite")));
}
else
{
   throw new Exception("Unsupported database provider");
}

// Добавляем DbContext с Identity для пользователя
builder.Services.AddDbContext<ClinicDbContext>(options =>
                                                  options.UseSqlServer(configuration
                                                                          .GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ClinicDbContext>()
       .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();