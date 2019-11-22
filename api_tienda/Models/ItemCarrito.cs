using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class ItemCarrito
    {
        public Producto producto { set; get; }
        public int cantidad { set; get; }

    }
}