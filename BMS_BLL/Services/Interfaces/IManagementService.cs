using BMS_DALL.DTOs.Responses;
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


    }
}
