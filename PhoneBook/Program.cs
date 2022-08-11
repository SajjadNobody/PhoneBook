using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Repository;
using PhoneBook.DataLayer.Services;
using PhoneBook.Services.AppSettingProperty;
using PhoneBook.Services.Services;
using RelationTest.Services.Swagger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connect to SQL Server 
builder.Services.AddDbContext<ApplicationDbContext>(option =>
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Connect to SQL Server 

// Add Dependency Injection
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddScoped<IJwtTokenBuilder, JwtTokenBuilder>();
// Add Dependency Injection

builder.Services.AddEndpointsApiExplorer();

// set AppSetting's class property value from appsettings.json
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UserApi",
    });

    //if you want to have to version use this and code in useSwaggerUI code 
    //options.SwaggerDoc("v2", new OpenApiInfo
    //{
    //    Version = "v2",
    //    Title = "Books Api",
    //});

    // For adding Athentication 
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/User/Login", UriKind.Relative),
            }
        }
    });
    // Add Lock Icon to Swagger
    options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
    // Add Lock Icon to Swagger
});

// For Adding Athentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("AppSetting:JwtSetting:Audience"), // Give value from appsettings.json
        ValidIssuer = builder.Configuration.GetValue<string>("AppSetting:JwtSetting:Issued"),// Give value from appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration
        .GetValue<string>("AppSetting:JwtSetting:Key")))// Give value from appsettings.json
    };
});
// For Adding Athentication

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // to use swagger
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true; // if you want to have 2 version use this code 
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "User");
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book"); // for having 2 version 
    });
    // to use swagger
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
