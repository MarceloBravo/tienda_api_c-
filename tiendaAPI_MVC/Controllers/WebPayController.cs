using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using tiendaAPI_MVC.Models;
using Transbank.Webpay;

namespace tiendaAPI_MVC.Controllers
{
    public class WebPayController : Controller
    {
        private static decimal gastosEnvio;
        private static string buyOrder;
        private static Usuario usuario;
        private static Dictionary<string, ItemCarrito> carrito;

        public ActionResult EfectuarPago()
        {
            gastosEnvio = 0;
            decimal amount = total() + gastosEnvio;
            buyOrder = new Random().Next(100000, 999999999).ToString();
            string sessionId = "";
            string returnUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/WebPay/ReturnUrl";
            string finalUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/WebPay/FinalUrl";
            usuario = (Usuario)Session["USUARIO"];
            carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];

            var formAction = "";
            var tokens = "";

            try
            {
                var configuration = Configuration.ForTestingWebpayPlusNormal();
                //configuration.Environment = "INTEGRACION";
                //configuration.CommerceCode = "597020000540";
                //configuration.PrivateCertPfxPath = @"C:/Path/to/private/Cert.pfx";
                //configuration.Password = "PfxPassword";

                var transaction = new Webpay(configuration).NormalTransaction;
                var initResult = transaction.initTransaction(amount, buyOrder, sessionId, returnUrl, finalUrl);
                
                formAction = initResult.url;    //La redirección a esta url debe ser vía POST
                tokens = initResult.token;                
                //    RegistrarTransaccion();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Redirect("Error");
            }
            ViewBag.formAction = formAction;
            ViewBag.token = tokens;            
            return View();
        }

        private decimal total()
        {
            //Obteneiendio los productos delk carrito de compra y agregandolos a la lista de compra de Paypal
            Dictionary<string, ItemCarrito> carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];
            decimal resp = 0;

            foreach(var item in carrito)
            {
                resp += item.Value.producto.Precio * item.Value.cantidad;
            }

            return resp;
        }

        //URL del comercio, a la cual Webpay redireccionará posterior al proceso de autorización. Largo máximo: 256
        public async Task<ActionResult> ReturnUrl()
        {
            Session["USUARIO"] = usuario;
            Session["carrito"] = carrito;
            Session["shipping"] = gastosEnvio;
            Session["numFactura"] = await Util.GenerarNuevoNumeroBoleta();
            Session["ordenDeCompra"] = buyOrder;

            return Redirect("/SuccessWebPay/Index");
        }

        //URL del comercio a la cual Webpay redireccionará posterior al voucher de éxito de Webpay. Largo máximo 256
        public ActionResult finalUrl()
        {
            return View();
        }

        public Boolean RegistrarTransaccion()
        {
            Boolean resp = false;

            return resp;
        }

        public ActionResult Error()
        {
            return Redirect("/Shared/Error");
        }
    }
}
