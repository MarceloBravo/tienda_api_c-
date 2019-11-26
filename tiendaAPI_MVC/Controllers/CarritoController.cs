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
            try
            {
                if (id == 0) { throw new System.ArgumentException("Código de producto no válido."); }
                if (cantidad == 0) { throw new System.ArgumentException("Cantidad de producto no válida."); }
                if(cantidad >0 )
                {
                    HttpClient hc = new HttpClient();
                    var json = await hc.GetStringAsync(endPoint + "productos/" + id);
                    Producto producto = JsonConvert.DeserializeObject<Producto>(json);
                    if(producto == null)
                    {
                        throw new System.ArgumentException("El producto no fue encontrado.");
                    }
                    this.agregarProductoAlCarrito(producto, cantidad);
                }
                else
                {
                    this.sumarCantidad(id, cantidad);       
                }
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
            }
            return Json(carrito, JsonRequestBehavior.AllowGet);
        }


        private void agregarProductoAlCarrito(Producto producto, int cantidad)
        {
            ItemCarrito item = new ItemCarrito();
            if (!carrito.ContainsKey(producto.Id.ToString()))
            {
                item.producto = producto;
                item.cantidad = cantidad;
                carrito.Add(producto.Id.ToString(), item);
            }
            else
            {
                this.sumarCantidad(producto.Id, cantidad);
            }
        }


        private void sumarCantidad(long id, int cantidad)
        {
            ItemCarrito item = carrito[id.ToString()];            
            item.cantidad += cantidad;
        }

        [HttpGet, Route("carrito/productos")]
        public ActionResult GetCarrito()
        {
            return Json(carrito, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public ActionResult EfectuarPagoCompra()
        {
            if(Session["USUARIO"] == null)
            {
                return Redirect("/login");
            }
            return View(carrito);
        }

        [HttpGet]
        public ActionResult VaciarCarrito()
        {
            carrito.Clear();
            return RedirectToAction( "Index",new { carrito = carrito });
        }
    }
}