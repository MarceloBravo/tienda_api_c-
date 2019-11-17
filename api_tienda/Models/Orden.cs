using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Orden
    {
        private decimal Shipping = 0;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        [Required(ErrorMessage = "El sub-total es obligatorio.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El sub-total debe ser un valor positivo.")]
        public decimal SubTotal { set; get; }
                
        [Required(ErrorMessage = "El campo costo de envío es obligatorio.")]
        [Range(0.0, Double.MaxValue, ErrorMessage ="Los contos de envío no pueden ser valores negativos.")]
        public decimal shipping { set => Shipping = value; get => Shipping; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public int IdUsuario { set; get; }
        public Usuario Usuario { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime? Deleted_at { set; get; }

        public ICollection<DetalleOrden> DetalleOrden { set; get; }

        public ICollection<WebPayError> WebPayErrors { set; get; }

        public ICollection<WebPayTransaction> WebPayTransactions { set; get; }
        
    }
    /*
    class OrdenDbContext: DbContext
    {
        public DbSet<Orden> Ordenes { set; get; }
    }
    */
}