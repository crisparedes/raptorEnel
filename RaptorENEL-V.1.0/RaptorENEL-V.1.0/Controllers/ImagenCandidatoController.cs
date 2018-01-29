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
using static System.Net.Mime.MediaTypeNames;

namespace RaptorENEL_V._1._0.Controllers
{
    public class ImagenCandidatoController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: ImagenCandidato
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

            return View(db.ImagenCandidato.ToList().OrderByDescending(c=> c.fecha_creacion));
        }

        // GET: ImagenCandidato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenCandidato imagenCandidato = db.ImagenCandidato.Find(id);
            if (imagenCandidato == null)
            {
                return HttpNotFound();
            }
            return View(imagenCandidato);
        }

        // GET: ImagenCandidato/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImagenCandidato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,solicitud,observaciones,imagen,fecha_creacion, PostedFile")] ImagenCandidato imagenCandidato)
        {

            if (imagenCandidato.PostedFile != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string exttension = System.IO.Path.GetExtension(imagenCandidato.PostedFile.FileName);

                if (supportedTypes.Contains(exttension.ToLower()))
                {
                    string path = Server.MapPath("~" + RaptorContext.imagesAnexo);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                    imagenCandidato.PostedFile.SaveAs(path + fileName);
                    imagenCandidato.imagen = RaptorContext.imagesAnexo + fileName;

                    if (ModelState.IsValid)
                    {
                        db.ImagenCandidato.Add(imagenCandidato);
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
        
            return View(imagenCandidato);
        }

        // GET: ImagenCandidato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenCandidato imagenCandidato = db.ImagenCandidato.Find(id);
            if (imagenCandidato == null)
            {
                return HttpNotFound();
            }
            return View(imagenCandidato);
        }

        // POST: ImagenCandidato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,solicitud,observaciones,imagen,fecha_creacion,PostedFile, limpiar")] ImagenCandidato imagenCandidato)
        {

            if (imagenCandidato.PostedFile != null)
            {                         
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string exttension = System.IO.Path.GetExtension(imagenCandidato.PostedFile.FileName);

                if (supportedTypes.Contains(exttension.ToLower()))
                {
                    string path = Server.MapPath("~" + RaptorContext.imagesAnexo);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                    imagenCandidato.PostedFile.SaveAs(path + fileName);

                    //eliminando la imagen anterior si tiene Limpiar en true

                    if (imagenCandidato.limpiar)
                    {
                        String filePath = Server.MapPath(imagenCandidato.imagen);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    imagenCandidato.imagen = RaptorContext.imagesAnexo + fileName;

                    if (ModelState.IsValid)
                    {
                        db.Entry(imagenCandidato).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Msg"] = "Modificado correctamente";
                        return View(imagenCandidato);
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
                    db.Entry(imagenCandidato).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                    return View(imagenCandidato);
                }               
            }

            return View(imagenCandidato);
        }

        // GET: ImagenCandidato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImagenCandidato imagenCandidato = db.ImagenCandidato.Find(id);
            if (imagenCandidato == null)
            {
                return HttpNotFound();
            }
            return View(imagenCandidato);
        }

        // POST: ImagenCandidato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImagenCandidato imagenCandidato = db.ImagenCandidato.Find(id);
            db.ImagenCandidato.Remove(imagenCandidato);
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
