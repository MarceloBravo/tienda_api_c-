using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Pais
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required(ErrorMessage = "El nombre del pais es obligatorio.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El nombre del pais debe tener entre 5 y 50 carácteres.")]
        public string Nombre { set; get; }
        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }

        public ICollection<Region> Regiones { set; get; }
    }
    /*
    class PaisDbContext: DbContext
    {
        public DbSet<Pais> Paises { set; get; }
    }
    */
}