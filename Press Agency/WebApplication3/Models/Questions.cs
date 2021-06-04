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
        public string Qustion { get; set; }
        public string Answer { get; set; }

        public Person Editor { get; set; }
        public int Editor_Id { get; set; }
        public int Answer_Id { get; set; }

        public bool IsAnswer { get; set; }
        
    }
}