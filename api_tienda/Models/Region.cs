using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la región.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la región debe tener entre 3 y 50 carácteres.")]
        public string Nombre { set; get; }
        [Required(ErrorMessage = "El páis es obligatorio.")]

        [ForeignKey("Pais")]    //Como el nombre no sigue la nomenclatura del framework para las claves foraneas, esque se debe especificar la clase asociadad a la clave esterna
        public int IdPais { set; get; }
        public virtual Pais Pais { set; get; }

        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set; get; } = DateTime.Today;
        public DateTime? Deleted_at { set; get; }

        [JsonIgnore]    //Evita el error por referencia circular entre la clase Region y la clase Pais al momento de desserializar el objeto en un Json
        public ICollection<Comuna> Comunas { set; get; }
        
    }
    /*
    class RegionDbContext: DbContext
    {
        public DbSet<Region> Regiones { set; get; }
    }
    */
}