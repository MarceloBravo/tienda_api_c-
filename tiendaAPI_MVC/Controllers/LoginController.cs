using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace tiendaAPI_MVC.Controllers
{
    public class LoginController : Controller
    {
        const string endPoint = "http://localhost:1612/api/";
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Autenticar(string nickname, string password)
        {
            try
            {
                /*
                using (HttpClient hc = new HttpClient())
                {
                    hc.BaseAddress = new Uri(endPoint + "login/autenticar");
                    var postResponse = await hc.PostAsJsonAsync(nickname, password);
                    postResponse.Await();
                    var result = postResponse.Result;
                }
                */
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("nicknameOrEmail",nickname);
                param.Add("password",password);

                HttpClient hc = new HttpClient();
                var resp = await hc.PostAsJsonAsync(endPoint + "login/autenticar", param);
                resp.EnsureSuccessStatusCode();

                var json = JsonConvert.SerializeObject(param);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await hc.PostAsync(endPoint + "login/autenticar", content).ConfigureAwait(false);
                Usuario usuario = new Usuario();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<Usuario>(result);
                    Session["USUARIO"] = usuario;
                }

                if(usuario.Nombre == null)
                {
                    throw new System.ArgumentException("Usuario o contraseña no existente.");
                }
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message); 
            }
            return Redirect("/carrito/EfectuarPagoCompra");
        }
             
    }
}
