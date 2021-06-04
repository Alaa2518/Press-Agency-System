using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.ViewModels
{
    public class SavedPostes
    {
        public IEnumerable<Saving> saving { get; set; }
        public IEnumerable<Article> articles { get; set; }

    }
}