using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL.DataAccess;
using Shop.DAL.Repositories;
using Shop.Tools;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Authentification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("tokenInfo").GetSection("secretKey").Value)),
            ValidateLifetime = true,
            ValidateIssuer = false,
            //ValidateAudience = false,
            ValidAudience = "http://localhost:4200"
        };
    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("connectedPolicy", policy => policy.RequireAuthenticatedUser());
});

#endregion

builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(configuration.GetConnectionString("home")));

builder.Services.AddScoped<JwtGenerator>();

builder.Services.AddScoped<IArticleRepository,ArticleService>();
builder.Services.AddScoped<IArticleBLLRepository,ArticleBLLService>();

builder.Services.AddScoped<IUserRepository,UserService>();
builder.Services.AddScoped<IUserBLLRepository,UserBLLService>();

builder.Services.AddScoped<IBasketRepository,BasketService>();
builder.Services.AddScoped<IBasketBLLRepository,BasketBLLService>();

builder.Services.AddCors(o => o.AddPolicy("front", options =>
    options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithExposedHeaders("Content-Disposition")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("front");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
