using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Comuna
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required(ErrorMessage = "El nombre de la comuna es obligatorio.")]
        [StringLength(5, MinimumLength = 50, ErrorMessage = "El nombre de la comuna debe tener entre 5 y 50 carácteres")]
        public string Nombre { set; get; }
        [Required(ErrorMessage = "La región es obligatoria.")]
        public int IdRegion { set; get; }
        public Region Region { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }

        public ICollection<Ciudad> Ciudades { set; get; }
        
    }
    /*
    class ComunaDbContext: DbContext
    {
        public DbSet<Comuna> Comunas { set; get; }
    }
    */
}