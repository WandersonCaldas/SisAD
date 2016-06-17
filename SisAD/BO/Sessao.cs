using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisAD.BO
{
    public class Sessao
    {
        public static void ExisteSessao()
        {
            if (HttpContext.Current.Session["id"] == null)
            {
                HttpContext.Current.Response.Redirect("/Account/Login");
            }
        }
    }
}