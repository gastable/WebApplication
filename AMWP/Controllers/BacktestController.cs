using AMWP.Models;
using AMWP.ViewModels;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace AMWP.Controllers
{

    public class BacktestController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        // GET: Backtest
        public ActionResult Input()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Input(Backtest backtests)
        {
            backtests.Symbol1 = backtests.Symbol1 != null ? backtests.Symbol1.ToUpper() : null;
            backtests.Symbol2 = backtests.Symbol2 != null ? backtests.Symbol1.ToUpper() : null;
            backtests.Symbol3 = backtests.Symbol3 != null ? backtests.Symbol1.ToUpper() : null;
            backtests.Symbol4 = backtests.Symbol4 != null ? backtests.Symbol1.ToUpper() : null;
            backtests.Symbol5 = backtests.Symbol5 != null ? backtests.Symbol1.ToUpper() : null;

            DateTime startDate = Convert.ToDateTime((backtests.StartYear - 1) + "-12-01");
            DateTime endDate = Convert.ToDateTime(backtests.EndYear + "-12-31");
            List<string> oldSymbols = new List<string>() { backtests.Symbol1, backtests.Symbol2, backtests.Symbol3, backtests.Symbol4, backtests.Symbol5 };
            List<string> newSymbols = new List<string>();
            List<string> hasDatas = new List<string>();
            List<string> noSymbols = new List<string>();
            List<string> noDatas = new List<string>();
            List<DateTime> firstDates = new List<DateTime>();
            DateTime dataFirstDate;
            DateTime newStartDate;
            List<TimeSeriesByYears> results = new List<TimeSeriesByYears>();
            

            var queriedTable = db.Monthly.Join(db.Securities, m => m.SecID, s => s.SecID, (m, s) => new { Symbol = s.Symbol, Date = m.Date, Close = m.Close, AdjClose = m.AdjClose }).ToList();

            //依據是否有市場資料分配新list
            foreach (var item in oldSymbols)
            {
                if (!item.IsNullOrEmpty())
                {
                    bool symbolExist = queriedTable.Exists(s => s.Symbol.Equals(item));
                    if (symbolExist)
                    {
                        newSymbols.Add(item);
                    }
                    else
                        noSymbols.Add(item);
                }
            }
            //有市場資料的再分查詢區間內是否有資料，並找出最晚的開始日期，再去取資料
            if (newSymbols.Count != 0)
            {
                foreach (var item in newSymbols)
                {
                    dataFirstDate = queriedTable.Where(t => t.Symbol == item).OrderBy(t => t.Date).FirstOrDefault().Date;
                    if (dataFirstDate != null)
                    {
                        firstDates.Add(dataFirstDate);
                    }
                    else
                    {
                        noDatas.Add(item);
                    }
                }

                newStartDate = firstDates.Max(t => t.Date);
                foreach (var item in newSymbols)
                {
                    GetAssets ga = new GetAssets();
                    results.Add(ga.GetTimeSeriesByYears(item, newStartDate, endDate));
                }

            }
            //沒交易資料的給錯誤訊息
            if (noSymbols.Count != 0)
            {
                string msg = "";
                foreach (var item in noSymbols)
                {
                    msg += item + ",";
                }
                ViewBag.NoSymbol = "查無" + msg + "的市場數據，請確認是否輸入錯誤代號";
            }
            //有交易資料但不在區間的給錯誤訊息
            if (noDatas.Count != 0)
            {
                string msg = "";
                foreach (var item in noDatas)
                {
                    msg += item + ",";
                }
                ViewBag.Nodata = backtests.StartYear + "年至" + backtests.EndYear + "年，無" + msg + "的市場數據，請調整查詢年份";
            }

            ViewBag.Result = results;
            ViewBag.Source = backtests;
            ViewBag.StartYear = backtests.StartYear;
            ViewBag.EndYear = backtests.EndYear;
            return View(backtests);
            //return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}