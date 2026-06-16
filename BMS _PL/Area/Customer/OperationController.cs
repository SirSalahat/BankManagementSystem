using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BMS__PL.Area.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService operationService;
        private readonly UserManager<ApplicationUser> userManager;


        public OperationController(IOperationService operationService, UserManager<ApplicationUser> userManager)
        {
            this.operationService = operationService;
            this.userManager = userManager;
        }
        [HttpPut
            ("Deposit")]
     
        public async Task <IActionResult> Deposit(string UserId ,DepositRequest depositRequest)
        {
            return Ok(await operationService.Deposit(UserId, depositRequest));
        }


        [HttpPut("Withdraw/{UserId}")]
       
        public async Task<IActionResult> Withdraw([FromRoute]string UserId, DepositRequest depositRequest)
        {
          var result=  await operationService.Withdraw(UserId, depositRequest);
            return Ok(result);
        }



        [HttpPut("Transfer/{userId2}")]

        public async Task<IActionResult> Transfer( [FromRoute] string UserId2, DepositRequest depositRequest)
        {
           var user= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await operationService.Transfer(user,UserId2, depositRequest);
            return Ok(result);
        }

        [HttpGet("ViewAllDetails")]
      
        public async Task<IActionResult> Details(string UserId)
        {
            
            var result = await operationService.Details(UserId);
            return Ok(result);
        }
        [HttpGet("Transaction_History")]
  
        public async Task<ActionResult<TransactionResponse>> Transaction_History(string UserId)
        {

            var result = await operationService.Transaction_History(UserId);
            return  Ok(result);
        }

    }
}
