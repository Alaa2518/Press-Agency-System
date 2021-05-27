using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Title")]
        [Display(Name = "Title")]
        public string ArticleTitle { get; set; }

        [Required(ErrorMessage = "Enter Body")]
        [Display(Name = "Body")]
        public string ArticleBody { get; set; }

        public string CreationDate { get; set; }

        [Required(ErrorMessage = "Enter type")]
        [Display(Name = "Type")]
        public string ArticleType { get; set; }
        public int NumberOfViewers { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public bool IfAproveed { get; set; }
        
        public virtual List<Photo> Photos { get; set; }
        
        public int ImageId { get; set; }

        
        public virtual List<Editor> Editors { get; set; }
        public int EditorId { get; set; }





    }
}