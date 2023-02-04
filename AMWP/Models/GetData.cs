using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class GetData
    {
        //1.建立資料庫連線的static物件
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMWPConnection"].ConnectionString);

        //2.建立SQL命令物件
        SqlCommand cmd = new SqlCommand("", conn); 
       
        //3.建立資料讀取物件
        SqlDataReader rd;

        //建立Login打開連線的方法，要傳SQL指令，及SqlParameter的要輸入進SQL的參數過來
        public SqlDataReader LoginQuery(string sql, List<SqlParameter> para)
        {
            cmd.CommandText = sql; //CommandText是SqlCommand物件的屬性

            foreach (SqlParameter p in para)
            {
                cmd.Parameters.Add(p);  //把SqlParameter裡的參數逐一加入SqlParameterCollection
            }

            conn.Open();//打開連線
            try         
            {
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //讀取cmd執行後的資料放入rd，傳入「關閉connection的指令參數」，這樣執行完rd後，關閉連線
                rd.Read();   //讀完後，會有HasRows的布林值可以判斷是否有資料                
            }
            catch
            {
                conn.Close();
                return rd;  
            }
            return rd;          
        }
    }
}