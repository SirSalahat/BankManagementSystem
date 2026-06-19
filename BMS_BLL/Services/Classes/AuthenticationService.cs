using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Classes
{
    public class AuthenticationService : BMS_BLL.Services.Interfaces.IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        public AuthenticationService(UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<TokenResponse> Login(BMS_DALL.DTOs.Requests.LoginRequest Request)
        {
          var user=await _usermanager.FindByEmailAsync(Request.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }
            if(await _usermanager.IsLockedOutAsync(user))
            {
                throw new Exception(" User Blocked");

            }

            if (user.LockoutEnd<DateTime.Now )
            {
                user.LockoutEnd = null;
                user.IsBlocked = false;
                await _usermanager.UpdateAsync(user);

            }
            var check= await _usermanager.CheckPasswordAsync(user, Request.Password);
            if (!check)
            {
                throw new Exception("Invalid email or password");
            }
            return new TokenResponse()
            {
                Token = await GenerateToken(user)
            };

        }

        public async Task<TokenResponse> Signup(BMS_DALL.DTOs.Requests.RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {

                Name = registerRequest.Name,
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                Account_Type = registerRequest.Account_Type

            };
            await _usermanager.AddPasswordAsync(user, registerRequest.Password);
            await _usermanager.CreateAsync(user);
            await _usermanager.AddToRoleAsync(user, "Customer");
            return new TokenResponse()
            {
                Token = await GenerateToken(user)
            };

        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Name, user.Name!),
    new Claim(ClaimTypes.Email, user.Email!),
    new Claim(ClaimTypes.UserData, user.UserName!)
};
            var roles = await _usermanager.GetRolesAsync(user);
            foreach (var x in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, x));

            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("69a16a0eda4e9b933ba421b9bd26d09761784be0a4132fec0d9e4b2478744fd0"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            claims: userClaims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
