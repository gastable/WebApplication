using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class Orders
    {
        [Key]
        [DisplayName("訂單編號")]
        [StringLength(11)]  //不用設定格式，用資料庫程式自動取號
        public string OrderID { get; set; }

        [DisplayName("訂單成立時間")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime OrderDate { get; set; }

        [DisplayName("收件人姓名")]
        [Required(ErrorMessage = ("請填寫收件人姓名"))]
        [StringLength(40,ErrorMessage ="收件人姓名最多40個字")]
        public string ShipName { get; set; }

        [DisplayName("收件人住址")]
        [Required(ErrorMessage = ("請填寫收件人住址"))]
        [StringLength(100, ErrorMessage = "收件人姓名最多100個字")]

        public string ShipAddress { get; set; }

        [DisplayName("出貨日期")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> ShippedDate { get; set; } //可為null值的泛型物件


        //Foreign Key資料型態不用再輸入一次，最多確定是否要 [Required]
        [Required]
        public int MemberID { get; set; }
        
        public int EmployeeID { get; set; }

        
        public int ShipID { get; set; }

        [Required]
        public int PayTypeID { get; set; }

        //Foreign Key 記得去其他資料表複製，比較不會打錯，造成無法關聯
        //拉關聯
        public virtual Employees Employees { get; set; }
        public virtual Members Members { get; set; }
        public virtual Shippers Shippers { get; set; }
        public virtual PayTypes PayTypes { get; set; }

    }
}