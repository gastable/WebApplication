using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class Products
    {
        [Key]
        [DisplayName("產品編號")]
        [StringLength(6)]
        [RegularExpression("[A-Z][0-9]{5}")]
        public string ProductID { get; set; }

        [DisplayName("產品名稱")]
        [Required(ErrorMessage = ("請填寫產品名稱"))]
        [StringLength(100, ErrorMessage = "產品名稱不可超過100個字")]
        public string ProductName { get; set; }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [DisplayName("產品照片")]
        [Required(ErrorMessage = ("請上傳產品照片"))]
        public byte[] PhotoFile { get; set; }  //直接存圖片的二進位資料

        [DisplayName("照片類型")]
        [StringLength(10)]
        [Required]
        public string ImageMimeType { get; set; }  //圖檔類型

        [DisplayName("單價")]
        [Required(ErrorMessage = ("請填寫單價"))]
        [Range(0,short.MaxValue,ErrorMessage ="單價不可小於0")]
        [DisplayFormat(DataFormatString ="{0:C0}",ApplyFormatInEditMode =true)]
        public short UnitPrice { get; set; }  //老師要求用短整數

        [DisplayName("產品說明")]
        [Required(ErrorMessage = ("請填寫商品說明"))]
        [StringLength(1000, ErrorMessage = "產品名稱不可超過1000個字")]
        public string Description { get; set; }

        [DisplayName("庫存量")]
        [Required(ErrorMessage = ("請填寫庫存量"))]
        [Range(0, short.MaxValue, ErrorMessage = "庫存量不可小於0")]
        public short UnitsInStock { get; set; }

        [DisplayName("已下架")]
        [DefaultValue(false)]  //設定預設值
        public bool Discontinued { get; set; }
    }
}