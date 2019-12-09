using api_tienda.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using tiendaAPI_MVC.Models;


/*
 * Antes de implementar estos métodos se require instalar los paquetes Paypal y log4Net
 * install-package Paypal
 * install-package log4net
 * 
 * Luego configurar el archivo web.config ubicado en la raiz con el contenido que actualmente contiene
 * Finalmente crear el modelo PaypalConfiguration
 */


namespace tiendaAPI_MVC.Controllers
{
    public class PaypalController : Controller
    {
        private PayPal.Api.Payment payment;

        public ActionResult PaymentWithCreditCard()
        {
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = "Demo Item";
            item.currency = "USD";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = "NewYork";
            billingAddress.country_code = "US";
            billingAddress.line1 = "23rd street kew gardens";
            billingAddress.postal_code = "43210";
            billingAddress.state = "NY";


            //Now Create an object of credit card and add above details to it
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874";  //card cvv2 number
            crdtCard.expire_month = 1; //card expire date
            crdtCard.expire_year = 2020; //card expire year
            crdtCard.first_name = "Aman";
            crdtCard.last_name = "Thakur";
            crdtCard.number = "1234567890123456"; //enter your credit card number here
            crdtCard.type = "visa"; //credit card type here paypal allows 4 types

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5";
            details.tax = "1";

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total = shipping tax + subtotal.
            amnt.total = "7";
            amnt.details = details;

            // Now make a transaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Description about the payment amount.";
            tran.item_list = itemList;
            tran.invoice_number = "your invoice number which you are generating";

            // Now, we have to make a list of transaction and add the transactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal
                //basically we are sending the clientID and clientSecret key in this function
                //to the get the context from the paypal API to make the payment
                //for which we have created the object above.

                //Basically, apiContext object has a accesstoken which is sent by the paypal
                //to authenticate the payment to facilitator account.
                //An access token could be an alphanumeric string

                APIContext apiContext = PaypalConfiguration.GetAPIContext();

                //Create is a Payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successful else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }
            }
            catch (PayPal.PayPalException ex)
            {
                //Logger.Log("Error: " + ex.Message);
                return View("FailureView");
            }
            return Redirect("/SuccessWebPay");
            //return View("SuccessView");
        }


        public async Task<ActionResult> PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    decimal dollar = await GetDollar();
                    if(dollar == -1)
                    {
                        throw new System.ArgumentException("Ocurrió un error al intemntar obtener el valor del dollar.");
                    }
                    long numDocumento = await GetNumDocumento();
                    if (numDocumento == -1)
                    {
                        throw new System.ArgumentException("Ocurrió un error al intemntar obtener el numero del documento a emitir.");
                    }
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, dollar, numDocumento);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                    else
                    {
                        if (await RegistrarVenta(executedPayment))
                            Session["numFactura"] = executedPayment.transactions[0].invoice_number;
                    }

                }
            }
            catch (Exception ex)
            {
                //Logger.log("Error" + ex.Message);
                return View("FailureView");
            }

            

            return Redirect("/SuccessWebPay");
            //return View("SuccessView");
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, decimal dollar = 0, long numDocumento = 0)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            //Obteneiendio los productos delk carrito de compra y agregandolos a la lista de compra de Paypal
            Dictionary<string, ItemCarrito> carrito = (Dictionary<string, ItemCarrito>)Session["carrito"];
            decimal total = 0;
            decimal impuestos = 0;
            decimal gastosEnvio = 1;
            long numFactura = numDocumento;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            System.IFormatProvider cultureUS = new System.Globalization.CultureInfo("en-US");


            foreach (var item in carrito)
            {
                itemList.items.Add(new Item() {
                    name = item.Value.producto.Nombre,
                    currency = "USD",
                    price = Math.Round(item.Value.producto.Precio / dollar, 2).ToString(nfi),
                    quantity = item.Value.cantidad.ToString(),
                    sku = "sku"
                });
                total += Math.Round((item.Value.producto.Precio / dollar) * item.Value.cantidad, 2);
            }


            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = impuestos.ToString(),  //Impuesto
                shipping = gastosEnvio.ToString(nfi), //Gastos de envío
                subtotal = total.ToString(nfi)
            };
            string strTotal = Math.Round(total + impuestos + gastosEnvio, 2).ToString(nfi);
            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = strTotal, // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Venta de productos tienda C#.",
                invoice_number = numFactura.ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        //Obtiene el valor del dollar al día 
        private async Task<decimal> GetDollar()
        {
            decimal dollar = 0;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://mindicador.cl/api"); //Consultando los indicadores económicos
                if (response.IsSuccessStatusCode)
                {
                    var indicador = await response.Content.ReadAsAsync<Object>();   //Recuperando la información
                    var obj = JObject.Parse(indicador.ToString());  //Convirtiendo a Objeto
                    dollar = decimal.Parse(obj.GetValue("dolar")["valor"].ToString());  //Recuperando el valor del dollar
                }
            }catch(Exception ex){
                dollar = -1;    //En caso de error se devolvera -!
            }
            return dollar;
        }

        //Obtiene el último número correlativo de boleta
        private async Task<long> GetNumDocumento()
        {
            long numDocumento = 15;
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync("http://localhost:1612/api/LatsWebPayTransaction");
                WebPayTransaction transaccion = JsonConvert.DeserializeObject<WebPayTransaction>(json);
                if(transaccion != null)
                    numDocumento = long.Parse(transaccion.IdOrden.ToString()) + 1;

                //HttpResponseMessage response = await client.GetAsync("http://localhost:1612/api/LatsWebPayTransaction");
                /*if (response.IsSuccessStatusCode)
                {
                    WebPayTransaction wpt = JsonConvert.DeserializeObject<WebPayTransaction>(response.ToString());
                    if(wpt != null)
                        numDocumento = long.Parse(wpt.IdOrden.ToString()) + 1;
                }
                */
            }
            catch(Exception ex)
            {
                numDocumento = -1;
            }
            return numDocumento;
        }


        private async Task<Boolean> RegistrarVenta(Payment payment)
        {
            Boolean resp = false;
            try
            {
                WebPayTransaction wpt = new WebPayTransaction();
                wpt.IdOrden = long.Parse(payment.transactions[0].invoice_number);
                wpt.Orden = null;
                wpt.AccountingDate = DateTime.Now.Millisecond;
                wpt.BuyOrder = payment.transactions[0].invoice_number;
                wpt.CardNumber = null;
                wpt.CardExpirationDate = null;
                wpt.AuthorizationCode = 0;
                wpt.PaymentTypeCode = payment.payer.payment_method;
                wpt.ResponseCode = 0;
                wpt.SharedNumber = 0;
                wpt.Ammount = decimal.Parse(payment.transactions[0].amount.total.ToString());
                wpt.CommerceCode = payment.transactions[0].payee.merchant_id;
                wpt.TransactionDate = payment.create_time;
                wpt.VCI = "";
                wpt.Token = payment.token;
                wpt.Created_at = DateTime.Today;
                wpt.Updated_at = DateTime.Today;
                
                
                var json = JsonConvert.SerializeObject(wpt);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("http://localhost:1612/api/WebPayTransactions", stringContent); //Consultando los indicadores económicos
                resp = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                resp = false;
            }
            return resp;
        }
    }
}
