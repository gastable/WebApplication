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

    public partial class Members
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Members()
        {
            this.Cash = new HashSet<Cash>();
            this.Managements = new HashSet<Managements>();
            this.Properties = new HashSet<Properties>();
            this.SecOrders = new HashSet<SecOrders>();
        }

        [DisplayName("會員代碼")]
        public int MemID { get; set; }

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "請輸入會員帳號")]
        [EmailAddress(ErrorMessage = "帳號請輸入電子郵件信箱")]
        public string Account { get; set; }

        string password;
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        //[MinLength(8, ErrorMessage = "密碼最少8碼")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = BusinessRules.getHashPassword(value);
            }
        }

        [DisplayName("會員姓名")]
        [StringLength(50, ErrorMessage = "姓名不可超過50個字")]
        [Required(ErrorMessage = "請填寫會員姓名")]
        public string Name { get; set; }

        [DisplayName("加入日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedDate { get; set; }

        [DisplayName("帳號狀態")]
        [DefaultValue(true)]
        public bool Status { get; set; }

        [DisplayName("預設幣別")]
        [Required(ErrorMessage = "請輸入預設幣別")]        
        public string CCYID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cash> Cash { get; set; }
        public virtual Currencies Currencies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Managements> Managements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Properties> Properties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecOrders> SecOrders { get; set; }
    }
}
