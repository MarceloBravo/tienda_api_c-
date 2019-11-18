using api_tienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_tienda.DAL
{
    //'', '', ''
    public class TiendaInitializer: System.Data.Entity. DropCreateDatabaseIfModelChanges<TiendaContext>
    {
        protected override void Seed(TiendaContext context)
        {
            var categorias = new List<Categoria>
            {
                new Categoria{Nombre="Laptops",Slug="laptops",Descripcion="Artículos de computación", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Audio y video",Slug="audio-video",Descripcion="Artículos de video (televidores, pantallas, etc.) y audio (Equipos musicales, hgome teather, audífonos, etc.)", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Camaras",Slug="video",Descripcion="Artículos de video y fotografía", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Accesorios",Slug="accesorios",Descripcion="audífonos, cargadores, cables usb, pendrives, etc.", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Ofertas",Slug="ofertas",Descripcion="Artículos en oferta.", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Laptops",Slug="laptops",Descripcion="Artículos de computación", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Hombre",Slug="hombre",Descripcion="Artículos para hombre", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Mujer",Slug="mujer",Descripcion="Artículos para mujer", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Categoria{Nombre="Unisex",Slug="unisex",Descripcion="Artículos para parejas", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null}
            };
            categorias.ForEach(cat => context.Categorias.Add(cat));
            context.SaveChanges();

            var marcas = new List<Marca>
            {
                new Marca{Nombre="Asus", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Lenovo", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Sony", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Samsung", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Hp", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="LG", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Wawei", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Mac", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Microlab", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Addidas", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Under Armour", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Puma", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null},
                new Marca{Nombre="Reebook", created_at=DateTime.Parse("2019-10-29"), Updated_at=DateTime.Parse("2019-10-29"), Deleted_at=null}
            };
            marcas.ForEach(m => context.Marcas.Add(m));
            context.SaveChanges();

            var tallas = new List<Talla>
            {
                new Talla{ Nombre="No aplica", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="XXS", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="XS", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="S", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="M", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="L", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="XL", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null},
                new Talla{ Nombre="XXL", Created_at = DateTime.Today, Updated_at = DateTime.Today, Deleted_at = null}
            };
            tallas.ForEach(t => context.Tallas.Add(t));
            context.SaveChanges();

            var productos = new List<Producto>
            {
                new Producto{CategoriaId = 1, Nombre = "Notebook Asus AS-456", Slug = "note-as-456", Descripcion ="Notebook Asus Core I7 7a generación, 1 TB Hdd, 8 GB Ram, 1 HDMI, 1 VGA, 2 USB 2.0, 1 USB 3.0, DVD", Resumen = "Notebook Asus Core I7, 1TB Hdd, 8GB Ram", Precio = 600000 , visible= true , color ="Negro", nuevo =true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at=DateTime.Parse("2019-11-08 20:55:45"), Deleted_at= null, MarcaId = 1, oferta=true, precioAnterior = 700000 },
                new Producto{CategoriaId = 1, Nombre = "Notebook Lenovo LNV123", Slug = "Note-lnv-123", Descripcion = "Notebook Lenovo Core I7 8a generación, 1 TB Hdd, 8 GB Ram, 1 HDMI, 1 VGA, 2 USB 2.0, 1 USB 3.0", Resumen = "Notebook Lenovo Core I7, 1TB Hdd, 8GB Ram", Precio = 700000, visible = true, color = "Gris", nuevo=true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at=DateTime.Parse("2019-11-09 17:43:55"), Deleted_at=null, MarcaId = 2, oferta=false, precioAnterior = 900000 },
                new Producto{CategoriaId = 1, Nombre = "Notebook Samsung SS-123", Slug = "note-ss-123", Descripcion = "Notebook Samsung Core I7 8a generación, 1 TB Hdd, 6 GB Ram, 1 HDMI, 2 USB 3.0", Resumen = "Notebook Lenovo Core I7, 1TB Hdd, 6GB Ram", Precio = 450000, visible = true, color = "Negro", nuevo = true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at=DateTime.Parse("2019-11-06 23:15:09"), Deleted_at=null, MarcaId = 4, oferta = false, precioAnterior = 550000 },
                new Producto{CategoriaId = 1, Nombre = "Audifono Sony Sny-0101", Slug = "Sny-0101", Descripcion = "Audifono stero alámbrico con Sony con manos libres, aro para la cabeza, almoadillas de xx pulgadas, etc.", Resumen = "Audifonos Stero Sony con manos libres Sny-0101", Precio = 15000, visible = true, color = "Negro", nuevo = true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at= DateTime.Parse("2019-11-06 23:01:42"), Deleted_at=null, MarcaId = 3, oferta = false, precioAnterior=15000 },
                new Producto{CategoriaId = 1, Nombre = "Notebook HP hp-0908K", Slug = "note-hp-0908K", Descripcion ="Notebook HP Core I7 8a generación, 1 TB Hdd, 6 GB Ram, 1 HDMI, 2 USB 3.0", Resumen = "Notebook HP Core I7, 1TB Hdd, 6GB Ram", Precio =550000, visible = true, color="Gris", nuevo=true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at=DateTime.Parse("2019-11-07 17:34:21"), Deleted_at = null, MarcaId = 5, oferta = false, precioAnterior = 650000 },
                new Producto{CategoriaId = 1, Nombre = "Macbook Pro mc-0123", Slug = "mc-0123", Descripcion ="Macbook pro, 1 TB Hdd, 6 GB Ram, 1 HDMI, 2 USB 3.0", Resumen = "Maxkbook pro, 1 TB Hdd, 6 GB Ram, 1 HDMI, 2 USB 3.0", Precio = 600000, visible = true, color="Negro", nuevo=true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at = DateTime.Parse("2019-10-29 00:00:00"), Deleted_at = null, MarcaId = 8, oferta = false, precioAnterior= 650000},
                new Producto{CategoriaId = 1, Nombre = "Celular Lg x-cam LG-K580F", Slug = "LG-K580F", Descripcion = "Celular Lg X-Cam, camara frontal y doble cámara trasera, 16 GB de memoria + Micro SD de hasta 16 GB, liberado.", Resumen = "Celular Lg x-cam LG-K580F, 16 GB de almacenamiento +  camara frontal y trasera", Precio = 150000, visible = true, color = "Negro", nuevo = true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at = DateTime.Parse("2019-11-06 23:16:01"), Deleted_at = null, MarcaId = 6, oferta = false, precioAnterior = 200000 },
                new Producto{CategoriaId = 1, Nombre = "Cámara fotgrafica LG ph-12345", Slug = "LG-ph-12345", Descripcion ="Cámara fotografica LG, con gran angular, menmoria de 32 GB, función nocturna, etc.", Resumen = "Cámara fotgrafica LG ph-12345, 32 Gb, Func. Nocturna", Precio = 250000, visible =  true, color="Gris", nuevo = true, created_at = DateTime.Parse("2019-10-29 00:00:00"), Updated_at=DateTime.Parse("2019-10-29 00:00:00"), Deleted_at = null, MarcaId = 6, oferta = false, precioAnterior = 300000 },
                new Producto{CategoriaId = 2, Nombre = "Audifono xyz", Slug = "audifono-xyz", Descripcion ="jjscjfhdsj  jhkj h\r\nnk lkkdjvkl djlvkjlvdfjlv dlkvdjlkv klv jkldjvkl dlv djfl\r\n hvvivkldfjkldf vkldfjvkldfj ldfj \r\nhv kdhvkld vdfkh", Resumen = "Audifono XYZ, Marca ABC ", Precio = 10000, visible = true, color = "Negro", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = DateTime.Parse("2019-11-08 21:06:39"), MarcaId = 3, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 7, Nombre = "Polera Hombre Adidas ad001", Slug = "polera-hombre-adidas-ad001", Descripcion ="Polera de hombre addidas, modelo ad001 para running", Resumen = "Polera Runnig hombre addidas", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 7, Nombre = "Polera Hombre Under Armour Millitary", Slug = "polera-hombre-ua-millitary", Descripcion ="Polera de hombre Under Armour, modelo Millitary para running", Resumen = "Polera Runnig hombre UA Millitary", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 7, Nombre = "Polera Hombre Addidas running ad-0101", Slug = "polera-hombre-add-running", Descripcion ="Polera de hombre Addidas, modelo ad-0101", Resumen = "Polera Runnig hombre Addidas add-0101", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 7, Nombre = "Polera Hombre Addidas running ad-0102", Slug = "polera-hombre-add-running-ad-0102", Descripcion ="Polera de hombre Addidas, modelo ad-0102", Resumen = "Polera Runnig hombre Addidas add-0101", Precio = 10000, visible = true, color = "Rojo", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 8, Nombre = "Polera Mujer Adidas adm001", Slug = "polera-mujer-adidas-adm001", Descripcion ="Polera de mujer addidas, modelo adm001 para running", Resumen = "Polera Runnig mujer addidas", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 8, Nombre = "Polera Mujer Under Armour Millitary", Slug = "polera-mujer-ua-millitary", Descripcion ="Polera de mujer Under Armour, modelo m Millitary para running", Resumen = "Polera Runnig mujer UA Millitary", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 8, Nombre = "Polera Mujer Addidas running adm-0101", Slug = "polera-mujer-addm-running", Descripcion ="Polera de mujer Addidas, modelo adm-0101", Resumen = "Polera Runnig mujer Addidas addm-0101", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 8, Nombre = "Polera Mujer Addidas running adm-0102", Slug = "polera-mujer-add-running-adm-0102", Descripcion ="Polera de mujer Addidas, modelo adm-0102", Resumen = "Polera Runnig mujer Addidas addm-0101", Precio = 10000, visible = true, color = "Rojo", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 9, Nombre = "Polera Unisex Adidas adus001", Slug = "polera-unisex-adidas-adus001", Descripcion ="Polera unisex addidas, modelo adus001 para running", Resumen = "Polera Runnig unisex addidas", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 9, Nombre = "Polera Unisex Under Armour Millitary", Slug = "polera-unisex-ua-millitary", Descripcion ="Polera unisex Under Armour, modelo Us Millitary para running", Resumen = "Polera Runnig unisex UA Millitary", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 9, Nombre = "Polera Unisex Addidas running adus-0101", Slug = "polera-unisex-addus-running", Descripcion ="Polera unisex Addidas, modelo adus-0101", Resumen = "Polera Runnig unisex Addidas addus-0101", Precio = 10000, visible = true, color = "Gris", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 },
                new Producto{CategoriaId = 9, Nombre = "Polera Unisex Addidas running adus-0102", Slug = "polera-unisex-add-running-adus-0102", Descripcion ="Polera unisex Addidas, modelo adus-0102", Resumen = "Polera Runnig unisex Addidas addus-0101", Precio = 10000, visible = true, color = "Rojo", nuevo = true, created_at = DateTime.Parse("2019-11-08 21:05:21"), Updated_at=DateTime.Parse("2019-11-08 21:06:39"), Deleted_at = null, MarcaId = 10, oferta = false, precioAnterior = 15000 }
            };
            productos.ForEach(p => context.Productos.Add(p));
            context.SaveChanges();

            var imagenesProducto = new List<ImagenesProducto>
            {
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product01.png", predeterminada = true, nombreArchivo = "product01.png", ProductoId = 1, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product02.png", predeterminada = false, nombreArchivo = "product02.png", ProductoId = 1, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product03.png", predeterminada = false, nombreArchivo = "product03.png", ProductoId = 1, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product04.png", predeterminada = true, nombreArchivo = "product04.png", ProductoId = 2, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product05.png", predeterminada = false, nombreArchivo = "product05.png", ProductoId = 2, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product06.png", predeterminada = false, nombreArchivo = "product06.png", ProductoId = 2, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product07.png", predeterminada = true, nombreArchivo = "product07.png", ProductoId = 3, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/img-electronica/product08.png", predeterminada = false, nombreArchivo = "product08.png", ProductoId = 3, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l1.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 10, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l2.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 11, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l3.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 12, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l4.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 13, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l5.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 14, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l6.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 15, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l7.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 16, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/l8.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 17, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/cd.png", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 18, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/i1.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 19, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/i5.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 20, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null },
                new ImagenesProducto{Ubicacion = "Content/img/i8.jpg", predeterminada = true, nombreArchivo = "product09.png", ProductoId = 21, Created_at =DateTime.Today, Updated_at=DateTime.Today, Deleted_at = null }
                
            };
            imagenesProducto.ForEach(img => context.ImagenesProductos.Add(img));
            context.SaveChanges();
            
        }
    }
}