using _07WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
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
        public IHttpActionResult Post([FromBody] Customers value)
        {
            //db.Customers.Add(value);
            //db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Customers.Add(value);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (db.Customers.Count(c => c.CustomerID == value.CustomerID) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id= value.CustomerID },value);
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
