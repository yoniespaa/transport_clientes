using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransPort.Filters;
using TransPortClientes.Models;

namespace TransPortClientes.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        TransPortEntities db = new TransPortEntities();
        public ActionResult Login(string user, string pass)
        {
            try
            {
                Encrypt encrypt = new Encrypt();
                string Epass = encrypt.getPass(pass);
                Usuarios_Externos usuario = db.Usuarios_Externos.Where(x => x.nombre_usuario == user && x.password == Epass).FirstOrDefault();
                if (usuario == null)
                {
                    ViewBag.error = "Usuario o contraseña incorrectos";
                    return View();
                }
                Session["user"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return View();
        }

        public ActionResult LogOut()
        {
            try
            {
                Session["user"] = null;
                return RedirectToAction("Login");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}