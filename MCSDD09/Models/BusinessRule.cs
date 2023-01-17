using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MCSDD09.Models
{
    public class BusinessRule
    {  //這是商業規則的類別


        //這個是密碼雜湊的演算法method
        public static string getHashPassword(string pw)
        {
            byte[] hashValue;
            string result = "";


            System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

            byte[] pwBytes = ue.GetBytes(pw);

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