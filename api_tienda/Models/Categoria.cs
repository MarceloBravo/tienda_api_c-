﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_tienda.Models
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoriaId { set; get; }
        [Required(ErrorMessage = "Debes ingresar un nombre para la categoría.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre de la categoria debe tener entre 5 y 100 carácteres.")]
        public string Nombre { set; get; }       
        public string Slug { set; get; }
        [Required(ErrorMessage = "Debes ingresar una descripción para la categoría.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción de la categoría debe tener entre 10 y 500 carácteres.")]
        public string Descripcion { set; get; }
        public DateTime created_at { set;  get; }
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }

        public virtual ICollection<Producto> Productos { set; get; }
                
        public Categoria()
        {
            this.Productos = new HashSet<Producto>();
        }

    }
    /*
    class CategoriasDbContext: DbContext
    {
        public DbSet<Categoria> Categorias { set; get; }
    }
    */
}