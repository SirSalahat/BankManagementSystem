using BMS_DALL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BMS_DALL.Classes.ApplicationUser;

namespace BMS_DALL.DTOs.Requests
{
  
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountType Account_Type { get; set; }
    }
}
