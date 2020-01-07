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

//Controlador WEB API 2 con acciones de Entity Framework
namespace api_tienda.Controllers
{
    public class RegionesController : ApiController
    {
        private TiendaContext db = new TiendaContext();
        private Paginacion<Region> paginacion;

        // GET: api/Regiones
        public IHttpActionResult GetRegiones(int pagina)
        {
            paginacion = new Paginacion<Region>();
            paginacion.pagina = pagina;
            paginacion.totalRegistros = db.Regiones.Count();
            paginacion.totalPaginas = (int)Math.Ceiling(paginacion.totalRegistros / paginacion.registrosPorPagina);

            paginacion.registros = db.Regiones.OrderBy(r => r.Nombre)                                
                                .Skip((pagina - 1) * paginacion.registrosPorPagina)
                                .Take(paginacion.registrosPorPagina)
                                .Include(p => p.Pais)
                                .ToList();
            var resp = Json(paginacion);
            return resp;

            //return db.Regiones;
        }

        // GET: api/Regiones/5
        [ResponseType(typeof(Paginacion<Region>))]
        public IHttpActionResult GetRegion(int id)
        {
            Region region = db.Regiones.Find(id);
            if (region == null)
            {
                return Json("");
            }

            return Json(region);
        }

        // PUT: api/Regiones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegion(int id, Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != region.Id)
            {
                return BadRequest();
            }

            db.Entry(region).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regiones
        [ResponseType(typeof(Region))]
        public IHttpActionResult PostRegion(Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Regiones.Add(region);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = region.Id }, region);
        }

        // DELETE: api/Regiones/5
        [ResponseType(typeof(Region))]
        public IHttpActionResult DeleteRegion(int id)
        {
            Region region = db.Regiones.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            db.Regiones.Remove(region);
            db.SaveChanges();

            return Ok(region);
        }

        [HttpGet]
        [Route("api/Regiones/filtrar/{filtro}/{pagina}")]
        [ResponseType(typeof(Paginacion<Region>))]
        public IHttpActionResult Filtrar(string filtro, int pagina = 1)
        {
            paginacion = new Paginacion<Region>();

            try
            {
                paginacion.totalRegistros = db.Paises.Count();
                paginacion.pagina = pagina;

                paginacion.registros = (from p in db.Regiones
                                           where
                                           (
                                              p.Nombre.Contains(filtro) ||
                                              p.Pais.Nombre.Contains(filtro) ||
                                              p.Created_at.ToString().Contains(filtro) ||
                                              p.Updated_at.ToString().Contains(filtro) ||
                                              p.Deleted_at.ToString().Contains(filtro)
                                           )
                                           select p)
                                     .OrderBy(p => p.Nombre)
                                     .Skip((pagina - 1) * paginacion.registrosPorPagina)
                                     .Take(paginacion.registrosPorPagina)
                                     .Include(p => p.Pais)
                                     .ToList();
                return Json(paginacion);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionExists(int id)
        {
            return db.Regiones.Count(e => e.Id == id) > 0;
        }
    }
}