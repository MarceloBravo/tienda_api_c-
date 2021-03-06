﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Producto
    {
        private decimal PrecioAnterior = 0;
        private bool Visible = true;
        private string Color = "#FFF";
        private bool Nuevo = true;
        private bool Oferta = false;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "El nombre del producto debe tener entre 10 y 150 carácteres.")]
        public string Nombre { set; get; }

        public string Slug { set; get; }

        [Required(ErrorMessage = "Debe ingresar un descripción para el producto.")]
        [StringLength(700, MinimumLength = 15, ErrorMessage = "La descripción debe tener entre 15 y 700 carácteres.")]
        public string Descripcion { set; get; }

        [Required(ErrorMessage = "Debe ingresar un resumen de la descripción del producto.")]
        [StringLength(300, MinimumLength = 15, ErrorMessage = "El resumen de la descripción del producto debe tener entre 15 y 300 carácteres.")]
        public string Resumen { set; get; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El precio no puede ser un valor negativo..")]
        public decimal Precio { set; get; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "El precio anterior no puede ser un valor negativo.")]
        public decimal precioAnterior { set => PrecioAnterior = value; get => PrecioAnterior; }

        [Required(ErrorMessage = "Debe especificar si el producto estárá visible en la página de inicio o no.")]
        [Range(typeof(bool), "false", "true", ErrorMessage = "Debe especificar si el producto estárá visible en la página de inicio o no.")]
        public bool visible { set => Visible = value; get => Visible; }

        [Required(ErrorMessage = "Debe ingresar un color para el producto.")]
        public string color { set => Color = value; get => Color; }

        [Required(ErrorMessage = "Debe especificar si el producto es nuevo o no.")]
        [Range(typeof(bool), "false", "true", ErrorMessage = "Debe especificar si el producto es nuevo o no.")]
        public bool nuevo { set => Nuevo = value; get => Nuevo; }

        [Required(ErrorMessage = "Debe especificar si el producto está en oferta o no.")]
        [Range(typeof(bool), "false", "true", ErrorMessage = "Debe especificar si el producto está en oferta o no.")]
        public bool oferta { set => Oferta = value; get => Oferta; }

        //[ForeignKey("Categoria")]
        public long CategoriaId { set; get; }        
        public virtual Categoria Categoria { set; get; }

        //[ForeignKey("Marca")]
        public long MarcaId { set; get; }        
        public virtual Marca Marca { set; get; }
        
        public ICollection<ImagenesProducto> Imagenes { set; get; }
        
        //public int Talla_id { set; get; }
        //[ForeignKey("Talla_id")]
        //public ICollection<Talla> Tallas {set; get;}        

        public DateTime created_at { set;  get; }
        public DateTime Updated_at { set; get; }
        public DateTime? Deleted_at { set; get; }


        public Producto()
        {
            Updated_at = DateTime.Today;
        }
        
    }
    /*
    class ProductoDbContext: DbContext
    {
        public DbSet<Producto> Productos { set; get; }
    }
    */
}