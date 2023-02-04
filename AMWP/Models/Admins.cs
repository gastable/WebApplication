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

    public partial class Admins
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admins()
        {
            this.Managements = new HashSet<Managements>();
        }

        [DisplayName("管理員代碼")]
        [Required(ErrorMessage = "請輸入管理員代碼")]
        [RegularExpression("[A-C][0-9]{3}", ErrorMessage = "管理員代碼格式錯誤")]
        public string AdminID { get; set; }

        [DisplayName("管理員帳號")]
        [Required(ErrorMessage = "請輸入管理員帳號")]
        [StringLength(20, ErrorMessage = "帳號不得超過20個字")]
        [RegularExpression("[A-Za-z0-9]{5,20}", ErrorMessage = "帳號格式錯誤")]
        public string Account { get; set; }

        [DisplayName("管理員姓名")]
        [StringLength(50, ErrorMessage = "姓名不可超過50個字")]
        [Required(ErrorMessage = "請填寫管理員姓名")]
        public string Name { get; set; }        

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Managements> Managements { get; set; }
    }
}
