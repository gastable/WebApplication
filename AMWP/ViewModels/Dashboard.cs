using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMWP.ViewModels
{
    public class Dashboard
    {
        public List<Cash> cash { get; set; }
        public List<Countries> countries { get; set; }
        public List<Currencies> currencies { get; set; }
        public List<Daily> daily { get; set; }
        public List<Members> members { get; set; }
        public List<Monthly> monthly { get; set; }
        public List<Properties> properties { get; set; }
        public List<Securities> securities { get; set; }
        public List<SecTypes> secTypes { get; set; }
        public List<SecOrders> secOrders { get; set; }
        public List<Weekly> weekly { get; set; }
    }
}