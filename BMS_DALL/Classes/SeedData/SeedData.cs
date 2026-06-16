using BMS_DALL.DBContextFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Classes.SeedData
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDb_Context _Context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(ApplicationDb_Context db_Context,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _Context = db_Context;
            _userManager = userManager;
           _roleManager = roleManager;
        }
        public async Task Migration()
        {
            if ((await _Context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _Context.Database.MigrateAsync();

            }
            await _Context.SaveChangesAsync();

        }

        public async Task UserData()
        {
            if (!await _Context.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Name = "Amer",
                    Email = "Abc123@gmail.com",
                    UserName = "AmeerShady",
                    PhoneNumber = "9967676555656"
                };

                var user2 = new ApplicationUser()
                {
                    Name = "Ahmad",
                    Email = "Abc1234@gmail.com",
                    UserName = "Ahmadshady",
                    PhoneNumber = "454537563388333"
                };

                var role1 = new IdentityRole()
                {
                    Name = "Admin"
                };
                var role2 = new IdentityRole()
                {
                    Name = "Customer"
                };
               if(!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(role1);
                    await _roleManager.CreateAsync(role2);
                }  
                
               await _userManager.CreateAsync(user1);
                await _userManager.CreateAsync(user2);

                await _userManager.AddPasswordAsync(user1,"@Abc1234");
                await _userManager.AddPasswordAsync(user2,"@Abc1234");   
           
                await _userManager.AddToRoleAsync(user1, role1.Name);
          
                await _userManager.AddToRoleAsync(user2, role2.Name);
            }
            await _Context.SaveChangesAsync();

        }
    

    }

}
