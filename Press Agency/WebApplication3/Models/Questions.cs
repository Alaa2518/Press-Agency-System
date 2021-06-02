using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Questions
    {
        public int ID { get; set; }
        public string Ask { get; set; }
        public string Answer { get; set; }

        public virtual Person Editor { get; set; }
        
        public bool IsAnswer { get; set; }
        public int Editor_Id { get; set; }

    }
}