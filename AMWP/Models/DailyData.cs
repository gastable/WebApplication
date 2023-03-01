using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class DailyData
    {
        public List<string> SecID { get; set; }
        public List<string> Date { get; set; }

        public List<double> Open { get; set; }
        public List<double> High { get; set; }

        public List<double> Low { get; set; }

        public List<double> Close { get; set; }
        public List<double> AdjClose { get; set; }

        public List<double> Dividend { get; set; }
        public List<long> Volume { get; set; }


    }
}