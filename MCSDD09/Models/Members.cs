using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;

namespace MCSDD09.Models
{
    public class Members
    {
        [Key]
        public int MemberID { get; set; }
        //要將照片儲存成供字串img src讀取
        [DisplayName("會員照片")]
        public string MemberPhotoFile { get; set; }
        
        [DisplayName("加入日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }
        [StringLength(40, ErrorMessage = "姓名以40字為限")]
        [Required(ErrorMessage = "姓名為必填欄位")]
        [DisplayName("姓名")]
        public string MemberName { get; set; }
        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime MemberBirthdy { get; set; }
        
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請填寫帳號")]
        [StringLength(20, ErrorMessage = "帳號20字為限")]
        [RegularExpression("[A-za-z1-9]{5,20}")]
        public string Account { get; set; }

        string password = "";
        [DisplayName("密碼")]
        public string Password { 
            get 
            {
                return password;
            }
            set 
            { 
                password =BusinessRule.getHashPassword (value);
            }
        }
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
