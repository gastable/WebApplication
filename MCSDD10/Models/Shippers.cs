using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class Shippers
    {
        [Key]  //主鍵設定int會變流水號，資料庫會自動編號
        [DisplayName("貨運編號")]        
        public int ShipID { get; set; }

        [DisplayName("貨運公司")]
        [Required(ErrorMessage = ("貨運公司為必填"))]
        [StringLength(100, ErrorMessage = "付款方式最多20個字")]
        public string ShipVia { get; set; }
    }
}