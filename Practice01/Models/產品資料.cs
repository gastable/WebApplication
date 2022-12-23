//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class 產品資料
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public 產品資料()
        {
            this.訂貨明細 = new HashSet<訂貨明細>();
        }
    
        public int 產品編號 { get; set; }
        public string 產品 { get; set; }
        public Nullable<int> 供應商編號 { get; set; }
        public Nullable<int> 類別編號 { get; set; }
        public string 單位數量 { get; set; }
        public Nullable<decimal> 單價 { get; set; }
        public Nullable<short> 庫存量 { get; set; }
        public Nullable<short> 已訂購量 { get; set; }
        public Nullable<short> 安全存量 { get; set; }
        public bool 不再銷售 { get; set; }
    
        public virtual 供應商 供應商 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<訂貨明細> 訂貨明細 { get; set; }
        public virtual 產品類別 產品類別 { get; set; }
    }
}
