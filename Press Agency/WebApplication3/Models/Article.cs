using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Article")]
        public string ArticleBody { get; set; }
        [DataType(DataType.DateTime)]
        public string CreationDate { get; set; }

        [Required(ErrorMessage = "Enter type")]
        [Display(Name = "Type")]
        public string ArticleType { get; set; }
        public int NumberOfViewers { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public bool IfAproveed { get; set; }


        
        public virtual IEnumerable<Person> Person { get; set; }
        public int EditorId { get; set; }




    }
}