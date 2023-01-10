using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCSDD10.Models
{
    public class PayTypes
    {
        [Key]
        [DisplayName("付款代碼")]        
        public int PayTypeID { get; set; }

        [DisplayName("付款方式")]
        [Required(ErrorMessage = ("付款方式為必填"))]
        [StringLength(20,ErrorMessage ="付款方式最多20個字")]
        public string PayTypeName { get; set; }
    }
}