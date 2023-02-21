//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMWP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class SecOrders
    {
        public long SSN { get; set; }

        [DisplayName("會員代碼")]
        public int MemID { get; set; }

        [DisplayName("證券代碼")]
        [RegularExpression("[A-Z][0-9]{5}", ErrorMessage = "證券代碼格式錯誤")]
        [Required(ErrorMessage = "請填寫證券代碼")]
        [StringLength(6, ErrorMessage = "證券代碼不可超過6個字")]
        public string SecID { get; set; }

        [DisplayName("交易類別")]
        [Required(ErrorMessage = "請輸入交易類別")]
        public bool TrancType { get; set; }

        [DisplayName("交易日期")]
        [Required(ErrorMessage = "請輸入交易日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date, ErrorMessage = "日期格式錯誤")]
        public System.DateTime Date { get; set; }

        [DisplayName("交易股數")]
        [Required(ErrorMessage = "請輸入交易股數")]
        [Range(0, double.MaxValue, ErrorMessage = "交易股數不可小於0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Share { get; set; }

        [DisplayName("交易價格")]
        [Required(ErrorMessage = "請輸入交易價格")]
        [Range(0, double.MaxValue, ErrorMessage = "交易價格不可小於0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Price { get; set; }

        [DisplayName("手續費")]
        [Range(0, double.MaxValue, ErrorMessage = "手續費不可小於0")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Fee { get; set; }

        public virtual Members Members { get; set; }
        public virtual Securities Securities { get; set; }
    }
}
