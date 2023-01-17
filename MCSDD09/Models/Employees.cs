using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;

namespace MCSDD09.Models
{
    public class Employees
    {
        [Key]

        [DisplayName("員工編號")]
        public int EmployeeID { get; set; }
        [DisplayName("員工姓名")]
        //設定字數長度
        [StringLength(40,ErrorMessage ="姓名以40字為限")]
        [Required(ErrorMessage ="姓名為必填欄位")]
        public string EmployeeName { get; set; }

        [DisplayName("到職日/建立日期")]
        //設定日期格式 (資料格式型態{目前資料0：表達方法M月h時m分鐘},在編輯模式時套用=T/F)
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true) ]
        //check data type
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }
        [DisplayName("帳號")]
        [Required(ErrorMessage ="請填寫帳號")]
        [StringLength(20,ErrorMessage ="帳號20字為限")]
        //如果帳號是email，要寫驗證[EmailAddress]
        //帳號是否可以重複//如果不行要驗證帳號是否重複
        //限定帳號格式(第一碼不為數字，後面不限，共需填5~20字)
        [RegularExpression("[A-Za-z][A-Za-z0-9]{4,19}")]
        public string Account { get; set; }


        string password;
        [DisplayName("密碼")]
        [Required(ErrorMessage ="請輸入密碼")]
        [DataType(DataType.Password)]
        //限制密碼長度
        //[MinLength(8,ErrorMessage ="密碼最少8碼")]
        //[MaxLength(20,ErrorMessage ="密碼最多20碼")]
        //需要讓資料變成雜湊存入資料庫要使用max才夠
        //[StringLength(20,ErrorMessage ="密碼20碼為限")]
        //密碼雜湊後才可存入資料庫(不同於加密，雜湊是不可解的)
        public string Password { 
            get 
            {
                return password;
            }
            set 
            {
                //先把password收到的value做雜湊，再把值給password


                password = BusinessRule.getHashPassword (value);
            }
        }

        //做成密碼雜湊method
        //public string getHashPassword(string pw)
        //{
        //    byte[] hashValue;
        //    string result = "";
        //    //使用namespace
        //    System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

        //    byte[] pwBytes = ue.GetBytes(pw);
        //    SHA256 shaHash = SHA256.Create();
        //    hashValue = shaHash.ComputeHash(pwBytes);
        //    foreach (byte b in hashValue)
        //    {
        //        result += b.ToString();
        //    }
        //    return result;
        //}



    }
}