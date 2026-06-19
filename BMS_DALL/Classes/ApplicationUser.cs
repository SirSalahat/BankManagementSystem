using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BMS_DALL.Classes
{
   

    public class ApplicationUser: IdentityUser
    {
        public enum AccountType
        {
            Saving = 1, Current = 2,
        }
        public string Name { get; set; }
        public decimal? Balance { get; set; }
        public int? CardNumber { get; set; }
        public List<Transaction>? Transactions { get; set; }

     

        public AccountType? Account_Type { get; set; } 
        public bool IsBlocked { get; set; } = false;
        public decimal? Limit { get; set; }
        public string? Position{ get; set; }


    }
}
