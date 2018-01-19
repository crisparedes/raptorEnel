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
    public class BasesController : Controller
    {
        int Paginacion = 10;

        private RaptorContext db = new RaptorContext();

        // GET: Bases
        public ActionResult Index()
        {

            if (Request.Form["grid-size"] != null)
            {
                Paginacion = int.Parse(Request["grid-size"]);
            }
            else if (this.Session["Paginacion"] != null)
            {
                Paginacion = int.Parse(this.Session["Paginacion"].ToString());
            }

            this.Session["Paginacion"] = Paginacion;
            return View(db.Base.ToList());
        }

        // GET: Bases/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Base.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // GET: Bases/Create
        public ActionResult Create()
        {
            Base @base = new Base();
            return View(@base);
        }

        // POST: Bases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,descripcion,proceso,tipo,activo,fecha_creacion")] Base @base)
        {
            if (ModelState.IsValid)
            {

                bool baseExist = db.Base.Any(x => x.codigo == @base.codigo);
                if (!baseExist)
                {
                    db.Base.Add(@base);
                    db.SaveChanges();
                    TempData["Msg"] = "Creado correctamente";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["MsgErr"] = "El codigo ya existe";
                    return View(@base);
                }
                
            }

            return View(@base);
        }

        // GET: Bases/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Base.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            this.Session["dataEdit"] = @base.codigo;
            return View(@base);
        }

        // POST: Bases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,descripcion,proceso,tipo,activo,fecha_creacion")] Base @base)
        {
            if (ModelState.IsValid)
            {
                bool baseExist = db.Base.Any(x => x.codigo == @base.codigo);
                String codigo = this.Session["dataEdit"].ToString();
                if (!baseExist || codigo == @base.codigo)
                {
                    db.Entry(@base).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                    return View(@base);
                }
                else
                {
                    TempData["MsgErr"] = "El código ya existe";
                    return View(@base);
                };
            }
            return View(@base);
        }

        // GET: Bases/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Base @base = db.Base.Find(id);
            if (@base == null)
            {
                return HttpNotFound();
            }
            return View(@base);
        }

        // POST: Bases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Base @base = db.Base.Find(id);
            db.Base.Remove(@base);
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
