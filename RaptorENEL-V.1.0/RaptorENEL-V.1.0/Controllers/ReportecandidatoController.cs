using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RaptorENEL_V._1._0.DAL;
using RaptorENEL_V._1._0.Models;

namespace RaptorENEL_V._1._0.Controllers
{
    public class ReportecandidatoController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Reportecandidato
        public ActionResult Index()
        {
            if (Request.Form["grid-size"] != null) { 
                Paginacion = int.Parse(Request["grid-size"]);
            }
            else if (this.Session["Paginacion"] != null)
            {
                Paginacion = int.Parse(this.Session["Paginacion"].ToString());
            }

            this.Session["Paginacion"] = Paginacion;

            var reportecandidato = db.Reportecandidato.Include(r => r.User);
            return View(reportecandidato.ToList());
        }

        // GET: Reportecandidato/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportecandidato reportecandidato = db.Reportecandidato.Find(id);
            if (reportecandidato == null)
            {
                return HttpNotFound();
            }
            return View(reportecandidato);
        }

        // GET: Reportecandidato/Create
        public ActionResult Create()
        {
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c=>c.username), "id", "username");
            Reportecandidato Reportecandidato = new Reportecandidato();
            return View(Reportecandidato);
        }

        // POST: Reportecandidato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "solicitud,estado_predio,nombre_propietario,documento,numero_contacto,latitud_pre,longitud_pre,codigo_conexion,latitud_con,longitud_con,tipo_red,cdt,estado_vivienda,obs_adecuacion_i,obs_adecuacion_e,tipo_servicio,carga,documentacion,carta,ced_prop,aut_cert,tarjeta,diseno,estrato,servicio_directo,valor_oferta,observaciones,fecha,fecha_creacion,distancia,usuario_id")] Reportecandidato reportecandidato)
        {

            if (ModelState.IsValid)
            {

                bool repCandidatoExist = db.Reportecandidato.Any(x => x.solicitud == reportecandidato.solicitud);
                if (!repCandidatoExist)
                {
                    db.Reportecandidato.Add(reportecandidato);
                    db.SaveChanges();
                    TempData["Msg"] = "Creado correctamente";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["MsgErr"] = "La solicitud ya existe";
                    return View(reportecandidato);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }

            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", reportecandidato.usuario_id);
            return View(reportecandidato);
        }

        // GET: Reportecandidato/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportecandidato reportecandidato = db.Reportecandidato.Find(id);
            if (reportecandidato == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", reportecandidato.usuario_id);
            return View(reportecandidato);
        }

        // POST: Reportecandidato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "solicitud,estado_predio,nombre_propietario,documento,numero_contacto,latitud_pre,longitud_pre,codigo_conexion,latitud_con,longitud_con,tipo_red,cdt,estado_vivienda,obs_adecuacion_i,obs_adecuacion_e,tipo_servicio,carga,documentacion,carta,ced_prop,aut_cert,tarjeta,diseno,estrato,servicio_directo,valor_oferta,observaciones,fecha,fecha_creacion,distancia,usuario_id")] Reportecandidato reportecandidato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportecandidato).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Msg"] = "Modificado correctamente";
                ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", reportecandidato.usuario_id);
                return View(reportecandidato);
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", reportecandidato.usuario_id);
            return View(reportecandidato);
        }

        // GET: Reportecandidato/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reportecandidato reportecandidato = db.Reportecandidato.Find(id);
            if (reportecandidato == null)
            {
                return HttpNotFound();
            }
            return View(reportecandidato);
        }

        // POST: Reportecandidato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Reportecandidato reportecandidato = db.Reportecandidato.Find(id);
            db.Reportecandidato.Remove(reportecandidato);
            db.SaveChanges();
            TempData["Msg"] = "Eliminado correctamente";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
