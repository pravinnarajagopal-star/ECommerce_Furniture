using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using Yarp.ReverseProxy;


var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddReverseProxy()
.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]))
        };
});

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "API Gateway",
            Version = "v1"
        });
});


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Gateway");

    

    c.SwaggerEndpoint(
        "/customer/swagger/v1/swagger.json",
        "Customer Service");

    c.SwaggerEndpoint(
        "/product/swagger/v1/swagger.json",
        "Product Service");

    c.SwaggerEndpoint(
        "/order/swagger/v1/swagger.json",
        "Order Service");

   

    c.SwaggerEndpoint(
        "/payment/swagger/v1/swagger.json",
        "Payment Service");
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();