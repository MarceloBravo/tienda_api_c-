using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tiendaAPI_MVC.Models
{
    public class ItemCarrito
    {
        public Producto producto {set; get;}
        public int cantidad { set; get; }
    }
}