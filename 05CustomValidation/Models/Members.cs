using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _05CustomValidation.Models
{
    public class Members
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("[A-Z][1-2][0-9]{8}")]
        [CheckIDNumber]
        public string IDNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }   

        public class CheckIDNumber : ValidationAttribute    //一定繼承ValidationAttribute
        {
            //建構子
            public CheckIDNumber()
            {
                ErrorMessage = "身份證字號不合法";  //自定預設驗證錯誤訊息
            }

            public override bool IsValid(object value)
            {
                string idNumber =value.ToString();
                string firstChar = idNumber.Substring(0, 1);
                string[] IDHead = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
                int n,n1;
                int[] IDBody = new int[9];

                //第一位字母的數字與乘積
                n1 = Array.IndexOf(IDHead, firstChar) + 10;
                n = (n1 / 10) * 1 + (n1 % 10) * 9;
                //抓後面亂碼的數字與乘積
                for (int i = 0; i < 8; i++)
                {
                    IDBody[i] = int.Parse(idNumber.Substring(i + 1, 1));
                    n += IDBody[i] * (8 - i);
                }
                //抓檢查碼與乘積
                n += int.Parse(idNumber.Substring(9, 1)) * 1;

                if (n%10==0)
                    return true;

                return false;
            }
        }

    }
}