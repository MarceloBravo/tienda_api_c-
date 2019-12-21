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
            //Asignando a las variables los datos para mostrarlos en la boleta
            ViewBag.usuario = (Usuario)Session["USUARIO"];
            ViewBag.numFactura = Session["numFactura"];
            ViewBag.ordenDeCompra = Session["ordenDeCompra"];
            ViewBag.shipping = Session["shipping"];
            ViewBag.buyOrder = Session["ordenDeCompra"];            
            ViewBag.carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];

            carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];           
            ViewBag.carrito = carrito.ToDictionary(item => item.Key, item => item.Value);   //Copiando el contenido del carrito de compras
            carrito.Clear();    //vaciando el carrito
            
            //Reseteando las variables de sessión de los datos de la compra
            Session["numFactura"] = null;
            Session["ordenDeCompra"] = null;
            Session["shipping"] = null;
            Session["ordenDeCompra"] = null;
            Session["carrito"] = carrito;

            return View();
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

            mensaje = "<html lang='es'>";
            mensaje += "    <head>";
            mensaje += "        <meta charset='UTF-8'>";
            mensaje += "        <meta name='viewport' content='width=device-width, user-scalable=no, initial-scale=1.0'>";
            mensaje += "        <title>Comprobante de compra</title>";
            mensaje += "    </head>";
            mensaje += "    <body>";
            mensaje += comprobante;
            mensaje += "    </body>";
            mensaje += "</html>";

            contrasena = ""; //Contraseña correo Live

            MailMessage correo = new MailMessage(usuario, destinatario, asunto, mensaje);
            correo.IsBodyHtml = true;   //Especifica que el mensaje que se está enviando corresponde a codigo HTML y debe ser interpretado como tal
            SmtpClient client = new SmtpClient("smtp.live.com");    //Cliente Smtp live (Utiliza correo live para el envio de emails)
            NetworkCredential credenciales = new NetworkCredential(usuario, contrasena);
            client.Credentials = credenciales;
            client.EnableSsl = true;

            try
            {
                client.Send(correo);
                correo.Dispose();

                CarritoController carrito = new CarritoController();
                carrito.VaciarCarro();
            }
            catch(Exception ex)
            {
                //ViewBag.Error = ex.Message;
                return Json(new { mensaje = "Ocurrió un error al intentar enviar el email. " + ex.Message });
            }

            

            return Json(new { mensaje = "El correo de finalización de compra ha sido enviado exitosamente" });
        }
    }
}
