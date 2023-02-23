using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AMWP.Controllers
{
    public class UploadDataController : Controller
    {
        // GET: UploadData
        public Task<ActionResult> Upload()
        {
            string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=VT&apikey=XBRCUGRFRDK9P0HJ";
            Uri queryUri = new Uri(url);

            using (WebClient client = new WebClient())
            {

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));



                //HttpClient client= new HttpClient();
                //client.MaxResponseContentBufferSize= Int32.MaxValue;
                //var resp = await client.GetStringAsync(url);

                //var collection = JsonConvert.DeserializeObject<List<string>>(resp);
                return View(json_data);
            }
        }
    }
}