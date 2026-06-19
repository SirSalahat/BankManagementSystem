using BMS_DALL.DBContextFolder;
using BMS_DALL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Classes
{
    public class ManagementRepository : IManagementRepository
    {
        private readonly ApplicationDb_Context context;

        public ManagementRepository(ApplicationDb_Context _Context)
        {
            context = _Context;
        }
        public async Task<int> SaveChange()
        {
           return await context.SaveChangesAsync();
        }
    }
}
