using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class SetData
    {
        //1.建立資料庫連線的static物件
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMWPConnection"].ConnectionString);

        //2.建立SQL DML命令物件
        SqlCommand cmd = new SqlCommand("", conn);

        public void executeSql(string sql, List<SqlParameter> list)
        {
            cmd.CommandText = sql;

            foreach (var p in list)
            {
                cmd.Parameters.Add(p);
            }

            conn.Open();
            cmd.ExecuteNonQuery();//只執行指令，不要求資料
            cmd.Dispose(); //用迴圈存入資料時，每一次都要將參數重新初始後，才能存入，不然就例外
            conn.Close();
        }

        public void executeSql(string sql)
        {
            cmd.CommandText = sql;

            conn.Open();
            cmd.ExecuteNonQuery();//只執行指令，不要求資料
            cmd.Dispose(); //用迴圈存入資料時，每一次都要將參數重新初始後，才能存入，不然就例外
            conn.Close();
        }

        public void executeSP(string sql, List<SqlParameter> para)
        {
            cmd.CommandText= sql;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in para)
            {
                cmd.Parameters.Add(p);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose(); 
            conn.Close();
        }

        public void executeSP(string sql)
        {
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}