using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DALL.Classes.SeedData
{
    public interface ISeedData
    {
        public Task Migration();
         public Task UserData();
    }
}
