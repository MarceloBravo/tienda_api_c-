using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

//Controlador WEB API 2 en blanco
namespace tiendaAPI_MVC.Controllers.Admin
{
    public class RegionesController : Controller
    {
        private string endPoint = "http://localhost:1612/api/";
        private IEnumerable<SelectListItem> ListaPaises;
        
        public ActionResult Index()
        {
            if (Session["USUARIO"] == null)
                return Redirect("/login");

            return View();
        }
        
        

        
        /// <summary>
        /// Retorna el listado de paises en formato Json
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Listar(int pagina)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

            try
            {
                HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(endPoint + "regiones?pagina=" + pagina);
                Paginacion<Region> paginacion = JsonConvert.DeserializeObject<Paginacion<Region>>(json);
                if(paginacion == null)
                {
                    throw new System.ArgumentException("No se encontraron registros");
                }

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        
        public async Task<ActionResult> Filtrar(string filtro, int pagina = 1)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

            try
            {
                HttpClient hc = new HttpClient();
                string json = await hc.GetStringAsync(endPoint + "Regiones/filtrar/"+filtro+"/"+pagina);
                Paginacion<Region> paginacion = JsonConvert.DeserializeObject<Paginacion<Region>>(json);
               if(paginacion == null)
                {
                    throw new System.ArgumentException("No se han encontrado registros.");
                }

                return Json(json, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
                
        public async Task<ActionResult> Form()
        {
            await ListarPaises(0);
            ViewBag.region = new Region();
            ViewBag.paises = ListaPaises;
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            try
            {
                //Obteniendo el registro a buscar
                HttpClient cli = new HttpClient();
                string json = await cli.GetStringAsync(endPoint + "regiones/" + id);
                Region region = JsonConvert.DeserializeObject<Region>(json);

                ViewBag.region = region ?? throw new System.ArgumentException("Registro no encontrado");

                await ListarPaises(region.IdPais);
                ViewBag.paises = this.ListaPaises; 
                
                return View("form");
            }catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return Redirect("/regiones");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Insertar([System.Web.Http.FromBody] Region region)
        {
            try
            {
                region.Created_at = DateTime.Now;
                var json = JsonConvert.SerializeObject(region);
                HttpContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpClient cli = new HttpClient();
                HttpResponseMessage resp = await cli.PostAsync(endPoint + "regiones", content);
                if(!resp.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException("Ocurrio un error al intentar crear el registro.");
                }

                return Json("El registro ha sido creado.");
            } catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Actualizar(int id, [System.Web.Http.FromBody] Region region) 
        {
            try
            {
                var json = JsonConvert.SerializeObject(region);
                HttpContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpClient cli = new HttpClient();
                HttpResponseMessage resp = await cli.PutAsync(endPoint + "regiones/" + id, content);
                if(!resp.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException("Ocurrio un error al intentar actualizar el registro");
                }

                return Json("El registro ha sido actualizado.");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }

        

        [HttpDelete]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                HttpClient cli = new HttpClient();
                HttpResponseMessage resp = await cli.DeleteAsync(endPoint + "Regiones/" + id);
                if (!resp.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException("Ocurrio un error al intentar eliminar el registro");
                }

                return Json("El registro ha sido eliminado");
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }
        

        private async Task<IEnumerable<SelectListItem>> ListarPaises(int selected)
        {
            //Generando el listado de elementos para el control Select de paises
            HttpClient cli = new HttpClient();
            var jsonPaises = await cli.GetStringAsync(endPoint + "paisList");
            List<Pais> paises = JsonConvert.DeserializeObject<List<Pais>>(jsonPaises);
            IEnumerable<SelectListItem> lstPaises = paises.Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString(),
                Selected = x.Id == selected
            });
            ListaPaises = lstPaises;

            return null;
        }
        
        
    }
}
