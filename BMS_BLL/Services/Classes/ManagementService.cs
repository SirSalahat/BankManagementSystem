using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using BMS_DALL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IManagementRepository repository;

        public ManagementService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> UserManager,IManagementRepository repository)
        {
            this.roleManager = roleManager;
            this.userManager = UserManager;
            this.repository = repository;
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

        public async Task<MessageResponse> AddEmployee(UserRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                try
                {
                    var Employee = new ApplicationUser()
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Position = request.Position,
                        UserName = request.UserName,
                        PhoneNumber = request.PhoneNumber,
                    };
                    await userManager.CreateAsync(Employee);
                    await userManager.AddPasswordAsync(Employee, request.Password);
                    await userManager.AddToRoleAsync(Employee, "Employee");



                    return new MessageResponse
                    {
                        Message = "Employee added successfully"
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding employee: " + ex.Message);
                }
            }
            else
            {
                throw new Exception("Employee with this email already exists");
            }

        }
        public async Task<IdentityResult> DeleteEmployee(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user==null)
            {
                throw new Exception("User not found ");
            }
           return await userManager.DeleteAsync(user);

        }

        public async Task<IdentityResult> UpdateEmployee(string UserId, UserRequest userRequest)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            userRequest.Adapt(user);
            return  await userManager.UpdateAsync(user);

        }

        public async Task<List<UserResponse>> AllEmployee()
        {
            var Employees = await userManager.GetUsersInRoleAsync("Employee");
            List<UserResponse> EmployeeList = new List<UserResponse>(); 
            foreach (var employee in Employees)
            {
                var select=new UserResponse
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    UserName = employee.UserName,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                    Balance = employee.Balance

                };
                EmployeeList.Add(select);
            }
            return EmployeeList.ToList();

        }
    }
}
