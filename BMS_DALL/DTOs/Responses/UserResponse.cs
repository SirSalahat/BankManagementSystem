using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.DTOs.Responses
{
   public class UserResponse
    {
      
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Balance { get; set; }
        public string? Position { get; set; }
    }
}
