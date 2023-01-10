using MCSDD10.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MCSDD10.Models
{
    public class Members
    {
        [Key]
        [DisplayName("會員編號")]        
        public int MemberID { get; set; }

        [DisplayName("會員姓名")]
        [StringLength(40, ErrorMessage = "姓名不可超過40個字")]
        [Required(ErrorMessage = ("請填寫會員姓名"))]
        public string MemberName { get; set; }

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = ("請填寫會員帳號"))]
        [StringLength(20, ErrorMessage = "帳號不可超過20個字")]
        [RegularExpression("[A-Z][a-zA-Z0-9]{4,19}")]
        public string Account { get; set; }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [DisplayName("生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime MemberBirthday { get; set; }

        [DisplayName("照片")]
        public string MemberPhotoFile { get; set; }   //用來存圖片的路徑

        string password;

        [DisplayName("密碼")]
        [Required(ErrorMessage = ("請填寫密碼"))]
        [DataType(DataType.Password)]
        //[MinLength(8, ErrorMessage = "密碼最少8碼")]
        //[MaxLength(20, ErrorMessage = "密碼最多20碼")]  因為密碼有雜湊，不能律定長度
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                //BR br = new BR();
                //要先把Password收到的value做雜湊，再把值給password     
                password = BR.getHashPassword(value);
            }
        }
    }
}