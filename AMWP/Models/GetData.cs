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

        SqlDataAdapter adp = new SqlDataAdapter("", conn);//等於 SqlCommand+SqlDataReader，但只能讀(不能增刪修)，而且比較佔記憶體

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

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
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection); /* 存入資料改用這個(cmd.ExecuteNonQuery)*/
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

        public DataTable TableQuery(string sql)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command
            adp.Fill(ds);  //把取到的Table填入DataSet

            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQuery(string sql, List<SqlParameter> para)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command

            foreach (SqlParameter p in para)
            {
                adp.SelectCommand.Parameters.Add(p);
            }

            adp.Fill(ds);  //把取到的Table填入DataSet

            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQueryBySP(string sql)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            adp.Fill(ds);  //把取到的Table填入DataSet

            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQueryBySP(string sql, List<SqlParameter> para)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;

            foreach (SqlParameter p in para)
            {
                adp.SelectCommand.Parameters.Add(p);
            }

            adp.Fill(ds);  //把取到的Table填入DataSet

            if (ds.Tables.Count == 0)
                return dt;

            dt = ds.Tables[0];

            return dt;
        }

        public void InsertCommand(string sql)
        {
            cmd.CommandText = sql;             

            conn.Open();//打開連線

            cmd.ExecuteNonQuery(); /* 存入資料改用這個(cmd.ExecuteNonQuery)*/            
            conn.Close();           
        }

        public TimeSeries GetTimeSeries()
        {
            TimeSeries ts = new TimeSeries();
            List<string> secIDs = new List<string>();
            List<string> dates = new List<string>();
            List<double> opens = new List<double>();
            List<double> highs = new List<double>();
            List<double> lows = new List<double>();
            List<double> closes = new List<double>();
            List<double> adjCloses = new List<double>();
            List<double> dividends = new List<double>();
            List<long> volumes = new List<long>();

            foreach (DataRow row in dt.Rows)
            {
                dates.Add(Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd"));
                opens.Add(Math.Round(Convert.ToDouble(row["Open"]), 2));
                highs.Add(Math.Round(Convert.ToDouble(row["High"]), 2));
                lows.Add(Math.Round(Convert.ToDouble(row["Low"]), 2));
                closes.Add(Math.Round(Convert.ToDouble(row["Close"]), 2));
                adjCloses.Add(Math.Round(Convert.ToDouble(row["AdjClose"]), 2));
                dividends.Add(Math.Round(Convert.ToDouble(row["Dividend"]), 2));
                volumes.Add(Convert.ToInt64(row["Volume"]));
            };
            ts.Date = dates;
            ts.Open = opens;
            ts.High = highs;
            ts.Low = lows;
            ts.Close = closes;
            ts.AdjClose = adjCloses;
            ts.Dividend = dividends;
            ts.Volume = volumes;
            return ts;
        }
    }
}