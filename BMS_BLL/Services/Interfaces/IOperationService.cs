using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Interfaces
{
   public interface IOperationService
    {
        public Task<DepositResponse> Deposit(string UserId, DepositRequest depositRequest);
        public Task<DepositResponse> Withdraw(string UserId, DepositRequest depositRequest);
        public Task<MessageResponse> Transfer(string UserId, string UserId2, DepositRequest depositRequest);
        public Task<UserResponse> Details(string UserId);
        public Task<TransactionResponse> Transaction_History(string UserId);


    }
}
