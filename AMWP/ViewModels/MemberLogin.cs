using AMWP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AMWP.ViewModels
{
    public class MemberLogin
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請填寫會員帳號")]
        [EmailAddress(ErrorMessage = "電子郵件格式有誤")]
        [DataType(DataType.EmailAddress)]
        public string Account { get; set; }

        string password;
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請填寫會員密碼")]
        [DataType(DataType.Password)]
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
    }
}