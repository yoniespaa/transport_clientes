using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransPortClientes.Models;

namespace TransPortClientes.Controllers
{
    public class SolicitudesController : Controller
    {
        // GET: Solicitudes
        TransPortEntities db = new TransPortEntities();
        public ActionResult Index(DateTime? fecha_inicio, DateTime? fecha_fin)
        {
            Usuarios_Externos user = (Usuarios_Externos)HttpContext.Session["user"];
            if (fecha_inicio == null) fecha_inicio = DateTime.Now.AddDays(-2);
            if (fecha_fin == null) fecha_fin = DateTime.Now.AddDays(1);
            List<Solicitudes_Transporte> st = db.Solicitudes_Transporte.Where(x => x.activo && x.fecha_creacion >= fecha_inicio && x.fecha_creacion <= fecha_fin).ToList();

            Fechas fechas = new Fechas();
            fechas.fecha_inicio = fecha_inicio;
            fechas.fecha_fin = fecha_fin;
            ViewBag.st = st;
            ViewBag.cliente = user.Clientes.nombre_cliente;
            return View(fechas);
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            Usuarios_Externos user = (Usuarios_Externos)HttpContext.Session["user"];
            ViewBag.id_punto_origen = new SelectList(db.Puntos.Where(x => x.id_cliente == user.id_cliente), "id_punto", "nombre");
            ViewBag.id_punto_destino = new SelectList(db.Puntos.Where(x => x.id_cliente == user.id_cliente), "id_punto", "nombre");
            return View();
        }

        // POST: Solicitudes/Create
        [HttpPost]
        public ActionResult Create(Solicitudes_Transporte solicitud)
        {
            Usuarios_Externos user = (Usuarios_Externos)HttpContext.Session["user"];
            try
            {
                
                solicitud.id_usuario_creacion = 1;
                solicitud.fecha_creacion = DateTime.Now;
                solicitud.id_cliente = user.id_cliente;
                solicitud.activo = true;
                solicitud.id_estado_solicitud = 1;
                db.Solicitudes_Transporte.Add(solicitud);
                db.SaveChanges();

                Solicitudes_Transporte st = db.Solicitudes_Transporte.OrderByDescending(x => x.id_solicitudes_transporte).FirstOrDefault();

                return RedirectToAction("Edit",new {id=st.id_solicitudes_transporte});
            }
            catch
            {
                ViewBag.id_punto_origen = new SelectList(db.Puntos.Where(x => x.id_cliente == user.id_cliente), "id_punto", "nombre");
                ViewBag.id_punto_destino = new SelectList(db.Puntos.Where(x => x.id_cliente == user.id_cliente), "id_punto", "nombre");
                return View();
            }
        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.detalle = db.Solicitudes_Transporte_Detalle.Where(x => x.activo && x.id_solicitud_transporte == id).ToList();
            ViewBag.id_solicitud = id;
            return View();
        }

        // POST: Solicitudes/Edit/5
        [HttpPost]
        public ActionResult Edit(Solicitudes_Transporte_Detalle std)
        {
            try
            {
                Usuarios_Externos user = (Usuarios_Externos)HttpContext.Session["user"];
                std.id_usuario_creacion = 1;
                std.fecha_creacion = DateTime.Now;
                std.activo = true;

                db.Solicitudes_Transporte_Detalle.Add(std);
                db.SaveChanges();

                ViewBag.id_solicitud = std.id_solicitud_transporte;
                ViewBag.detalle = db.Solicitudes_Transporte_Detalle.Where(x => x.activo && x.id_solicitud_transporte == std.id_solicitud_transporte).ToList();
                return View();
            }
            catch
            {
                ViewBag.id_solicitud = std.id_solicitud_transporte;
                ViewBag.detalle = db.Solicitudes_Transporte_Detalle.Where(x => x.activo && x.id_solicitud_transporte == std.id_solicitud_transporte).ToList();
                return View();
            }
            
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int id)
        {
            Solicitudes_Transporte std = db.Solicitudes_Transporte.Find(id);
            std.activo = false;
            std.eliminado = true;
            db.Entry(std).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDetalle(int id)
        {
            Solicitudes_Transporte_Detalle std = db.Solicitudes_Transporte_Detalle.Find(id);
            std.activo = false;
            std.eliminado = true;
            db.Entry(std).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = std.id_solicitud_transporte });
        }

    }
}
public class Fechas
{
    public DateTime? fecha_inicio { get; set; }
    public DateTime? fecha_fin { get; set; }
}