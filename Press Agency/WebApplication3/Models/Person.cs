﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Phone Number")]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your Role")]
        [Display(Name = "RleUser")]
        //[ForeignKey("UserRole")]
        
        public virtual IEnumerable<UserRole> UserRole { get; set; }
        public int RoleUserID { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name= "Password")]
        public string Password { get; set; }

        


       
        [DataType(DataType.ImageUrl)]
        
        public string Image { get; set; }









    }
}