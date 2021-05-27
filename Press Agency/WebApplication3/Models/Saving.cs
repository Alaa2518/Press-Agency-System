using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Saving
    {
        public int ID { get; set; }

        
        public virtual List<Article> Articles { get; set; }
        public int PostId { get; set; }
        
        public virtual List<User> Users { get; set; }
        public int userId { get; set; }
    }
}