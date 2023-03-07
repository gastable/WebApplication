using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class TimeSeriesByYears
    {
        public List<string> Symbol { get; set; }
        public List<string> Date { get; set; }      

        public List<double> Close { get; set; }
        public List<double> AdjClose { get; set; }
        
    }
}