using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Oferta
    {
        private bool Portada = true;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required(ErrorMessage = "La imágen es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La ruta a la imágen es demasiado larga.")]
        public string src_imagen { set; get; }

        [Required(ErrorMessage = "El texto de la oferta es obligatorio.")]
        [StringLength(30, ErrorMessage = "El texto no debe sobrepasar lo 30 carácteres.")]
        public string Texto1 { set; get; }

        [StringLength(45, ErrorMessage = "El texto 2 no debe sobrepasar lo 30 carácteres.")]
        public string Texto2 { set; get; }

        public DateTime FechaExpiracion { set; get; }

        public bool portada { set => Portada = value; get => Portada; }


        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }
    }
    /*
    class OfertaDbContext: DbContext
    {
        public DbSet<Oferta> Ofertas { set; get; }
    }
    */
}