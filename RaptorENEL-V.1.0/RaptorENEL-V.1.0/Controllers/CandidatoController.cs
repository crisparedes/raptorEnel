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
    public class CandidatoController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Candidato
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
            return View(db.Candidato.ToList());
        }

        // GET: Candidato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato candidato = db.Candidato.Find(id);
            if (candidato == null)
            {
                return HttpNotFound();
            }
            return View(candidato);
        }

        // GET: Candidato/Create
        public ActionResult Create()
        {
            Candidato candidato = new Candidato();
            return View(candidato);
        }

        // POST: Candidato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sucursal,solicitud,municipio,direccion,propietario,zona,subzona,latitud,longitud,estado")] Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                
                bool solicitudExist = db.Candidato.Any(x => x.solicitud == candidato.solicitud);
                if (!solicitudExist || candidato.solicitud == null)
                {
                    db.Candidato.Add(candidato);
                    db.SaveChanges();
                    TempData["Msg"] = "Creado correctamente";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["MsgErr"] = "La solicitud ya existe";
                    return View(candidato);
                }


            }

            return View(candidato);
        }

        // GET: Candidato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato candidato = db.Candidato.Find(id);
            if (candidato == null)
            {
                return HttpNotFound();
            }
            this.Session["dataEdit"] = candidato.solicitud;
            return View(candidato);
        }

        // POST: Candidato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sucursal,solicitud,municipio,direccion,propietario,zona,subzona,latitud,longitud,estado")] Candidato candidato)
        {
            if (ModelState.IsValid)
            {
          
                bool solicitudExist = db.Candidato.Any(x => x.solicitud == candidato.solicitud);
                String solicitud = this.Session["dataEdit"].ToString();
                if (!solicitudExist || solicitud == candidato.solicitud || candidato == null)
                {
                    db.Entry(candidato).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                    return View(candidato);
                }
                else
                {
                    TempData["MsgErr"] = "La solicitud ya existe";
                    return View(candidato);
                };
            
            }
            return View(candidato);
        }

        // GET: Candidato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato candidato = db.Candidato.Find(id);
            if (candidato == null)
            {
                return HttpNotFound();
            }
            return View(candidato);
        }

        // POST: Candidato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidato candidato = db.Candidato.Find(id);
            db.Candidato.Remove(candidato);
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
