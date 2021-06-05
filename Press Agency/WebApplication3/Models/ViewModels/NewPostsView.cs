using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit;

namespace WebApplication3.Models.ViewModels
{
    public class NewPostsView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Title")]
        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }

        [Required(ErrorMessage = "Enter Body")]
        [Display(Name = "Article")]
        public string ArticleBody { get; set; }
        

        [Required(ErrorMessage = "Enter type")]
        [Display(Name = "Type")]
        public string ArticleType { get; set; }
       
        public string image { get; set; }
    }
}