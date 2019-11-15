using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace api_tienda.DAL
{
    public class TiendaContext: DbContext
    {
        public TiendaContext(): base("TiendaContext")   //base debe indicar el nombre de la conexión configurada en el archivo Web.config
        {
        }

        public DbSet<Categoria> Categorias { set; get; }
        public DbSet<Marca> Marcas { set; get; }
        public DbSet<Talla> Tallas { set; get; }
        public DbSet<Producto> Productos { set; get; }
        public DbSet<ImagenesProducto> ImagenesProductos { set; get; }
        public DbSet<Pais> Paises { set; get; }
        public DbSet<Region> Regiones { set; get; }
        public DbSet<Comuna> Comunas { set; get; }
        public DbSet<Ciudad> Ciudades { set; get; }
        public DbSet<Rol> Roles { set; get; }
        public DbSet<Usuario> Usuarios { set; get; }
        public DbSet<Orden> Ordenes { set; get; }
        public DbSet<DetalleOrden> DetalleOrdenes { set; get; }
        public DbSet<WebPayTransaction> WebPayTransactions { set; get; }
        public DbSet<WebPayError> WebPayErrors { set; get; }
        public DbSet<Oferta> Ofertas { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}