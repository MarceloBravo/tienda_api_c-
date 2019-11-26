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
using System.Web.Script.Serialization;
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
                Login loginData = new Login()
                {
                    nicknameOrEmail = nickname,
                    password = password
                };
                //HttpContent content = new StringContent(loginData.ToString(), Encoding.UTF8, "application/json");
                HttpClient hc = new HttpClient();
                var response = hc.PostAsJsonAsync(endPoint + "login/autenticar", loginData).Result;

                Usuario usuario = new Usuario();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<Usuario>(result);
                    Session["USUARIO"] = usuario;
                }

                if (usuario.Nombre == null)
                {
                    throw new System.ArgumentException("Usuario o contraseña no existente.");
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message); 
            }
            return Json(true);
        }
             
    }
}
