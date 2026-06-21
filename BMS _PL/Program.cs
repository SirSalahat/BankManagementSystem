
using BMS_BLL.Services.Classes;
using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.Classes.SeedData;
using BMS_DALL.DBContextFolder;
using BMS_DALL.Repository.Classes;
using BMS_DALL.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

namespace BMS__PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddOpenApi("v1");
            // Add services to the container.
         
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddDbContext<ApplicationDb_Context>(x => x.UseSqlServer(
            builder.Configuration.GetConnectionString("MyConnection")
    )); 
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); 
            builder.Services.AddScoped<IOperationRepository, OperationRepository>();
            builder.Services.AddScoped<IManagementRepository, ManagementRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IOperationService, OperationService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped< AuthenticationService>();
            builder.Services.AddScoped<IManagementService, ManagementService>();
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>

            option.SignIn.RequireConfirmedAccount = true
            ).AddEntityFrameworkStores<ApplicationDb_Context>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("69a16a0eda4e9b933ba421b9bd26d09761784be0a4132fec0d9e4b2478744fd0"))
            };
        });
            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi("v1");
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapOpenApi();
            app.MapScalarApiReference();      
            var scope = app.Services.CreateScope();
            var objectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objectOfSeedData.UserData();
           await objectOfSeedData.Migration();
            app.Run();
        }
    }
}
