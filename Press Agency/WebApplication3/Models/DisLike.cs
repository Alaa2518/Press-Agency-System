using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DisLike
    {
        public int Id { get; set; }

        public Person person { get; set; }
        public int per_ID { get; set; }

        public Article article { get; set; }
        public int art_ID { get; set; }
    }
}