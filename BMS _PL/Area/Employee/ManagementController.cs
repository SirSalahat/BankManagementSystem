using BMS_BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BMS__PL.Area.Employee
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Employee")]

    public class ManagementController : ControllerBase
    {
        private readonly IManagementService _service;
        private readonly IEmailService emailService;

        public ManagementController( IManagementService service,IEmailService emailService)
        {
            _service = service;
            this.emailService = emailService;
        }
        [AllowAnonymous]
        [HttpPut("BlockUser")]
        public async Task<IActionResult> BlockUser(string UserId)
        {
            try
            {
                await emailService.SendEmailAsync("ReceiverEmail@gmail.com", "Accoutn Blocked", "Congratulation , your account has been blocked");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("INNER:");
                    Console.WriteLine(ex.InnerException.ToString());
                }
                throw new Exception(ex.ToString());

            }
            
            await _service.Block(UserId);
            return Ok();
        }
    }
}
