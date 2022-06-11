using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransPortClientes.Controllers;
using TransPortClientes.Models;

namespace TransPort.Filters
{
    public class VerificarLogin : ActionFilterAttribute
    {
        private Usuarios_Externos user;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                user = (Usuarios_Externos)HttpContext.Current.Session["user"];
                if (user == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("/Acceso/Login");
            }
        }
    }
}