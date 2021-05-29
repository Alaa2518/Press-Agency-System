using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Photo
    {
        public int Id { get; set; }
        
        public string Title { get; set;}

        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Article Image")]
        public string Path { get; set; }

        //[ForeignKey("Article")]
        
        public virtual Article Aritcle { get; set; }
        public int AritclesID { get; set; }




    }
}