using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Marca
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre de la marca debe tener entre 2 y 50 carácteres.")]
        public string Nombre { set; get; }

        public DateTime created_at { set;  get; }

        public DateTime Updated_at { set; get; }

        public DateTime? Deleted_at { set; get; }

        //public virtual ICollection<Producto> Productos { set; get; }
        
        //public Marca()
        //{
        //    this.Productos = new HashSet<Producto>();
        //}
    }
    
}