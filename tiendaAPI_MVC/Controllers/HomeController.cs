using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace tiendaAPI_MVC.Controllers
{
    public class HomeController : Controller
    {
        const string endPoint = "http://localhost:1612/api/";

        public async Task<ActionResult> Index()
        {
            HttpClient hc = new HttpClient();
            //var jsonCategorias = await hc.GetStringAsync(endPoint+ "categorias");
            //List<Categoria> categoriasList = JsonConvert.DeserializeObject<List<Categoria>>(json);
            List<Producto> productosList = new List<Producto>();
            try
            {
                var json = await hc.GetStringAsync(endPoint + "productos-categoria/7/4");
                ViewBag.prodHombre = JsonConvert.DeserializeObject<List<Producto>>(json);

                json = await hc.GetStringAsync(endPoint + "productos-categoria/8/4");
                ViewBag.prodMujer = JsonConvert.DeserializeObject<List<Producto>>(json);

                json = await hc.GetStringAsync(endPoint + "productos-categoria/9/4");
                ViewBag.prodUnisex = JsonConvert.DeserializeObject<List<Producto>>(json);

                json = await hc.GetStringAsync(endPoint + "DetalleOrdenes/12");
                ViewBag.masVendidos = JsonConvert.DeserializeObject<List<DetalleOrden>>(json);
                
            }
            catch (TaskCanceledException ex)
            {
                TaskCanceledException e = ex;
            }
            catch (Exception ex)
            {
                Exception e = ex;
            }

            return View(productosList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}