using BMS_DALL.Classes;
using BMS_DALL.DBContextFolder;
using BMS_DALL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Repository.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDb_Context context): base(context)
        {
            
        }
    }
}
