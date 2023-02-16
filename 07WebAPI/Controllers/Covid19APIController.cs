using _07WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace _07WebAPI.Controllers
{
    public class Covid19APIController : ApiController
    {

        public async Task<IEnumerable<Covid19>> Get()
        {

            string url = "https://covid-19.nchc.org.tw/api/covid19?CK=covid-19@nchc.org.tw&querydata=5002&limited=全部縣市";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var resp = await client.GetStringAsync(url);

            var collection = JsonConvert.DeserializeObject<IEnumerable<Covid19>>(resp);

            return collection;
        }
    }
}
