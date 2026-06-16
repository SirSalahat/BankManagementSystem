using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Interfaces
{
   public interface IAuthenticationService
    {

        public Task<TokenResponse> Login(BMS_DALL.DTOs.Requests.LoginRequest Request);
        public Task<TokenResponse> Signup(BMS_DALL.DTOs.Requests.RegisterRequest registerRequest);
    }
}
