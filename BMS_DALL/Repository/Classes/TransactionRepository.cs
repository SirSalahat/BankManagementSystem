using BMS_DALL.DBContextFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Classes
{
   public class TransactionRepository
    {
        private readonly ApplicationDb_Context _Context;
        public TransactionRepository(ApplicationDb_Context db_Context)
        {
            _Context = db_Context;
        }
        
    }
}
