using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _05CustomValidation.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="請填寫身分證字號")]
        [StringLength(10)]
        [RegularExpression("[A-Z][12][0-9]{8}")]
        [CheckIDNumber(ErrorMessage ="身分證字號不合法")]
        public string IDNumber { get;set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }    



        public class CheckIDNumber:ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                string idNumber = value.ToString();

                const string eng = "ABCDEFGHJKLMNPQRSTUVXYWZIO";

                //假設 Ａ１２３４５６７８９

                string t = idNumber.Substring(0,1);  //會抓到A
                int intEng = eng.IndexOf(t) + 10;  //intEng=10
                int n1 = intEng / 10;  //取到1
                int n2 = intEng % 10;  //取到0

                int sum = 0;
               
                
                sum = n1 * 1 + n2 * 9;

                /*
                sum +=  Convert.ToInt32(idNumber.Substring(1, 1)) * 8 +
                        Convert.ToInt32(idNumber.Substring(2, 1)) * 7 +
                        Convert.ToInt32(idNumber.Substring(3, 1)) * 6 +
                        Convert.ToInt32(idNumber.Substring(4, 1)) * 5 +
                        Convert.ToInt32(idNumber.Substring(5, 1)) * 4 +
                        Convert.ToInt32(idNumber.Substring(6, 1)) * 3 +
                        Convert.ToInt32(idNumber.Substring(7, 1)) * 2 +
                        Convert.ToInt32(idNumber.Substring(8, 1)) * 1 +
                        Convert.ToInt32(idNumber.Substring(9, 1)) * 1 ;
                */


                for(int i = 1; i < 9; i++)
                {
                    sum += Convert.ToInt32(idNumber.Substring(i, 1)) * (9-i);
                }

                sum += Convert.ToInt32(idNumber.Substring(9, 1));

                if (sum % 10 == 0)
                   return true;


                return false;
            }
        }

    }
}