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

        //2.建立SQL DML命令物件
        SqlCommand cmd = new SqlCommand("", conn); //最少給2個屬性參數(sql指令字串CommandText(提出去用sql指定)，跟連線物件)

        //3.建立資料讀取物件，逐筆讀取
        SqlDataReader rd; //抽象類別不能初始化，因為下面要寫參數

        
        SqlDataAdapter adp = new SqlDataAdapter("", conn);//等於 SqlCommand+SqlDataReader，但只能讀(不能增刪修)，而且比較佔記憶體
        DataSet ds = new DataSet();  //建立資料集物件
        DataTable dt = new DataTable();  //建立資料表物件

        public DataTable TableQuery(string sql)  //從db讀資料後，存進記憶體的datatable物件
        {
            adp.SelectCommand.CommandText = sql;  //指定 SelectCommand
            adp.Fill(ds);  //把取到的Table填入DataSet，第一張表索引值Tables[0]

            dt = ds.Tables[0];  

            return dt;
        }

        public DataTable TableQuery(string sql, List<SqlParameter> para)  //多載
        {
            adp.SelectCommand.CommandText = sql;
            foreach (SqlParameter p in para)
            {
                adp.SelectCommand.Parameters.Add(p);  //餵參數
            }
            adp.Fill(ds);  

            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQueryBySP(string sql, List<SqlParameter> para)  //執行預存程序讀TABLE
        {
            adp.SelectCommand.CommandText = sql;
            adp.SelectCommand.CommandType = CommandType.StoredProcedure; //告訴SQL要執行預存程序
            foreach (SqlParameter p in para)
            {
                adp.SelectCommand.Parameters.Add(p);  //餵參數
            }
            adp.Fill(ds);

            if (ds.Tables.Count == 0)        //如果取到沒有資料的表
                return dt;
            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQueryBySP(string sql)  
        {
            adp.SelectCommand.CommandText = sql;
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.Fill(ds); 

            dt = ds.Tables[0];

            return dt;
        }



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
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);      /* 存入用這個(cmd.ExecuteNonQuery)*/
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