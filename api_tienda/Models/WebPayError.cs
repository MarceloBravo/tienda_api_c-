using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class WebPayError
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        
        [Required(ErrorMessage = "El N° de orden es obligatorio.")]
        public long IdOrden { set; get; }
        public Orden Orden { set; get; }

        [Required(ErrorMessage = "El token de WebPay es obligatorio.")]
        public string Token_webpay { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }
    }
    /*
    class WebPayErrorDbContext: DbContext
    {
        public DbSet<WebPayError> WebPayErrors { set; get; }
    }
    */
}