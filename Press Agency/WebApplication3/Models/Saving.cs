using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Saving
    {

        public int ID { get; set; }
        
        public virtual IEnumerable<Article> Article { get; set; }
       // [ForeignKey("Article")]
        public int PostId { get; set; }
    
        public virtual IEnumerable<Person> Person { get; set; }
      //  [ForeignKey("Person")]
        public int userId { get; set; }
    }
}