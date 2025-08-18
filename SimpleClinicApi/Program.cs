using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleClinicApi.DataAccess;
using SimpleClinicApi.Extensions;
using SimpleClinicApi.Infrastructure.Auth.Commands;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var databaseProvider = builder.Configuration["DatabaseProvider"];

switch (databaseProvider)
{
   case "Postgres":
      builder.Services.AddDbContext<ClinicDbContext>(options =>
                                                        options.UseNpgsql(configuration
                                                                             .GetConnectionString("Postgres")));

      break;
   case "SQLite":
      builder.Services.AddDbContext<ClinicDbContext>(options =>
                                                        options.UseSqlite(configuration.GetConnectionString("SQLite")));

      break;
   default:
      throw new Exception("Unsupported database provider");
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ClinicDbContext>()
       .AddDefaultTokenProviders();

// Configure JWT authentication
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
                                   {
                                      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                   })
       .AddJwtBearer(options =>
                     {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                           ValidateIssuer = true,
                           ValidIssuer = configuration["Jwt:Issuer"],
                           ValidateAudience = true,
                           ValidAudience = configuration["Jwt:Audience"],
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,

                           IssuerSigningKey = new SymmetricSecurityKey(key),
                        };
                     });

// Add Authorization services
builder.Services.AddAuthorization();

builder.Services.AddClinicServices();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();