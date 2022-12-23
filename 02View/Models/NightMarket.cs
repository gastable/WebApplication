using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace _02View.Models
{
    public class NightMarket
    {
        [DisplayName("夜市編號")]
        [Required(ErrorMessage ="夜市編號為必填")]
        [RegularExpression("[a-f][0-9]{3}")]
        public string Id { get; set; }
        [DisplayName("夜市名稱")]
        [Required(ErrorMessage = "夜市名稱為必填")]
        public string Name { get; set; }
        [DisplayName("夜市住址")]
        [Required(ErrorMessage = "住址為必填")]
        public string Address { get; set; } 
    }
}