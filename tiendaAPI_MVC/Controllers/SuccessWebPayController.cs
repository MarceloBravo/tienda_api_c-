using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using tiendaAPI_MVC.Models;

namespace tiendaAPI_MVC.Controllers
{
    public class SuccessWebPayController : Controller
    {
        private static Dictionary<string, ItemCarrito> carrito = new Dictionary<string, ItemCarrito>();
        public ActionResult Index()
        {
            EnviarCorreoVenta();
            ViewBag.usuario = (Usuario)Session["USUARIO"];
            carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];
            return View(carrito);
        }

        private void EnviarCorreoVenta()
        {

        }
    }

   
}
