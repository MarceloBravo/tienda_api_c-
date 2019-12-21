using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

//Controlador MVC 5: en blanco
namespace tiendaAPI_MVC.Controllers
{
    public class ProductoController : Controller
    {
        private string endPoint = "http://localhost:1612/api/";
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> buscar(int id)
        {
            var resp = "";
            Producto producto = new Producto();
            try
            {
                HttpClient cli = new HttpClient();
                var json = await cli.GetStringAsync(endPoint + "productos/" + id);
                producto = JsonConvert.DeserializeObject<Producto>(json);
                if (producto == null)
                {
                    throw new System.ArgumentException("El producto no fue endontrado");
                }
                
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(producto, JsonRequestBehavior.AllowGet);
            //return Json("abc", JsonRequestBehavior.AllowGet);
        }
    }
}