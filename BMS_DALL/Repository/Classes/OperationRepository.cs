using BMS_DALL.Classes;
using BMS_DALL.DBContextFolder;
using BMS_DALL.DTOs.Responses;
using BMS_DALL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Classes
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationDb_Context _Context;

        public OperationRepository(ApplicationDb_Context applicationDb_Context)
        {
            _Context = applicationDb_Context;
        }

        public async Task<int> Transaction(Transaction transaction)
        {

            _Context.Transactions.Add(transaction);
            return await _Context.SaveChangesAsync();

        }

        public async Task<List<Transaction>> Transaction_History(string UserId)
        {
        var list=await _Context.Transactions.Where(p => p.UserId == UserId).ToListAsync();
            return list;

        }
    }
}
