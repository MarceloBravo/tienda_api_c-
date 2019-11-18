using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class DetalleOrden
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        [Required(ErrorMessage = "No se ha especificado un producto.")]
        public int IdProducto { set; get; }
        public Producto Producto { set; get; }
        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El prexcio del producto no puede ser un valor negativo.")]
        public decimal Precio { set; get; }

        [Required(ErrorMessage = "Debe especificar la cantidad de producto.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "La cantidad de producto no puede ser negativa.")]
        public int Cantidad { set; get; }

        [Required(ErrorMessage = "Se debe especificar el N° de orden.")]
        public long IdOrden { set; get; }
        public Orden Orden { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime? Deleted_at { set; get; }
    }
}