using AMWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class UploadDataController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        // GET: UploadData
        public async Task<ActionResult> Upload()
        {
            string url = "https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=VT&apikey=XBRCUGRFRDK9P0HJ";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var resp = await client.GetStringAsync(url);

            var collection = JsonConvert.DeserializeObject<IEnumerable<Securities>>(resp);

            return View(collection);
        }
    }
}