using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_tienda.DAL
{
    public class TiendaInitializer: System.Data.Entity. DropCreateDatabaseIfModelChanges<TiendaContext>
    {
    }
}