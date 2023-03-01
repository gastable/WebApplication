using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace AMWP.Models
{
    public class AlphaVantageData
    {
        
        public DateTime Timestamp { get; set; }
        public double Open { get; set; }

        public double High { get; set; }
        public double Low { get; set; }

        public double Close { get; set; }

        public double Adjusted_Close { get; set; }

        public double Dividend_Amount { get; set; }


        public double Volume { get; set; }
    }
}