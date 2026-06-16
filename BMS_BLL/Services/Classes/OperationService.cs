using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using BMS_DALL.Migrations;
using BMS_DALL.Repository.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Classes
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public OperationService(IOperationRepository repository, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }
        public async Task<DepositResponse> Deposit(string UserId, DepositRequest depositRequest)
        {
            var finduser = await userManager.FindByIdAsync(UserId);
            if (finduser == null) {
                throw new Exception("User not found");
            }
            finduser.Balance += depositRequest.Balance;

            var update = new ApplicationUser()
            {
                Balance = finduser.Balance,
            };

            await userManager.UpdateAsync(finduser);
            await Transction(UserId, depositRequest);

            return new DepositResponse
            {
                Balance = update.Balance,
            };

        }


        public async Task<DepositResponse> Withdraw(string UserId, DepositRequest depositRequest)
        {

            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (depositRequest.Balance > user.Balance)
            {
                throw new Exception("Insufficient balance");
            }
            await WithdrawLimit(UserId);

            user.Balance -= depositRequest.Balance;
            user.Limit += depositRequest.Balance;
            await userManager.UpdateAsync(user);
            await Transction(user.Email, depositRequest);
            return new DepositResponse
            {
                Balance = user.Balance,

            };
        }

        public async Task<MessageResponse> Transfer(string UserId, string UserId2, DepositRequest depositRequest)
        {
            var user1 = await userManager.FindByIdAsync(UserId);
            var user2 = await userManager.FindByIdAsync(UserId2);
            if (user1 == null || user2 == null)
            {
                throw new Exception("User Not Found");
            }
            if (depositRequest.Balance > user1.Balance)
            {
                throw new Exception("Insufficient balance");
            }
            user1.Balance = user1.Balance - depositRequest.Balance;
            user2.Balance = user2.Balance + depositRequest.Balance;
            await userManager.UpdateAsync(user1);
            await userManager.UpdateAsync(user2);
            await Transction(UserId, depositRequest);
            return new MessageResponse
            {
                Message = "Transfer completed successfully"
            };
        }

        public async Task<UserResponse> Details(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            return new UserResponse()
            {
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Balance = user.Balance
            };

        }
        public async Task<TransactionResponse> Transaction_History(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var transactions = await repository.Transaction_History(UserId);

            return new TransactionResponse
            {
                Transactions = transactions //This assigns your transaction list
            };
        }
        public async Task<MessageResponse> Transction(string Email, DepositRequest depositRequest)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                throw new Exception("not found");
            }

            var User = new Transaction()
            {
                UserId = user.Id,
                Balance = depositRequest.Balance,
                Name = user.Name,
            };
            await repository.Transaction(User);
            return new MessageResponse()
            {
                Message = "Transaction Completed Successfully"
            };
        }

        public async Task<MessageResponse> WithdrawLimit(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            if (user.Account_Type == ApplicationUser.AccountType.Saving)
            {
                await Limit(UserId);

            }
            return new MessageResponse()
            {
                Message = "Done"
            };
        }
        public async Task<MessageResponse> Limit(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            if (user.Limit >= 10000)
            {
                throw new Exception("You have reached the maximum limit for withdrawals.");
            }
            return new MessageResponse()
            {
                Message = "Done"
            };

        }

    }
    }

