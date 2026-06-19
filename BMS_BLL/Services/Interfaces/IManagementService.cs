using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Interfaces
{
   public interface IManagementService
    {
        
        public Task<MessageResponse> AddNewRole(string RoleName);
        public Task<MessageResponse> Approvement(string RoleName);
        public Task<MessageResponse> Block(string UserId);
        public  Task<MessageResponse> AddEmployee(UserRequest request);
        public Task<IdentityResult> DeleteEmployee(string UserId);
        public Task<IdentityResult> UpdateEmployee(string UserId, UserRequest userRequest);
        public Task<List<UserResponse>> AllEmployee();




    }
}
