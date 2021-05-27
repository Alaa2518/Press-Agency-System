using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Editor:Person
    {

        public List<Questions> questions { get; set; }
        public int QId { get; set; }
    }
}