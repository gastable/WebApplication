using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MCSDD09.Models
{
    public class Products
    {
        [Key]
        [DisplayName("商品編號")]
        [StringLength(6)]
        [RegularExpression("[A-F][0-9]{5}")]
        public string ProductID { get; set; }
        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        [StringLength(150, ErrorMessage = "商品名稱以40字為限")]
        public string ProductName { get; set; }
        //會把圖轉成二進位模式儲存
        [DisplayName("商品照片")]
        [Required(ErrorMessage = "請上傳照片")]
        public byte[] PhotoFile { get; set; }
        //儲存圖片型態for還原
        [DisplayName("照片類型")]
        [Required]
        [StringLength(10)]
        public string ImageMimeType { get; set; }
        //定價
        [DisplayName("商品定價")]
        [Required(ErrorMessage = "請輸入金額")]
        [Range(0, short.MaxValue, ErrorMessage = "單價不可以小於0")]
        [DisplayFormat(DataFormatString ="{0:C0}",ApplyFormatInEditMode =true)]
        //C的顯示為$0.00，如果要顯示整數C0
        public short UnitPrice { get; set; }
        [DisplayName("商品描述")]
        [Required(ErrorMessage ="請輸入商品描述")]
        [StringLength(1000,ErrorMessage ="商品描述不可超過1000字")]
        public string Description { get; set; }

        [DisplayName("商品庫存量")]
        [Required(ErrorMessage ="請輸入商品庫存量")]
        [Range(0, short.MaxValue, ErrorMessage = "庫存量不可以小於0")]
        public short UnitsInStock { get; set; }
        [DisplayName("已下架")]
        [DefaultValue(false)]
        //預設商品在架上
        public bool Discontinued { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}