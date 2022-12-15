using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace _02View.Models
{
    public class NightMarket
    {
        [DisplayName("夜市編號")]
        public string Id { get; set; }
        [DisplayName("夜市名稱")]
        public string Name { get; set; }
        [DisplayName("夜市住址")]
        public string Address { get; set; } 
    }
}