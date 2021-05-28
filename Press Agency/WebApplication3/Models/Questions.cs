using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Questions
    {
        public int ID { get; set; }
        public string Ask { get; set; }
        public string Answer { get; set; }

        public virtual IEnumerable<Person> Editor { get; set; }
        public int EditorAnswerId { get; set; }

        public bool IsAnswer { get; set; }


    }
}