using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.ViewModels
{
    public class PostesImagesView
    {
        public IEnumerable<Article> articles { get; set; }

        public IEnumerable<Photo> photos { get; set; }
    }
}