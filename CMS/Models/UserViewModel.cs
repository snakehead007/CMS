using CMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class UserViewModel
    {
        public string userName { get; set; }
        public UserRole role { get; set; }
        public UserRole[] roles = new UserRole[] { UserRole.Admin, UserRole.Lector, UserRole.Student };
    }
}