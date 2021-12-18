
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.EF.Entities
{
    public class User:IdentityUser
    {
        public string Location { get; set; }

        public int? DeviceId { get; set; }
        public Device Device { get; set; }


        
    }
}
