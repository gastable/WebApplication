using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AMWP.ViewModels
{
    public class Backtest
    {
        [DisplayName("開始年度")]        
        [Required(ErrorMessage = "請選擇開始年度")]
        public int StartYear { get; set; }
        [DisplayName("結束年度")]       
        [Required(ErrorMessage = "請選擇結束年度")]
        public int EndYear { get; set; }

        [DisplayName("起始資金")]
        [DefaultValue(10000)]
        [Range(0,float.MaxValue)]
        public decimal InitAmount { get; set; }

        [DisplayName("證券#1")]
        [Required(ErrorMessage ="請輸入證券代號")]
        public string Asset1 { get; set; }


        public int Slice1 { get; set; }

        public int Slice2 { get; set; }

        public int Slice3 { get; set; }

        [DisplayName("證券#2")]
        public string Asset2 { get; set; }        

        public int Slice4 { get; set; }

        public int Slice5 { get; set; }

        public int Slice6 { get; set; }

        [DisplayName("證券#3")]
        public string Asset3 { get; set; }

        public int Slice7 { get; set; }
       
        public int Slice8 { get; set; }
      
        public int Slice9 { get; set; }


        [DisplayName("證券#4")]
        public string Asset4 { get; set; }
      
        public int Slice10 { get; set; }

        public int Slice11 { get; set; }

        public int Slice12 { get; set; }

        [DisplayName("證券#5")]
        public string Asset5 { get; set; }

        public int Slice13 { get; set; }

        public int Slice14 { get; set; }

        public int Slice15 { get; set; }
    }
}