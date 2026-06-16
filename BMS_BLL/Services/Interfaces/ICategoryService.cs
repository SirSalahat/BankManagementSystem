using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_BLL.Services.Interfaces
{
   public interface ICategoryService:IGenericService<CategoryRequest, CategoryResponse, Category>
    {
    }
}
