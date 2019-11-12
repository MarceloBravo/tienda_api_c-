using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Rol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El nombre del rol debe tener entre 5 y 50 carácteres.")]
        public string Nombre { set; get; }
        [Required(ErrorMessage = "La descripción del rol es obligatoria.")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "La descripción del rol debe tener entre 10 y 255 carácteres.")]
        public string Descripcion { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }

        public ICollection<Usuario> Usuarios { set; get; }
    }
    /*
    class RolDbContext: DbContext
    {
        public DbSet<Rol> Roles { set; get; }
    }
    */
}