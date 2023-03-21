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

    [LoginCheck(type =2)]
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
            DateTime startDate = Convert.ToDateTime((backtests.StartYear) + "-01-01");
            DateTime endDate = Convert.ToDateTime((backtests.EndYear) + "-12-31");
            List<string> oldSymbols = new List<string>() { backtests.Symbol1, backtests.Symbol2, backtests.Symbol3, backtests.Symbol4, backtests.Symbol5};
            List<double> oldSlices1 = new List<double>() { Convert.ToDouble(backtests.Slice1), Convert.ToDouble(backtests.Slice4), Convert.ToDouble(backtests.Slice7), Convert.ToDouble(backtests.Slice10), Convert.ToDouble(backtests.Slice13) };
            List<double> oldSlices2 = new List<double>() { Convert.ToDouble(backtests.Slice2), Convert.ToDouble(backtests.Slice5), Convert.ToDouble(backtests.Slice8), Convert.ToDouble(backtests.Slice11), Convert.ToDouble(backtests.Slice14) };
            List<double> oldSlices3 = new List<double>() { Convert.ToDouble(backtests.Slice3), Convert.ToDouble(backtests.Slice6), Convert.ToDouble(backtests.Slice9), Convert.ToDouble(backtests.Slice12), Convert.ToDouble(backtests.Slice15) };
            List<double> newSlices1 = new List<double>();
            List<double> newSlices2 = new List<double>();
            List<double> newSlices3 = new List<double>();
            List<double> hasSlices1 = new List<double>();
            List<double> hasSlices2 = new List<double>();
            List<double> hasSlices3 = new List<double>();
            List<double> shares1 = new List<double>();
            List<double> shares2 = new List<double>();
            List<double> shares3 = new List<double>();
            List<double> values1 = new List<double>();
            List<double> values2 = new List<double>();
            List<double> values3 = new List<double>();
            List<double> endValues1 = new List<double>();
            List<double> endValues2 = new List<double>();
            List<double> endValues3 = new List<double>();
            List<string> newSymbols = new List<string>();
            List<string> hasDatas = new List<string>();
            List<string> noSymbols = new List<string>();
            List<string> noDatas = new List<string>();

            List<DateTime> firstDates = new List<DateTime>();
            List<DateTime> lastDates = new List<DateTime>();
            DateTime dataFirstDate;
            DateTime dataLastDate;
            DateTime newStartDate;
            DateTime newEndDate;

            List<TimeSeriesByYears> mktDatas = new List<TimeSeriesByYears>();

            var queriedTable = db.Weekly.Join(db.Securities, m => m.SecID, s => s.SecID, (m, s) => new { Symbol = s.Symbol, Date = m.Date, Close = m.Close, AdjClose = m.AdjClose }).ToList();
            string msg = "", gdMsg="";

            ViewBag.StartYear = backtests.StartYear;
            ViewBag.EndYear = backtests.EndYear;
            
            TempData["backtests"] = backtests;
            TempData.Keep("backtests");
            //依據是否有市場資料分配新list
            for(int i = 0; i < 5; i++)
            {
                if (!(oldSymbols[i].IsNullOrEmpty()))
                {
                    bool symbolExist = queriedTable.Exists(s => s.Symbol.Equals(oldSymbols[i].ToUpper()));
                    if (symbolExist)
                    {
                        newSymbols.Add(oldSymbols[i].ToUpper());
                        newSlices1.Add(oldSlices1[i]);
                        newSlices2.Add(oldSlices2[i]);
                        newSlices3.Add(oldSlices3[i]);
                    }
                    else
                    {
                        noSymbols.Add(oldSymbols[i].ToUpper());
                        msg += oldSymbols[i].ToUpper() + ",";
                    }
                }
            }
           
            //沒交易資料的給錯誤訊息，直接踢回
            if (noSymbols.Count() != 0)
            {
                ViewBag.NoSymbol = "查無" + msg + "的市場數據，請確認是否輸入錯誤代號";
                ViewBag.Color = "text-danger";
                ViewBag.Source = backtests;
                return View(backtests);
            }

            //有市場資料的再分查詢區間內是否有資料，全部都有資料才找出最晚的開始日期去取資料
            msg = "";
            if (newSymbols.Count() != 0)
            {
                for (int i = 0; i < newSymbols.Count(); i++)
                {
                    var newQueriedTable = queriedTable.Where(t => t.Symbol == newSymbols[i] && t.Date >= startDate && t.Date <= endDate).ToList();
                    bool symbolExist = newQueriedTable.Exists(s => s.Symbol.Equals(newSymbols[i]));
                    if (symbolExist)
                    {
                        dataFirstDate = newQueriedTable.OrderBy(t => t.Date).FirstOrDefault().Date;
                        dataLastDate = newQueriedTable.OrderByDescending(t => t.Date).FirstOrDefault().Date;
                        firstDates.Add(dataFirstDate);
                        lastDates.Add(dataLastDate);
                        hasDatas.Add(newSymbols[i]);
                        hasSlices1.Add(newSlices1[i] * backtests.InitAmount * 100);
                        hasSlices2.Add(newSlices2[i] * backtests.InitAmount * 100);
                        hasSlices3.Add(newSlices3[i] * backtests.InitAmount * 100);
                        gdMsg += newSymbols[i] + ",";
                    }
                    else
                    {
                        noDatas.Add(newSymbols[i]);
                        msg += newSymbols[i] + ",";
                    }
                }
            }

            //查詢區間內沒資料的給錯誤訊息，踢回
            if (noDatas.Count() != 0)
            {
                ViewBag.Nodata = backtests.StartYear + "年至" + backtests.EndYear + "年，無" + msg + "的市場數據，請調整查詢年份";
                ViewBag.Color = "text-danger";
                ViewBag.Source = backtests;
                return View(backtests);
            }

            if (hasDatas.Count() != 0)
            {
                int d = 0;
                newStartDate = firstDates.Max(t => t.Date);
                newEndDate = lastDates.Max(t => t.Date);

                d = firstDates.IndexOf(newStartDate);

                for (int i = 0; i < hasDatas.Count(); i++)
                {
                    GetAssets ga = new GetAssets();
                    mktDatas.Add(ga.GetTimeSeriesByYears(hasDatas[i], newStartDate, newEndDate));
                }    
                for (int j = 0; j < mktDatas[0].AdjClose.Count(); j++)
                {
                    endValues1.Add(0);
                    endValues2.Add(0);
                    endValues3.Add(0);
                }
                for (int i = 0; i < hasDatas.Count(); i++)
                {
                    shares1.Add(hasSlices1[i] / mktDatas[i].AdjClose[0]);
                    shares2.Add(hasSlices2[i] / mktDatas[i].AdjClose[0]);
                    shares3.Add(hasSlices3[i] / mktDatas[i].AdjClose[0]);
                    values1.Clear();
                    values2.Clear();
                    values3.Clear();
                    
                        for (int j= 0; j < mktDatas[i].AdjClose.Count();j++)
                    {                        
                        values1.Add(mktDatas[i].AdjClose[j] * shares1[i]);
                        values2.Add(mktDatas[i].AdjClose[j] * shares2[i]);
                        values3.Add(mktDatas[i].AdjClose[j] * shares3[i]);
                        endValues1[j] += Math.Round(values1[j],0);
                        endValues2[j] += Math.Round(values2[j],0);
                        endValues3[j] += Math.Round(values3[j],0);
                    }
                   
                }

                ViewBag.HasData = "成功取得"+ gdMsg + "的市場資料，回測區間為" + newStartDate.ToString("yyyy/MM/dd") + "至" + newEndDate.ToString("yyyy/MM/dd");
                ViewBag.Color = "text-primary";
                ViewBag.Values1 = endValues1;
                ViewBag.Values2 = endValues2;
                ViewBag.Values3 = endValues3; 
                ViewBag.Results = mktDatas;
                
                
                ;
                //ViewBag.Slice = Shares1[2];
            }
            else
            {
                ViewBag.NoSymbol = "請輸入證券代號";
                ViewBag.Color = "text-danger";
            }

            ViewBag.Source = backtests;
            return View(backtests);
        }




    }
}