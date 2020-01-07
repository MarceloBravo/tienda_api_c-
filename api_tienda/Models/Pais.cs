using Newtonsoft.Json;
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
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }

        [JsonIgnore]    //Evita el error por referencia circular entre la clase Region y la clase Pais al momento de desserializar el objeto en un Json
        public ICollection<Region> Regiones { set; get; }

        public Pais()
        {
            this.Updated_at = DateTime.Today;
        }
    }
    /*
    class PaisDbContext: DbContext
    {
        public DbSet<Pais> Paises { set; get; }
    }
    */
}