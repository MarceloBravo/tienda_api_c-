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
        const string endPoint = "http://localhost:1612/api/Categorias";

        public async Task<ActionResult> Index()
        {
            HttpClient hc = new HttpClient();
            var json = await hc.GetStringAsync(endPoint);
            List<Categoria> categoriasList = JsonConvert.DeserializeObject<List<Categoria>>(json);

            return View(categoriasList);
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