using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class TallasProducto
    {
        public long Id { set; get; }
        public long Producto_id { set; get; }
        //public ICollection<Producto> Productos { set; get; }
        public long Talla_id { set; get; }
        //public ICollection<Talla> Tallas { set; get; }

        public TallasProducto()
        {
            //this.Productos = new HashSet<Producto>();
            //this.Tallas = new HashSet<Talla>();
        }
    }
}