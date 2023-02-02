using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _06ADOnet.Models
{
    public class VMLogin
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage ="請填寫帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}