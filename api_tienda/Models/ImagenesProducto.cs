
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class ImagenesProducto
    {
        public bool Predeterminada = true;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required(ErrorMessage = "Debe especificar la ubicación de la imágen del producto.")]        
        public string Ubicacion { set; get; }
        public bool predeterminada {set => Predeterminada = value; get => Predeterminada;}
        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }

        public int IdProducto { set; get; }
        public Producto Producto { set; get; }

    }
    /*
    class ImagenesProductoDbContext: DbContext
    {
        public DbSet<ImagenesProducto> ImagenesProductos { set; get; }
    }
    */

}