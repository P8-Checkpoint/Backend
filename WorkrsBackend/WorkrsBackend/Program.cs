using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using WorkrsBackend.Config;
using WorkrsBackend.DataHandling;
using WorkrsBackend.RabbitMQ;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkrsBackend.DTOs;
using WorkrsBackend.Controllers;

namespace WorkrsBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            builder.Host.UseSerilog();
            // Add services to the container.

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IServerConfig, ServerConfig>();
            builder.Services.AddSingleton<IDataAccessHandler, DataAccessHandler>();
            builder.Services.AddSingleton<ISharedResourceHandler, SharedResourceHandler>();
            builder.Services.AddSingleton<IRabbitMQHandler, RabbitMQHandler>();
            //builder.Services.AddHostedService<ServiceLogic>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            //app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();
            //app.MapPost("/security/createToken",
            //[AllowAnonymous] (User user) =>
            //{
            //    if (user.UserName == "joydip" && user.Password == "joydip123")
            //    {
            //        var issuer = builder.Configuration["Jwt:Issuer"];
            //        var audience = builder.Configuration["Jwt:Audience"];
            //        var key = Encoding.ASCII.GetBytes
            //        (builder.Configuration["Jwt:Key"]);
            //        var tokenDescriptor = new SecurityTokenDescriptor
            //        {
            //            Subject = new ClaimsIdentity(new[]
            //            {
            //    new Claim("Id", Guid.NewGuid().ToString()),
            //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Jti,
            //    Guid.NewGuid().ToString())
            //             }),
            //            Expires = DateTime.UtcNow.AddMinutes(5),
            //            Issuer = issuer,
            //            Audience = audience,
            //            SigningCredentials = new SigningCredentials
            //            (new SymmetricSecurityKey(key),
            //            SecurityAlgorithms.HmacSha512Signature)
            //        };
            //        var tokenHandler = new JwtSecurityTokenHandler();
            //        var token = tokenHandler.CreateToken(tokenDescriptor);
            //        var jwtToken = tokenHandler.WriteToken(token);
            //        var stringToken = tokenHandler.WriteToken(token);
            //        return Results.Ok(stringToken);
            //    }
            //    return Results.Unauthorized();
            //});


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            Log.Information("Starting app");
            app.Run();

        }
    }
}