using _07WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace _07WebAPI.Controllers
{
    public class ValuesController : ApiController
    {

        NorthwindEntities db = new NorthwindEntities();

        public IEnumerable<Customers> Get()
        {
            return db.Customers;
        }

        // GET api/values/5
        public Customers Get(string id)
        {
            var c = db.Customers.Find(id);
            return c;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
