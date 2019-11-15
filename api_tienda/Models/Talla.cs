using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Talla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TallaId { set; get; }
        [Required(ErrorMessage = "Debe ingresar el nombre o descripción de la talla.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "La talla debe tener entre 1 y 10 carácteres.")]
        public string Nombre{ set; get; }
        public DateTime Created_at { set; get; }
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }
        
        public virtual ICollection<Producto> Productos { set; get; }
        
        public Talla()
        {
            this.Productos = new HashSet<Producto>();
        }
    }
}