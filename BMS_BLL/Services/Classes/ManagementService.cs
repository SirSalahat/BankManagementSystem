using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Classes
{
    public class ManagementService : IManagementService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ManagementService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager)
        {
            this.roleManager = roleManager;
            this.userManager = UserManager;
        }
        public async Task<MessageResponse> AddNewRole(string RoleName)
        {
            var role= new IdentityRole()
            {
                Name = RoleName,
                
            };
            
          await roleManager.CreateAsync(role);
            
            return new MessageResponse
            {
                Message = "Role added successfully"
            };
        }

        public Task<MessageResponse> Approvement(string RoleName)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageResponse> Block(string UserId)
        {
            var user=await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            user.LockoutEnd=DateTime.Now.AddMinutes(5);  
            user.IsBlocked = true;
            user.LockoutEnabled = true;
            await userManager.UpdateAsync(user);
         
            return new MessageResponse
            {
                Message = "User blocked successfully"
            };

        }
    }
}
