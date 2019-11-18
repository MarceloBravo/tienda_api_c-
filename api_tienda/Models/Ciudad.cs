using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Ciudad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required(ErrorMessage = "El nombre de la ciudad es obligatorio.")]
        [StringLength(5, MinimumLength =50, ErrorMessage = "El nombre de la ciudad es obligatorio.")]
        public string Nombre { set; get; }
        [Required(ErrorMessage = "La comuna es obligatoria")]
        public int IdComuna { set; get; }
        public Comuna Comuna { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime? Deleted_at { set; get; }

        public ICollection<Usuario> Usuarios { set; get; }
    }
    /*
    class CiudadDbContext: DbContext
    {
        public DbSet<Ciudad> Ciudades { set; get; }
    }
    */

}