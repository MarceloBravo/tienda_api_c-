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
        private string endPoint = "http://localhost:1612/api/";
        private static Webpay transaction;
        private static decimal ammount;
        private static Configuration configuration;

        public async Task<ActionResult> EfectuarPago()
        {
            gastosEnvio = 0;
            ammount = total() + gastosEnvio;
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
                configuration = Configuration.ForTestingWebpayPlusNormal();
                //configuration.Environment = "INTEGRACION";
                //configuration.CommerceCode = "597020000540";
                //configuration.PrivateCertPfxPath = @"C:/Path/to/private/Cert.pfx";
                //configuration.Password = "PfxPassword";

                transaction = new Webpay(configuration);
                var normalTransaction = transaction.NormalTransaction;
                var initResult = normalTransaction.initTransaction(ammount, buyOrder, sessionId, returnUrl, finalUrl);
                
                formAction = initResult.url;    //La redirección a esta url debe ser vía POST
                tokens = initResult.token;                
                //await RegistrarTransaccion(transaction, tokens, amount, configuration.CommerceCode);
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
        public async Task<ActionResult> ReturnUrl(string token_ws)
        {
            await RegistrarTransaccion(transaction, token_ws, ammount, configuration.CommerceCode);
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

        public async Task<Boolean> RegistrarTransaccion(Webpay transaction, string token, decimal total, string commerceCode)
        {
            Boolean resp = false;
            try
            {   
                var transactionResult = transaction.NormalTransaction.getTransactionResult(token);
                Transaction ts = new Transaction();
                ts.AccountingDate = DateTime.Today.Millisecond;
                ts.BuyOrder = transactionResult.buyOrder;
                ts.CardNumber = transactionResult.cardDetail.cardNumber;
                ts.CardExpirationDate = transactionResult.cardDetail.cardExpirationDate;
                ts.AuthorizationCode = 0;
                ts.PaymentTypeCode = null;
                ts.ResponseCode = 0;
                ts.SharedNumber = 0;
                ts.Ammount = total;
                ts.CommerceCode = commerceCode;
                ts.TransactionDate = transactionResult.transactionDate.ToString();
                ts.VCI = transactionResult.VCI;
                ts.Token = token;
                ts.Created_at = DateTime.Today;
                ts.Updated_at = DateTime.Today;
                
                HttpClient hc = new HttpClient();
                //var jsonContent = JsonConvert.SerializeObject(ts);
                //HttpContent content = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await hc.PostAsJsonAsync(this.endPoint + "Transactions", ts);
                resp = response.IsSuccessStatusCode;

            }catch(Exception ex)
            {
                resp = false;
            }
            return resp;
        }

        public ActionResult Error()
        {
            return Redirect("/Shared/Error");
        }
    }
}
