using _02View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _02View.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };

            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };

            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };

            List<NightMarket> list = new List<NightMarket>();
            NightMarket nm;

            for (int i = 0; i < id.Length; i++)
            {
                nm=new NightMarket();
                nm.Id = id[i];
                nm.Name = name[i];
                nm.Address= address[i];
                list.Add(nm);
            }

            return View(list);
        }
        public ActionResult Create(string id)
        {
            return View();
        }

        public ActionResult Display(string id)
        {
            ViewBag.ID = id;

            return View();
        }

        public ActionResult Razor()
        {
            return View();
        }
       
    }
}