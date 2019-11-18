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
    public class ProductosController : ApiController
    {
        private TiendaContext db = new TiendaContext();

        // GET: api/Productoes
        public IQueryable<Producto> GetProductos()
        {
            return db.Productos;
        }

        [Route("api/productosHome")]
        [HttpGet]
        public IQueryable<Producto> GetProductosHome()
        {
            List<Producto> lstProductos = new List<Producto>();
            lstProductos = db.Productos.Where(s => s.visible == true).Include(p => p.Categoria).Include(p => p.Marca).Include(p => p.Imagenes).ToList();            
            var prod = lstProductos.AsQueryable();
            return prod;
        }
        
        [Route("api/productos-categoria/{categoria}")]
        [HttpGet]
        public IQueryable<Producto> GetProductosCategoria(int categoria)
        {
            List<Producto> lstProducto = new List<Producto>();
            lstProducto = db.Productos.Where(s => s.visible == true && s.CategoriaId == categoria).Include(s => s.Categoria).Include(s => s.Marca).Include(s => s.Imagenes).ToList();
            return lstProducto.AsQueryable();
        }

        [Route("api/productos-categoria/{categoria}/{limit}")]
        [HttpGet]
        public IQueryable<Producto> GetProductosCategoria(int categoria,int limit)
        {
            List<Producto> lstProducto = new List<Producto>();
            lstProducto = db.Productos.Where(s => s.visible == true && s.CategoriaId == categoria).Include(s => s.Categoria).Include(s => s.Marca).Include(s => s.Imagenes).Take(limit).ToList();
            return lstProducto.AsQueryable();
        }

        // GET: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(int id)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(int id, Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.Id)
            {
                return BadRequest();
            }

            db.Entry(producto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productoes
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(producto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            db.Productos.Remove(producto);
            db.SaveChanges();

            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }
    }
}