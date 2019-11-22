using api_tienda.DAL;
using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace api_tienda.Controllers
{
    public class CarritoController : ApiController
    {
        private static Dictionary<long, ItemCarrito> carrito = new Dictionary<long, ItemCarrito>();
        private TiendaContext db = new TiendaContext();

        
        [Route("Carrito/ahrehar-producto/{id}/{cantidad}")]
        [HttpGet]
        public IHttpActionResult AgregarProducto(long id, int cantidad)
        {
            try
            {
                if (id == 0) { throw new System.ArgumentException("Código de producto no válido."); }
                if (cantidad == 0) { throw new System.ArgumentException("Cantidad de producto no válida."); }
                Producto producto = db.Productos.Find(id);
                if(producto == null)
                {
                    ItemCarrito item = new ItemCarrito();
                    item = carrito[id];
                    if (item == null)
                    {
                        item.producto = producto;
                        item.cantidad = cantidad;
                        carrito.Add(id, item);
                    }
                    else
                    {
                        item.cantidad = item.cantidad + cantidad;
                    }
                }
                else
                {
                    throw new System.ArgumentException("El producto no fue encontrado.");
                }
            }catch(Exception e)
            {
                return (IHttpActionResult)e;
            }
            return (IHttpActionResult)carrito;
        }
    }
}
