using api_tienda.DAL;
using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;

namespace api_tienda.Controllers
{
    public class LoginController : ApiController
    {        
        private TiendaContext db = new TiendaContext();


        //[ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult Autenticar([FromBody]Login loginData)
        {
            Usuario user = new Usuario();
            try
            {
                user = db.Usuarios.FirstOrDefault(u => u.Nickname.Equals(loginData.nicknameOrEmail) || u.Email.Equals(loginData.nicknameOrEmail));

                if(user == null)
                    return BadRequest("Usuario no existente.");

                if(Crypto.VerifyHashedPassword(user.Password, loginData.password))
                    return Ok(user);

                throw new System.ApplicationException("Contraseña incorrecta.");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
