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

namespace tiendaAPI_MVC.Controllers.Admin
{   
    public class PaisController : Controller
    {
        const string endPoint = "http://localhost:1612/api/";

        /// <summary>
        /// Muestra el listado del mantenedor de Paises
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

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
                HttpClient hc = new HttpClient();
                string json = await hc.GetStringAsync(endPoint + "Pais?pagina=" + pagina);
                Paginacion<Pais> paises = JsonConvert.DeserializeObject<Paginacion<Pais>>(json);
                if (paises == null)
                {
                    throw new System.ArgumentException("No se encontraron registros");
                }
                var resp = Json(json, JsonRequestBehavior.AllowGet);
                return resp;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// Busca y retorna el registro a partir del id recibido cómo parametro
        /// </summary>
        /// <param name="id">Id o clave primaria del registro que se desea buscar</param>
        /// <returns>Un objeto json con el los datos del objeto encontrado o de lo contrario un mensaje en caso de que el abjeto no exista u ocurra algún error</returns>
        [HttpGet]
        public async Task<ActionResult> Show(int id)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");
            try
            {
                HttpClient cli = new HttpClient();
                var json = await cli.GetStringAsync(endPoint + "Pais/" + id.ToString());
                Pais pais = JsonConvert.DeserializeObject<Pais>(json);
                if(pais == null)
                {
                    throw new System.ArgumentException("El registro no existe o no fue encontrado.");
                }
                return Json(pais, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public async Task<ActionResult> Create([System.Web.Http.FromBody] Pais pais)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

            try
            {
                var json = JsonConvert.SerializeObject(pais);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(endPoint + "Pais/", stringContent); //Consultando los indicadores económicos
                Boolean resp = response.IsSuccessStatusCode;
                
                return Json("El registro ha sido ingresado.");
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, [System.Web.Http.FromBody] Pais pais)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

            try
            {                
                var json = JsonConvert.SerializeObject(pais);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PutAsync(endPoint + "Pais/" + id, stringContent); //Consultando los indicadores económicos
                Boolean resp = response.IsSuccessStatusCode;

                return Json("El registro ha sido actualizado.");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");
            
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.DeleteAsync(endPoint + "Pais/" + id);
                if(json.IsSuccessStatusCode )
                {
                    Response.StatusCode = 200;
                    return Json("El registro ha sido eliminado");
                }
                else
                {   
                    throw new System.ArgumentException("Ocurrió un error al intentar eliminar el registro.");
                }
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json("Error: " + ex.Message);
            }
        }



        [HttpGet]
        [Route("pais/filtrar/{filtro}")]
        public async Task<ActionResult> filtrar(string filtro, int pagina = 1)
        {
            //Validación de permisos de usuario
            if (Session["USUARIO"] == null)
                return Redirect("/Login");

            try
            {
                HttpClient cli = new HttpClient();
                string json = await cli.GetStringAsync(endPoint + "Pais/filtrar/" + filtro + "?pagina=" + pagina.ToString());
                Paginacion<Pais> paises = JsonConvert.DeserializeObject<Paginacion<Pais>>(json);
                if(paises == null)
                {
                    throw new System.ArgumentException("No se han encontrado registros.");
                }
                return Json(paises, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
