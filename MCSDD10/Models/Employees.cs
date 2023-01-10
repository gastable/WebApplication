using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace MCSDD10.Models
{
    public class Employees
    {
        [Key]
        [DisplayName("員工代碼")]  //命名規則會在資料庫端執行        
        public int EmployeeID { get; set; }

        [DisplayName("員工姓名")]
        [StringLength(40,ErrorMessage ="員工姓名不可超過40個字")] //資料長度
        [Required(ErrorMessage = ("請填寫員工姓名"))]
        public string EmployeeName { get; set; }

        [DisplayName("帳號")]
        [Required(ErrorMessage = ("請填寫帳號"))]
        [StringLength(20, ErrorMessage = "帳號不可超過20個字")]  //若帳號為email，加用[EmailAddress]
        [RegularExpression("[A-Z][a-zA-Z0-9]{4,19}")]
        public string Account { get; set; }  //1.屬性封裝 2.get是讀取，set是指定運算，如只寫{get;}就是唯讀屬性！

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //DataFormatStrin="{0}"表示用預設格式，"{0:yyyy-MM-dd}"表示用這樣的格式，(要加時間用 hh:mm)，要套用在資料庫的編輯模式加上ApplyFormatInEditMode
        [DataType(DataType.DateTime)]  //驗證是否為DateTime格式
        [Required]  //系統自動產生，必要欄位但不用errorMsg
        public DateTime CreatedDate { get; set; }


        //field 先宣告一個欄位
        string password;

        [DisplayName("密碼")]
        [Required(ErrorMessage = ("請填寫密碼"))]
        [DataType(DataType.Password)]  //會遮住密碼
        //[MinLength(8, ErrorMessage = "密碼最少8碼")]
        //[MaxLength(20, ErrorMessage = "密碼最多20碼")]
        //[StringLength(20)]因為密碼要雜湊運算，所以不能限制長度
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                //BR br = new BR();   沒宣告static要這行
                //要先把Password收到的value做雜湊，再把值給password     
                password= BR.getHashPassword(value);//執行完的值拋給password，value是保留字，代表函數return的值
            }
        }
        
        

    }
}