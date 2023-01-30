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

    public partial class SecTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecTypes()
        {
            this.Securities = new HashSet<Securities>();
        }

        [DisplayName("證券類別代碼")]
        [RegularExpression("[A-Z]{3}", ErrorMessage = "類別代碼格式錯誤")]
        [Required(ErrorMessage = "請填寫證券類別代碼")]
        [StringLength(3, ErrorMessage = "證券類別代碼不可超過3個字")]
        public string TypeID { get; set; }

        [DisplayName("證券類別名稱")]
        [Required(ErrorMessage = "請填寫證券類別名稱")]
        [StringLength(20, ErrorMessage = "證券類別名稱不可超過20個字")]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Securities> Securities { get; set; }
    }
}