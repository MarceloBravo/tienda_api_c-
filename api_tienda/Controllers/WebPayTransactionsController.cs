using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using api_tienda.DAL;
using api_tienda.Models;

namespace api_tienda.Controllers
{
    public class WebPayTransactionsController : ApiController
    {
        private TiendaContext db = new TiendaContext();

        // GET: api/WebPayTransactions
        public IQueryable<Transaction> GetWebPayTransactions()
        {
            return db.WebPayTransactions;
        }
        
        [Route("api/LatsWebPayTransaction")]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult GetLastWebPayTransaction()
        {
            Transaction webPayTransaction = db.WebPayTransactions.OrderByDescending(p => p.Id).First();
            if (webPayTransaction == null)
            {
                return NotFound();
            }
            return Ok(webPayTransaction);
        }

        // GET: api/WebPayTransactions/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult GetWebPayTransaction(long id)
        {
            Transaction webPayTransaction = db.WebPayTransactions.Find(id);
            if (webPayTransaction == null)
            {
                return NotFound();
            }

            return Ok(webPayTransaction);
        }

        // PUT: api/WebPayTransactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWebPayTransaction(long id, Transaction webPayTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webPayTransaction.Id)
            {
                return BadRequest();
            }

            db.Entry(webPayTransaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebPayTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WebPayTransactions
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostWebPayTransaction(Transaction webPayTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebPayTransactions.Add(webPayTransaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = webPayTransaction.Id }, webPayTransaction);
        }

        // DELETE: api/WebPayTransactions/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteWebPayTransaction(long id)
        {
            Transaction webPayTransaction = db.WebPayTransactions.Find(id);
            if (webPayTransaction == null)
            {
                return NotFound();
            }

            db.WebPayTransactions.Remove(webPayTransaction);
            db.SaveChanges();

            return Ok(webPayTransaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebPayTransactionExists(long id)
        {
            return db.WebPayTransactions.Count(e => e.Id == id) > 0;
        }
    }
}