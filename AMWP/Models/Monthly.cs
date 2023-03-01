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

    public partial class Monthly
    {
        public long SSN { get; set; }

        [DisplayName("證券代碼")]
        public string SecID { get; set; }

        [DisplayName("交易日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }

        [DisplayName("開盤價")]
        public double Open { get; set; }

        [DisplayName("最高價")]
        public double High { get; set; }

        [DisplayName("最低價")]
        public double Low { get; set; }

        [DisplayName("收盤價")]
        public double Close { get; set; }

        [DisplayName("調整收盤價")]
        public double AdjClose { get; set; }

        [DisplayName("配息")]
        public double Dividend { get; set; }

        [DisplayName("成交量")]
        public long Volume { get; set; }

        public virtual Securities Securities { get; set; }
    }
}
