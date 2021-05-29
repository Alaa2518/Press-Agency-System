using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.ViewModels
{
    public class PersonRoleViewModel
    {
        public Person Person { get; set; }
        public IEnumerable<UserRole> userRoles { get; set; }
    }
}