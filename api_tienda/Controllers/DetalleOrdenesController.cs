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
    public class DetalleOrdenesController : ApiController
    {
        private TiendaContext db = new TiendaContext();

        // GET: api/DetalleOrdens
        public IQueryable<DetalleOrden> GetDetalleOrdenes()
        {
            return db.DetalleOrdenes;
        }

        [HttpGet, Route("api/DetalleOrdenes/{limit}")]
        // GET: api/DetalleOrdens
        public IQueryable<DetalleOrden> GetDetalleOrdenes(int limit)
        {
            List<DetalleOrden> lstProductos = new List<DetalleOrden>();
            lstProductos = db.DetalleOrdenes.Where(s => s.Deleted_at != null).Take(limit).ToList();
            return lstProductos.AsQueryable();
        }

        // GET: api/DetalleOrdens/5
        [ResponseType(typeof(DetalleOrden))]
        public IHttpActionResult GetDetalleOrden(long id)
        {
            DetalleOrden detalleOrden = db.DetalleOrdenes.Find(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return Ok(detalleOrden);
        }

        // PUT: api/DetalleOrdens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleOrden(long id, DetalleOrden detalleOrden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleOrden.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleOrden).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleOrdenExists(id))
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

        // POST: api/DetalleOrdens
        [ResponseType(typeof(DetalleOrden))]
        public IHttpActionResult PostDetalleOrden(DetalleOrden detalleOrden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleOrdenes.Add(detalleOrden);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleOrden.Id }, detalleOrden);
        }

        // DELETE: api/DetalleOrdens/5
        [ResponseType(typeof(DetalleOrden))]
        public IHttpActionResult DeleteDetalleOrden(long id)
        {
            DetalleOrden detalleOrden = db.DetalleOrdenes.Find(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            db.DetalleOrdenes.Remove(detalleOrden);
            db.SaveChanges();

            return Ok(detalleOrden);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetalleOrdenExists(long id)
        {
            return db.DetalleOrdenes.Count(e => e.Id == id) > 0;
        }
    }
}