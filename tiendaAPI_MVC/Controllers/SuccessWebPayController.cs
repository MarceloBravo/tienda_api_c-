using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Mvc;
using tiendaAPI_MVC.Models;

namespace tiendaAPI_MVC.Controllers
{
    public class SuccessWebPayController : Controller
    {
        private static Dictionary<string, ItemCarrito> carrito = new Dictionary<string, ItemCarrito>();
        public ActionResult Index()
        {
            ViewBag.usuario = (Usuario)Session["USUARIO"];
            ViewBag.NumFactura = Session["numFactura"];
            carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];
            return View(carrito);
        }
        

        //Envío de comprobante de venta por correo
        [HttpPost]
        public ActionResult EnviarCorreoVenta(string comprobante)
        {
            string usuario, destinatario, mensaje, asunto, contrasena;
            Usuario usuarioSession = (Usuario)Session["USUARIO"];
            usuario = "xxx@live.cl";   //Correo electrónico Live
            destinatario = usuarioSession.Email;
            asunto = "Comprobante de venta";
            mensaje = comprobante;  
            contrasena = ""; //Contraseña correo Live

            MailMessage correo = new MailMessage(usuario, destinatario, asunto, mensaje);
            SmtpClient client = new SmtpClient("smtp.live.com");    //Cliente Smtp live (Utiliza correo live para el envio de emails)
            NetworkCredential credenciales = new NetworkCredential(usuario, contrasena);
            client.Credentials = credenciales;
            client.EnableSsl = true;

            try
            {
                client.Send(correo);
                correo.Dispose();
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Redirect("/");
            }

            return Redirect("/");
        }

    }
}
