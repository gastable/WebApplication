using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class OrderDetails
    {
        [Key]
        [DisplayName("訂單編號")]
        [Column(Order =1)]  //複合主鍵一定要記得加(第1欄)        
        public string OrderID { get; set; }
        [Key]
        [DisplayName("產品編號")]
        [Column(Order = 2)]  //複合主鍵一定要記得加(第2欄)        
        public string ProductID { get; set; }
        [DisplayName("商品售價")]
        [Required(ErrorMessage = ("請填寫商品售價"))]
        [Range(0,short.MaxValue,ErrorMessage ="商品售價不可小於0")]
        public short UnitPrice { get; set; }//實際售價
        [DisplayName("數量")]
        [Required(ErrorMessage = ("請填寫商品數量"))]
        [Range(1, short.MaxValue, ErrorMessage = "商品數量不可小於0")]
        public short Quantity { get; set; }
        //拉關聯
        public virtual Products Products { get; set; }
        public virtual Orders Orders { get; set; }
    }
}