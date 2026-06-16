using BMS_BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS__PL.Area.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize (Roles ="Admin")]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementService service;

        public ManagementController(IManagementService service)
        {
            this.service = service;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult>AddRole(string RoleName)
        {
           await service.AddNewRole(RoleName);
            return Ok(RoleName);
        }
    }
}
