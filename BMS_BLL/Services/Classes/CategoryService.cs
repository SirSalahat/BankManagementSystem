using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using BMS_DALL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Classes
{
   public class CategoryService : GenericService<CategoryRequest,CategoryResponse,Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository repo):base(repo)
        {
            
        }
    }
}
