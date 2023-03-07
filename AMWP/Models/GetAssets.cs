using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMWP.Models
{
    public class GetAssets
    {

        public double GetTotalCash(int id)
        {
            GetData gd = new GetData();
            string sqlCash = "select isnull(c.SSN,0) as SSN, cu.CCYID,cu.[Name], isnull(c.Amount,0) as Amount,cu.ExchRate, isnull(dbo.fnToMemCCY(@id)*C.Amount*Cu.ExchRate, 0) as ToCCY " +
                        "from Currencies as CU  left outer join(select * " +
                        "from cash where MemID = @id) as c " +
                        "on CU.CCYID = c.CCYID " +
                        "order by Cu.CCYID desc";
            List<SqlParameter> listCash = new List<SqlParameter>() {
                    new SqlParameter("id",id)
            };
            DataTable cash = gd.TableQuery(sqlCash, listCash);
            double cashSum = 0;
            foreach (DataRow row in cash.Rows)
            {
                cashSum += Convert.ToDouble(row["ToCCY"]);
            }
            return cashSum;
        }

        public double GetTotalSecValue(int id)
        {
            GetData gd = new GetData();

            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            DataTable securities = gd.TableQueryBySP(sql, list);
            double sumPV = 0;
            foreach (DataRow row in securities.Rows)
            {
                sumPV += (Math.Round(Convert.ToDouble(row["Close"]) * Convert.ToDouble(row["Share"]) * Convert.ToDouble(row["ExchRate"]) * Convert.ToDouble(row["ToCCY"]), 2));
            }
            return sumPV;
        }

        public TimeSeriesByYears GetTimeSeriesByYears(string symbol,DateTime startDate,DateTime endDate)
        {
            GetData gd = new GetData();
            string sql = "select s.Symbol, m.[Date], m.[Close], m.AdjClose from Monthly as m inner join Securities as s on m.SecID = s.SecID where s.Symbol = @symbol and m.[Date] between @date1 and @date2 order by m.[Date]";
            List<SqlParameter> list = new List<SqlParameter> {
                         new SqlParameter("symbol",symbol),
                         new SqlParameter("date1",startDate),
                         new SqlParameter("date2",endDate)
                    };
            DataTable dt = gd.TableQuery(sql, list);

            TimeSeriesByYears ty = new TimeSeriesByYears();
            List<string> symbols = new List<string>();
            List<string> dates = new List<string>();
            List<double> closes = new List<double>();
            List<double> adjCloses = new List<double>();

            foreach (DataRow row in dt.Rows)
            {
                dates.Add(Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd"));
                symbols.Add(Convert.ToString(row["Symbol"]));
                closes.Add(Math.Round(Convert.ToDouble(row["Close"]), 2));
                adjCloses.Add(Math.Round(Convert.ToDouble(row["AdjClose"]), 2));
            };

            ty.Date = dates;
            ty.Symbol = symbols;
            ty.Close = closes;
            ty.AdjClose = adjCloses;

            return ty;
        }

    }
}