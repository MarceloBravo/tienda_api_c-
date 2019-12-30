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
    public class PaisController : ApiController
    {
        private TiendaContext db = new TiendaContext();
        private Paginacion<Pais> paginadorPais;

        // GET: api/Pais
        public IHttpActionResult GetPaises(int pagina = 1)
        {
            paginadorPais = new Paginacion<Pais>();
            //La cantidad de registros por página es 10 y viene por defecto al momento de contruir el objeto, este valor se puede cambiar en la propiedad paginadorPais.registrosPorPagina o en la declaración de la clase Paginacion

            paginadorPais.totalRegistros = db.Paises.Count();
            paginadorPais.totalPaginas = (int)Math.Ceiling(paginadorPais.totalRegistros / paginadorPais.registrosPorPagina);

            //Obteniendo los registros para la página 
            paginadorPais.registros = db.Paises.OrderBy(p => p.Nombre)
                                        .Skip((pagina - 1) * paginadorPais.registrosPorPagina)
                                        .Take(paginadorPais.registrosPorPagina)
                                        .ToList();
            return Json(paginadorPais);
            //return db.Paises;
        }


        // GET: api/Pais/5
        [ResponseType(typeof(Pais))]
        public IHttpActionResult GetPais(int id)
        {
            Pais pais = db.Paises.Find(id);
            if (pais == null)
            {
                return NotFound();
            }

            return Ok(pais);
        }

        // PUT: api/Pais/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPais(int id, Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pais.Id)
            {
                return BadRequest();
            }

            db.Entry(pais).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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

        // POST: api/Pais
        [ResponseType(typeof(Pais))]
        public IHttpActionResult PostPais(Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paises.Add(pais);
            db.SaveChanges();
            
            return CreatedAtRoute("DefaultApi", new { id = pais.Id }, pais);
        }

        // DELETE: api/Pais/5
        [ResponseType(typeof(Pais))]
        public IHttpActionResult DeletePais(int id)
        {
            Pais pais = db.Paises.Find(id);
            if (pais == null)
            {
                return NotFound();
            }

            db.Paises.Remove(pais);
            db.SaveChanges();

            return Ok(pais);
        }

        // GET: api/Pais/5
        [HttpGet]
        [Route("api/Pais/filtrar/{filtro}")]
        [ResponseType(typeof(Paginacion<Pais>))]
        public IHttpActionResult Filtrar(string filtro, int pagina = 1)
        {            
            paginadorPais = new Paginacion<Pais>();

            try
            {
                paginadorPais.totalRegistros = db.Paises.Count();
                paginadorPais.pagina = pagina;

                paginadorPais.registros = (from p in db.Paises
                                     where 
                                     (
                                        p.Nombre.Contains(filtro) || 
                                        p.Created_at.ToString().Contains(filtro) || 
                                        p.Updated_at.ToString().Contains(filtro) || 
                                        p.Deleted_at.ToString().Contains(filtro)                                        
                                     )
                                     select p)
                                     .OrderBy(p => p.Nombre)
                                     .Skip((pagina -1) * paginadorPais.registrosPorPagina)
                                     .Take(paginadorPais.registrosPorPagina)                                     
                                     .ToList();
                return Ok(paginadorPais);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
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


        private bool PaisExists(int id)
        {
            return db.Paises.Count(e => e.Id == id) > 0;
        }
    }
}