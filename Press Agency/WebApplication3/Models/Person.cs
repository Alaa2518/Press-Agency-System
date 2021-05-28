using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter first Name")]
        [Display(Name = "FName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "LName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Phone Number")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your Role")]
        [Display(Name = "RleUser")]
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
        public int RoleUserID { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name= "Password")]
        public string Password { get; set; }

        
        public virtual List<Photo> Photos { get; set; }
        public int photoID { get; set; }









    }
}