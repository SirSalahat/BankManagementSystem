using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BMS__PL.Area.Admin
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementService service;
        private readonly UserManager<ApplicationUser> userManager;

        public ManagementController(IManagementService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string RoleName)
        {
            await service.AddNewRole(RoleName);
            return Ok(RoleName);
        }
        [AllowAnonymous]
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<MessageResponse>> AddEmployee(UserRequest applicationUser)
        {
            return await service.AddEmployee(applicationUser);

        }
        [AllowAnonymous]

        [HttpPost("Delete/{UserId}")]
        public async Task<ActionResult> DeleteEmployee(string UserId)
        {
            await service.DeleteEmployee(UserId);
            return Ok();
        }


        /*  [HttpPatch("Update/{UserId}")]
          public async Task<ActionResult> UpdateEmployee(string UserId,[FromBody] JsonPatchDocument<ApplicationUser> jsonPatchDocument)
          {
              var user = await userManager.FindByIdAsync(UserId);
              jsonPatchDocument.ApplyTo(user);
              await service.UpdateEmployee(UserId);
              return Ok();

          }*/

        /// <summary>
        /// Updates full employee's information using Put
        /// </summary>
        /// <param name="UserId"></param>

        /// <returns></returns>

        
        [HttpPut("Update/{UserId}")]
        public async Task<ActionResult> UpdateEmployee(string UserId,[FromBody]UserRequest userRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await service.UpdateEmployee(UserId,userRequest);
            return Ok();

        }

        [HttpGet("AllEmployee")]
        public async Task<List<EmployeeResponse>> AllEmployee()
        {
            return await service.AllEmployee();
           
        }
    }
}
