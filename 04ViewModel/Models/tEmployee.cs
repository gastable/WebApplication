//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _04ViewModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tEmployee
    {
        [DisplayName("員工代碼")]
        [Required(ErrorMessage ="員工代碼為必填")]
        [RegularExpression("[A-FS-W][0-9]{2}", ErrorMessage = "員工代碼格式錯誤")]//如果大小寫都接受，第一碼的規則變成[A-FS-Wa-fs-w]
        [Key]//標註那個欄位是主鍵，但是code first用的，不能拿來驗證是否重覆
        public string fEmpId { get; set; }
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名為必填")]
        public string fName { get; set; }
        [DisplayName("電話")]
        public string fPhone { get; set; }
        [DisplayName("部門代碼")]
        public Nullable<int> fDepId { get; set; }
    }
}
