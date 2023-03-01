using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace AMWP.Models
{
    public class AlphaVantageDataWM
    {
        
        public DateTime Timestamp { get; set; }
        public double Open { get; set; }

        public double High { get; set; }
        public double Low { get; set; }

        public double Close { get; set; }

        [DataMember(Name = "Adjusted Close")]
        public double Adjusted_Close { get; set; }

        [DataMember(Name = "Dividend Amoun")]
        public double Dividend_Amount { get; set; }


        public double Volume { get; set; }
    }
}