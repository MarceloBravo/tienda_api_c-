using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_tienda.Models
{
    public class Login
    {
        public string nicknameOrEmail { set; get; }
        public string password { set; get; }
    }
}