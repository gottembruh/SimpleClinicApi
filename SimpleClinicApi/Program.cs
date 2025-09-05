using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleClinicApi.DataAccess;
using SimpleClinicApi.Extensions;
using SimpleClinicApi.Infrastructure.Errors;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var databaseProvider = builder.Configuration["DatabaseProvider"];

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

switch (databaseProvider)
{
    case "Postgres":
        builder.Services.AddDbContext<ClinicDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
        );

        break;
    case "SQLite":
        builder.Services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SQLite"))
        );

        break;
    default:
        throw new InvalidOperationException($"Unsupported database provider");
}


builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");

builder
    .Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ClinicDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

builder
    .Services.AddAuthentication(options =>
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


builder.Services.AddClinicServices();

builder.Services.AddCors();

// Add controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
})
    // .AddJsonOptions(opt =>
    // opt.JsonSerializerOptions.DefaultIgnoreCondition = System
    //     .Text
    //     .Json
    //     .Serialization
    //     .JsonIgnoreCondition
    //     .WhenWritingNull )
    ;
builder.Services.AddAuthorization();

// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Clinic API", Version = "v1" });

    x.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Enter JWT Bearer token **only**",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme },
                Scheme = "bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    x.SupportNonNullableReferenceTypes();
    x.CustomSchemaIds(y => y.FullName?.Replace("+", "."));
    x.DocInclusionPredicate((_, _) => true);

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");


    // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleClinic API V1"));
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
