using _07WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace _07WebAPI.Controllers
{
    public class ValuesController : ApiController
    {

        NorthwindEntities db = new NorthwindEntities();

        public IEnumerable<Customers> Get()
        {
            return db.Customers;
        }
        public IEnumerable<Customers> Get(int page)
        {
            const int pageSize = 15;
            int skip = (page - 1) * pageSize;

            return db.Customers.OrderBy(m=>m.CustomerID).Skip(skip).Take(pageSize);
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
        public IHttpActionResult Put(string id, [FromBody] Customers value)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Entry(value).State = EntityState.Modified;
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
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(string id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
                return NotFound();

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }
    }
}
