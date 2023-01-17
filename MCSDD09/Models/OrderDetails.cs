using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MCSDD09.Models
{
    public class OrderDetails
    {
        //使用外來鍵當主鍵 使用column組合(order表示排列的順序)
        [Key]
        [Column(Order = 1)]
        [DisplayName("訂單編號")]
        public string OrderID { get; set; }
        [Key]
        [Column(Order =2)]
        [DisplayName("商品編號")]
        public string ProductID { get; set; }
        
        //售價 配合產品資料表裡的屬性
        [DisplayName("商品售價")]
        [Required(ErrorMessage ="請填商品售價")]
        [Range(0,short.MaxValue,ErrorMessage ="單價不可低於0元")]
        public short UnitPrice { get; set; }
        
        //配合產品資料表UnitsInStock裡的屬性
        [DisplayName("數量")]
        [Required(ErrorMessage ="請選擇數量")]
        [Range(1, short.MaxValue, ErrorMessage = "數量不可低於1")]
        public short Quantity { get; set; }

        //拉關聯 因為已經寫在主鍵位置上面，所以直接拉關聯即可
        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}