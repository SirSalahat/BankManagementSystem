using BMS_DALL.Classes;
using BMS_DALL.DTOs.Responses;
using BMS_DALL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Interfaces
{
   public interface IOperationRepository
    {
        public Task<int> Transaction(Transaction transaction);
        public Task<List<Transaction>> Transaction_History(string UserId);

    }
}
