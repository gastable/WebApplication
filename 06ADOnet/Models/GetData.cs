using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _06ADOnet.Models
{
    public class GetData
    {
        //1.建立資料庫連線的static物件
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString);
        //運用組態管理員物件去找ConnectionStrings，再用name當索引後，找到屬性ConnectionString的值

        //2.建立SQL命令物件
        SqlCommand cmd = new SqlCommand("", conn); //最少給2個屬性參數(sql指令字串CommandText(提出去用sql指定)，跟連線物件)

        //3.建立資料讀取物件
        SqlDataReader rd; //先不初始化，因為下面要寫參數

        //建立Login打開連線的方法，要傳SQL指令，及SqlParameter的要輸入進SQL的參數過來
        public SqlDataReader LoginQuery(string sql,List<SqlParameter> para)
        {
            cmd.CommandText = sql; //CommandText是SqlCommand物件的屬性

            foreach(SqlParameter p in para)
            {
            cmd.Parameters.Add(p);  //把SqlParameter裡的參數逐一加入SqlParameterCollection
            }

            conn.Open();//打開連線，另外記得後面要再先寫Close()
            try         //因為讀取資料庫很容易發生例外，放try catch處理
            {
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //讀取cmd執行後的資料放入rd，傳入「關閉connection的指令參數」，這樣執行完rd後，關閉連線
                rd.Read();   //讀完後，會有HasRows的布林值可以判斷是否有資料                
            }
            catch
            {
                conn.Close();
                return rd;  //也有可能rd是null(如：伺服器壞了)，那controller的rd.Close()會發生例外，controller來處理
            }
            
            return rd;  //回傳給session接        
         }
    }
}