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
    public class NotificacionController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Notificacion
        public ActionResult Index()
        {
            if(Request.Form["grid-size"] != null)
            {
                Paginacion = int.Parse(Request["grid-size"]);
            }
            else if (this.Session["Paginacion"] != null)
            {
                Paginacion = int.Parse(this.Session["Paginacion"].ToString());
            }

            this.Session["Paginacion"] = Paginacion;
            var notificacion = db.Notificacion.Include(n => n.User);
            return View(notificacion.ToList().OrderBy(c=>c.fecha_creacion));
        }

        // GET: Notificacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // GET: Notificacion/Create
        public ActionResult Create()
        {
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c=>c.username), "id", "username");
            return View();
        }

        // POST: Notificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha_creacion,activo,usuario_id")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.Notificacion.Add(notificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", notificacion.usuario_id);
            return View(notificacion);
        }

        // GET: Notificacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", notificacion.usuario_id);
            return View(notificacion);
        }

        // POST: Notificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha_creacion,activo,usuario_id")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", notificacion.usuario_id);
            return View(notificacion);
        }

        // GET: Notificacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // POST: Notificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notificacion notificacion = db.Notificacion.Find(id);
            db.Notificacion.Remove(notificacion);
            db.SaveChanges();
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
