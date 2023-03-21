using AMWP.Models;
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
        [Range(0,double.MaxValue)]
        public double InitAmount { get; set; }
        
        [DisplayName("證券#1")]        
        public string Symbol1 { get; set; }

        public int Slice1 { get; set; }

        public int Slice2 { get; set; }

        public int Slice3 { get; set; }

        [DisplayName("證券#2")]
        public string Symbol2 { get; set; }        

        public int Slice4 { get; set; }

        public int Slice5 { get; set; }

        public int Slice6 { get; set; }

        [DisplayName("證券#3")]
        public string Symbol3 { get; set; }

        public int Slice7 { get; set; }
       
        public int Slice8 { get; set; }
      
        public int Slice9 { get; set; }


        [DisplayName("證券#4")]
        public string Symbol4 { get; set; }
      
        public int Slice10 { get; set; }

        public int Slice11 { get; set; }

        public int Slice12 { get; set; }

        [DisplayName("證券#5")]
        public string Symbol5 { get; set; }

        public int Slice13 { get; set; }

        public int Slice14 { get; set; }

        public int Slice15 { get; set; }
    }
}