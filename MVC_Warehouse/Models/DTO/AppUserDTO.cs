using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Warehouse.Models.DTO
{
    public class AppUserDTO:BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}