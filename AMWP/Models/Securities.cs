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

    public partial class Securities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Securities()
        {
            this.Daily = new HashSet<Daily>();
            this.Monthly = new HashSet<Monthly>();
            this.SecOrders = new HashSet<SecOrders>();
            this.Weekly = new HashSet<Weekly>();
        }

        [DisplayName("證券代碼")]
        [RegularExpression("[A-Z][0-9]{5}", ErrorMessage = "證券代碼格式錯誤")]
        [Required(ErrorMessage = "請填寫證券代碼")]
        [StringLength(6, ErrorMessage = "證券代碼不可超過6個字")]
        public string SecID { get; set; }

        [DisplayName("證券類別")]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "類別代碼格式錯誤")]
        [Required(ErrorMessage = "請填寫證券類別代碼")]
        [StringLength(3, ErrorMessage = "證券類別代碼不可超過3個字")]
        public string TypeID { get; set; }

        [DisplayName("註冊國家")]
        [RegularExpression("[0-9]{2}", ErrorMessage = "格式錯誤，請輸入2碼數字")]
        [Required(ErrorMessage = "請輸入國家代碼")]
        [StringLength(2, ErrorMessage = "國家代碼不可超過2個數字")]
        public string CountryID { get; set; }

        [DisplayName("證券代號")]
        [Required(ErrorMessage = "請輸入證券代號")]
        [StringLength(20, ErrorMessage = "證券代號不可超過20個字")]
        public string Symbol { get; set; }

        [DisplayName("證券名稱")]
        [Required(ErrorMessage = "請輸入證券名稱")]
        [StringLength(100, ErrorMessage = "證券名稱不可超過100個字")]
        public string Name { get; set; }

        [DisplayName("計價幣別")]
        [Required(ErrorMessage = "請輸入計價幣別")]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "幣別格式錯誤")]
        [StringLength(3, ErrorMessage = "計價幣別不可超過3個字")]
        public string CCYID { get; set; }

        public virtual Countries Countries { get; set; }
        public virtual Currencies Currencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Daily> Daily { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Monthly> Monthly { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecOrders> SecOrders { get; set; }
        public virtual SecTypes SecTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Weekly> Weekly { get; set; }
    }
}
