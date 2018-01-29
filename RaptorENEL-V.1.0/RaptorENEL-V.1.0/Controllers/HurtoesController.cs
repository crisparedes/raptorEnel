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
using System.IO;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;

namespace RaptorENEL_V._1._0.Controllers
{
    public class HurtoesController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Hurtoes
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

            var hurto = db.Hurto.Include(h => h.anomalia).Include(h => h.tipo_reporte).Include(h => h.User);
            return View(hurto.ToList());
        }

        // GET: Hurtoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurto hurto = db.Hurto.Find(id);
            if (hurto == null)
            {
                return HttpNotFound();
            }
            return View(hurto);
        }

        // GET: Hurtoes/Create
        public ActionResult Create()
        {
            Hurto hurto = new Hurto();
            ViewBag.anomalia_id = new SelectList(db.Base.Where(c => c.tipo == "A").Where(d => d.activo == true), "codigo", "descripcion");
            ViewBag.tipo_reporte_id = new SelectList(db.Base.Where(c => c.tipo == "R").Where(d => d.activo == true), "codigo", "descripcion");
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username");
            return View(hurto);
        }

        // POST: Hurtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,tipo_reporte_id,nse_referencia,nse,medidor,lectura,anomalia_id,imagen,usuario_id,ciclo,grupo,fecha,observacion,fecha_creacion,procesado,finalizado,geo_nse,PostedFile, latitud, longitud")] Hurto hurto)
        {
            // POINT(Long Lat)
            var myLocation = DbGeography.FromText("POINT("+hurto.longitud.ToString()+" "+hurto.latitud.ToString()+ ")", 4326);

            hurto.geo_nse = myLocation.ToString();
            if (hurto.PostedFile != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string exttension = System.IO.Path.GetExtension(hurto.PostedFile.FileName);

                if (supportedTypes.Contains(exttension.ToLower()))
                {
                    string path = Server.MapPath("~" + RaptorContext.imagesHurto);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                    hurto.PostedFile.SaveAs(path + fileName);
                    hurto.imagen = RaptorContext.imagesHurto + fileName;

                    if (ModelState.IsValid)
                    {
                        db.Hurto.Add(hurto);
                        db.SaveChanges();
                        TempData["Msg"] = "Creado correctamente";
                        return RedirectToAction("Create");
                    }
                }
                else
                {
                    TempData["MsgErr"] = "Debe elegir archivos de imagenes con exetención jpg, jpeg ó png";
                }
            }
            else
            {
                TempData["MsgErr"] = "Debe elegir una imagen válida";
            }



            ViewBag.anomalia_id = new SelectList(db.Base.Where(c => c.tipo == "A").Where(d => d.activo == true), "codigo", "descripcion");
            ViewBag.tipo_reporte_id = new SelectList(db.Base.Where(c => c.tipo == "R").Where(d => d.activo == true), "codigo", "descripcion");
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c=>c.username), "id", "username");
            return View(hurto);
        }

        // GET: Hurtoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurto hurto = db.Hurto.Find(id);
            if (hurto == null)
            {
                return HttpNotFound();
            }

            var dateQuery = db.Database.SqlQuery<String>("SELECT ST_AsText('" + hurto.geo_nse + "')");
            String punto = dateQuery.AsEnumerable().First().Replace("POINT(","").Replace(")","");

            String[] LatLong = punto.Split(' ');

            ViewBag.longitud = LatLong[0];
            ViewBag.latitud = LatLong[1];

            ViewBag.anomalia_id = new SelectList(db.Base.Where(c => c.tipo == "A").Where(d => d.activo == true), "codigo", "descripcion", hurto.anomalia_id);
            ViewBag.tipo_reporte_id = new SelectList(db.Base.Where(c => c.tipo == "R").Where(d => d.activo == true), "codigo", "descripcion", hurto.tipo_reporte_id);
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", hurto.usuario_id);
            return View(hurto);
        }

        // POST: Hurtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,tipo_reporte_id,nse_referencia,nse,medidor,lectura,anomalia_id,imagen,usuario_id,ciclo,grupo,fecha,observacion,fecha_creacion,procesado,finalizado,geo_nse, PostedFile, latitud, longitud, limpiar")] Hurto hurto)
        {

            // POINT(Long Lat)
            var myLocation = DbGeography.FromText("POINT(" + hurto.longitud.ToString() + " " + hurto.latitud.ToString() + ")", 4326);

            hurto.geo_nse = myLocation.ToString();
            if (hurto.PostedFile != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string exttension = System.IO.Path.GetExtension(hurto.PostedFile.FileName);

                if (supportedTypes.Contains(exttension.ToLower()))
                {
                    string path = Server.MapPath("~" + RaptorContext.imagesHurto);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                    hurto.PostedFile.SaveAs(path + fileName);

                    //eliminando la imagen anterior si tiene Limpiar en true

                    if (hurto.limpiar)
                    {
                        String filePath = Server.MapPath(hurto.imagen);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    hurto.imagen = RaptorContext.imagesHurto + fileName;

                    if (ModelState.IsValid)
                    {
                        db.Entry(hurto).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Msg"] = "Modificado correctamente";
                    }
                }
                else
                {
                    TempData["MsgErr"] = "Debe elegir archivos de imagenes con exetención jpg, jpeg ó png";
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hurto).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                }
            }


            var dateQuery = db.Database.SqlQuery<String>("SELECT ST_AsText('" + hurto.geo_nse + "')");
            String punto = dateQuery.AsEnumerable().First().Replace("POINT(", "").Replace(")", "");
            String[] LatLong = punto.Split(' ');
            ViewBag.longitud = LatLong[0];
            ViewBag.latitud = LatLong[1];
            ViewBag.anomalia_id = new SelectList(db.Base.Where(c => c.tipo == "A").Where(d => d.activo == true), "codigo", "descripcion", hurto.anomalia_id);
            ViewBag.tipo_reporte_id = new SelectList(db.Base.Where(c => c.tipo == "R").Where(d => d.activo == true), "codigo", "descripcion", hurto.tipo_reporte_id);
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", hurto.usuario_id);
            return View(hurto);
        }

        // GET: Hurtoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hurto hurto = db.Hurto.Find(id);
            if (hurto == null)
            {
                return HttpNotFound();
            }

            var dateQuery = db.Database.SqlQuery<String>("SELECT ST_AsText('" + hurto.geo_nse + "')");
            String punto = dateQuery.AsEnumerable().First().Replace("POINT(", "").Replace(")", "");

            String[] LatLong = punto.Split(' ');

            ViewBag.longitud = LatLong[0];
            ViewBag.latitud = LatLong[1];

            return View(hurto);
        }

        // POST: Hurtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Hurto hurto = db.Hurto.Find(id);
            db.Hurto.Remove(hurto);
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
