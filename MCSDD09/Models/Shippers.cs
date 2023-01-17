using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCSDD09.Models
{
    public class Shippers
    {
        [Key]
        public int ShipID { get; set; }
        
        [DisplayName("運送方式")]
        [Required(ErrorMessage ="請選擇運輸方式")]
        public string ShipVia { get; set; }
    }
}