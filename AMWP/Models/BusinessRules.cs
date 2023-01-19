using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AMWP.Models
{
    public class BusinessRules
    {
        //密碼雜湊的演算法method
        public static string getHashPassword(string pw)  //宣告對公開靜態方法，在其他類別就可以不用鑄造物件就使用
        {
            byte[] hashValue;
            string result = "";

            System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

            byte[] pwBytes = ue.GetBytes(pw);  //把密碼明碼轉成byte[]

            SHA256 shHash = SHA256.Create();
            hashValue = shHash.ComputeHash(pwBytes);

            foreach (byte b in hashValue)
            {
                result += b.ToString();
            }
            return result;
        }
    }
}