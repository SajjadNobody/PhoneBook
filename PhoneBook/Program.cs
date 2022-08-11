using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Repository;
using PhoneBook.DataLayer.Services;
using PhoneBook.Services.AppSettingProperty;
using PhoneBook.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(option => 
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddScoped<IJwtTokenBuilder, JwtTokenBuilder>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UserApi",
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Books Api",
    });

    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        //Scheme = "Bearer",
        //In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/User/LoginSwagger", UriKind.Relative),
            }
        }
    });

});



//Method1

//Method 2
 



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("AppSetting:JwtSetting:Audience"),
        ValidIssuer = builder.Configuration.GetValue<string>("AppSetting:JwtSetting:Issued"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration
        .GetValue<string>("AppSetting:JwtSetting:Key")))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "User");
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
