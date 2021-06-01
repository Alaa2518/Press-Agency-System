using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Like
    {
        public int Id { get; set; }

        public Person person { get; set; }
        public int person_ID { get; set; }

        public Article article { get; set; }
        public int ART_ID { get; set; }
    }
}