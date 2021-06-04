using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class NumberOfViews
    {
        public int id { get; set; }

        public int article_Id { get; set; }
        public Article article { get; set; }

        public int person_Id { get; set; }

        public Person person { get; set; }


    }
}