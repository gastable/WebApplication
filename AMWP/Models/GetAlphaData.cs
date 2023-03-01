using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceStack;

namespace AMWP.Models
{
    public class GetAlphaData
    {
        public void TimeSeries(string apiUrl)
        {
            apiUrl = apiUrl.GetStringFromUrl();
            var timeSeries = apiUrl.FromCsv<List<AlphaVantageData>>().ToList();


            string json = JsonConvert.SerializeObject(new { Table = timeSeries });
            DataSet set = JsonConvert.DeserializeObject<DataSet>(json);

            string conString = ConfigurationManager.ConnectionStrings["AMWPConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name.
                    sqlBulkCopy.DestinationTableName = "dbo.Uploads";

                    //[OPTIONAL]: Map the DataTable columns with that of the database table

                    sqlBulkCopy.ColumnMappings.Add("Timestamp", "Date");
                    sqlBulkCopy.ColumnMappings.Add("Open", "Open");
                    sqlBulkCopy.ColumnMappings.Add("High", "High");
                    sqlBulkCopy.ColumnMappings.Add("Low", "Low");
                    sqlBulkCopy.ColumnMappings.Add("Close", "Close");
                    sqlBulkCopy.ColumnMappings.Add("Adjusted_Close", "AdjClose");
                    sqlBulkCopy.ColumnMappings.Add("Volume", "Volume");
                    sqlBulkCopy.ColumnMappings.Add("Dividend_Amount", "Dividend");

                    con.Open();
                    sqlBulkCopy.WriteToServer(set.Tables[0]);
                    con.Close();
                }
            }
        }

        public void TimeSeriesWM(string apiUrl)
        {
            apiUrl = apiUrl.GetStringFromUrl();
            var timeSeries = apiUrl.FromCsv<List<AlphaVantageDataWM>>().ToList();


            string json = JsonConvert.SerializeObject(new { Table = timeSeries });
            DataSet set = JsonConvert.DeserializeObject<DataSet>(json);

            string conString = ConfigurationManager.ConnectionStrings["AMWPConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name.
                    sqlBulkCopy.DestinationTableName = "dbo.Uploads";

                    //[OPTIONAL]: Map the DataTable columns with that of the database table

                    sqlBulkCopy.ColumnMappings.Add("Timestamp", "Date");
                    sqlBulkCopy.ColumnMappings.Add("Open", "Open");
                    sqlBulkCopy.ColumnMappings.Add("High", "High");
                    sqlBulkCopy.ColumnMappings.Add("Low", "Low");
                    sqlBulkCopy.ColumnMappings.Add("Close", "Close");
                    sqlBulkCopy.ColumnMappings.Add("Adjusted Close", "AdjClose");
                    sqlBulkCopy.ColumnMappings.Add("Volume", "Volume");
                    sqlBulkCopy.ColumnMappings.Add("Dividend Amount", "Dividend");

                    con.Open();
                    sqlBulkCopy.WriteToServer(set.Tables[0]);
                    con.Close();
                }
            }
        }
    }
}