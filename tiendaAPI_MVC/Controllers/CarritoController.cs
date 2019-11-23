using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tiendaAPI_MVC.Models;

namespace tiendaAPI_MVC.Controllers
{
    public class CarritoController: Controller
    {
        private static Dictionary<string, ItemCarrito> carrito = new Dictionary<string, ItemCarrito>();
        const string endPoint = "http://localhost:1612/api/";
        
        public ActionResult Index()
        {
            return View(carrito);
        }

        [HttpGet]       
        [Route("carrito/agregar-producto/{id}/{cantidad}")]
        public async Task<ActionResult> AgregarProducto(long id, int cantidad)
        {
            HttpClient hc = new HttpClient();
            try
            {
                if (id == 0) { throw new System.ArgumentException("Código de producto no válido."); }
                if (cantidad == 0) { throw new System.ArgumentException("Cantidad de producto no válida."); }

                var json = await hc.GetStringAsync(endPoint + "productos/"+id);
                Producto producto = JsonConvert.DeserializeObject<Producto>(json);
                
                if (producto != null)
                {
                    ItemCarrito item = new ItemCarrito();                    
                    if (!carrito.ContainsKey(id.ToString()))
                    {                        
                        item.producto = producto;
                        item.cantidad = cantidad;
                        carrito.Add(id.ToString(), item);
                    }
                    else
                    {
                        item = carrito[id.ToString()];
                        item.cantidad = item.cantidad + cantidad;
                    }
                }
                else
                {
                    throw new System.ArgumentException("El producto no fue encontrado.");
                }
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
            }
            return Json(carrito, JsonRequestBehavior.AllowGet);
        }
    }
}