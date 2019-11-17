
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        [Required(ErrorMessage = "Debe especificar la ubicación de la imágen del producto.")]        
        public string Ubicacion { set; get; }
        public bool predeterminada {set => Predeterminada = value; get => Predeterminada;}
        
        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }

        [Required(ErrorMessage = "El nombre del archivo es obligatorio")]
        public string nombreArchivo { set; get; }

        //[ForeignKey("Producto")]
        public long ProductoId { set; get; }        
        //public virtual Producto Producto { set; get; }

    }
    /*
    class ImagenesProductoDbContext: DbContext
    {
        public DbSet<ImagenesProducto> ImagenesProductos { set; get; }
    }
    */

}