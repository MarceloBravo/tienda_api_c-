using api_tienda.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace tiendaAPI_MVC.Controllers
{
    public static class Util
    {
        //Obtiene el último número correlativo de boleta
        public static async Task<long> GenerarNuevoNumeroBoleta()
        {
            long numDocumento = 15;
            try
            {
                HttpClient client = new HttpClient();
                var json = await client.GetStringAsync("http://localhost:1612/api/LatsWebPayTransaction");
                WebPayTransaction transaccion = JsonConvert.DeserializeObject<WebPayTransaction>(json);
                if (transaccion != null)
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
            catch (Exception ex)
            {
                numDocumento = -1;
            }
            return numDocumento;
        }        
    }
}