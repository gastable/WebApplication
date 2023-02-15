using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class Chart
    {
        public List<string> labels { get; set; }
        public List<decimal> data { get; set; }
    }
    
}