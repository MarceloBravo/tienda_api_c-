using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace api_tienda.Models
{
    //[DataContract(IsReference = true)]
    public class Paginacion<T> where T: class
    {
        public int pagina { set; get; } = 1;
        public int registrosPorPagina { set; get; } = 10;
        public int pagVisibles { set; get; } = 2;  //Determina la cantidad de vinculos de páginas a m mostrar antes y despúes de la página actúal
        public int totalPaginas { set; get; }
        public double totalRegistros { set; get; }
        public IEnumerable<T> registros { set; get; }
        
    }
}