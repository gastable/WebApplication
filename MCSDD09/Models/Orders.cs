using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCSDD09.Models
{
    public class Orders
    {
        [Key]
        [DisplayName("訂單編號")]
        //自動取號
        [StringLength(12)]
        public string OrderID { get; set; }
        
        [DisplayName("訂單成立日期")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType (DataType.DateTime)]
        [Required]
        public DateTime OrderDate { get; set; }
        
        [DisplayName("收貨人姓名")]
        [Required(ErrorMessage ="請輸入收貨人姓名")]
        [StringLength(40,ErrorMessage ="姓名最多40字")]
        public string ShipName { get; set; }
        
        [DisplayName("收貨地址")]
        [Required(ErrorMessage ="請輸入收貨地址")]
        [StringLength(200,ErrorMessage ="收件地址以200字為限")]
        public string ShipAddress { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [DisplayName("出貨日期")]
        public Nullable<DateTime> ShippedDate { get; set; }

        //foreign key 全部到原model複製
        
        public int EmployeeID { get; set; }
        [Required]
        public int MemberID { get; set; }
        public int ShipID { get; set; }
        [Required]
        public int PayTypeID { get; set; }
        //拉關聯 使用虛擬屬性
                      //類別     鑄造出來的屬性  封裝屬性
        public virtual Employees Employees { get; set; }
        public virtual Members Members { get; set; }
        public virtual Shippers Shippers { get; set; }
        public virtual PayTypes PayTypes { get; set; }
    
    }
}