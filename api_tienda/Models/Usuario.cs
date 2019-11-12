using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Usuario
    {
        private bool Activo = true;
        private string Fono = "";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 yt 50 carácteres.")]
        public string Nombre { set; get; }

        [Required(ErrorMessage = "El campo apellido paterno es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido paterno debe tener entre 3 yt 50 carácteres.")]
        public string APaterno { set; get; }

        [Required(ErrorMessage = "El campo apellido materno es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El apellido materno debe tener entre 3 yt 50 carácteres.")]
        public string AMaterno { set; get; }

        [Required(ErrorMessage = "El campo email es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El correeo electrónico debe tener un másximo de 255 carácteres. Ingrese un email más corto.")]
        [EmailAddress(ErrorMessage = "El correo electrónico ingresado, no es válido.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "El nickname o nombre de usuario es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El nickname debe tener entre 5 y 20 carácteres.")]
        public string Nickname { set; get; }

        [Required(ErrorMessage = "Debe ingresar la contraseña del usuario.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña ha de tener entre 6 y 20 carácteres.")]        
        public string Password { set; get; }

        [Required(ErrorMessage = "El campo activo es obligatorio.")]
        [Range(typeof(bool), "true", "false", ErrorMessage = "El campo activo sólo acepta valores como Verdadero o Falso.")]
        public bool activo { set => Activo = value; get => Activo; }

        [Required(ErrorMessage = "La dirección del usuario es obligatoria.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 255 carácteres.")]
        public string Direccion { set; get; }
        
        [MaxLength(ErrorMessage = "El fono no debe superar los 15 carácteres.")]
        public string fono { set => Fono = value; get => Fono; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public int IdRol { set; get; }
        public Rol Rol { set; get; }

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public int IdCiudad { set; get; }
        public Ciudad Ciudad { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set => Updated_at = DateTime.Today; get => Updated_at; }
        public DateTime Deleted_at { set; get; }
    }
    /*
    class UsuarioDbContext: DbContext
    {
        public DbSet<Usuario> Usuarios { set; get; }
    }
    */
}