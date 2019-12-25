using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace tiendaAPI_MVC.Controllers.Admin
{
    public class HomeAdminController : Controller
    {

       [Route("Admin/Home")]
        public ActionResult Home()
        {
            if (Session["USUARIO"] == null)
                return Redirect("/Login");
            
            return View();
        }

    }
}
