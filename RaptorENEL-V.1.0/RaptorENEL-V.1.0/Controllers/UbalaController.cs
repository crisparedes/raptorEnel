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

namespace RaptorENEL_V._1._0.Controllers
{
    public class UbalaController : Controller
    {
        int Paginacion = 10;
        private RaptorContext db = new RaptorContext();

        // GET: Ubala
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

            var ubala = db.Ubala.Include(u => u.User).OrderBy(c => c.User.username);
            return View(ubala.ToList());
        }

        // GET: Ubala/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubala ubala = db.Ubala.Find(id);
            if (ubala == null)
            {
                return HttpNotFound();
            }
            return View(ubala);
        }

        // GET: Ubala/Create
        public ActionResult Create()
        {
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username");

            Ubala ubala = new Ubala();
            return View(ubala);
        }

        // POST: Ubala/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "factibilidad,estado_predio,municipio,nombre_propietario,documento,numero_contacto,latitud_pre,longitud_pre,codigo_conexion,latitud_con,longitud_con,tipo_red,mantenimiento_red,mantenimiento,cdt,estado_vivienda,obs_adecuacion_i,obs_adecuacion_e,tipo_servicio,carga,tipo_carga,calibre,documentacion,observaciones,usuario_id,servicio_directo,fecha,cobertura,imagen,distancia,fecha_creacion, PostedFile")] Ubala ubala)
        {

            bool codigoExist = db.Ubala.Any(x => x.factibilidad == ubala.factibilidad);
            if (!codigoExist)
            {

                if (ubala.PostedFile != null)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                    string exttension = System.IO.Path.GetExtension(ubala.PostedFile.FileName);

                    if (supportedTypes.Contains(exttension.ToLower()))
                    {
                        string path = Server.MapPath("~" + RaptorContext.imagesUbala);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                        ubala.PostedFile.SaveAs(path + fileName);
                        ubala.imagen = RaptorContext.imagesUbala + fileName;

                        if (ModelState.IsValid)
                        {
                            db.Ubala.Add(ubala);
                            db.SaveChanges();
                            TempData["Msg"] = "Creado correctamente";
                            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
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
            }
            else
            {
                TempData["MsgErr"] = "El codigo ya existe";
                ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
                return View(ubala);
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
            return View(ubala);
        }

        // GET: Ubala/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubala ubala = db.Ubala.Find(id);
            if (ubala == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
            return View(ubala);
        }

        // POST: Ubala/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "factibilidad,estado_predio,municipio,nombre_propietario,documento,numero_contacto,latitud_pre,longitud_pre,codigo_conexion,latitud_con,longitud_con,tipo_red,mantenimiento_red,mantenimiento,cdt,estado_vivienda,obs_adecuacion_i,obs_adecuacion_e,tipo_servicio,carga,tipo_carga,calibre,documentacion,observaciones,usuario_id,servicio_directo,fecha,cobertura,imagen,distancia,fecha_creacion,PostedFile, limpiar")] Ubala ubala)
        {
            if (ubala.PostedFile != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                string exttension = System.IO.Path.GetExtension(ubala.PostedFile.FileName);

                if (supportedTypes.Contains(exttension.ToLower()))
                {
                    string path = Server.MapPath("~" + RaptorContext.imagesUbala);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    String fileName = string.Format(@"{0}" + exttension, Guid.NewGuid());

                    ubala.PostedFile.SaveAs(path + fileName);

                    //eliminando la imagen anterior si tiene Limpiar en true

                    if (ubala.limpiar)
                    {
                        String filePath = Server.MapPath(ubala.imagen);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    ubala.imagen = RaptorContext.imagesUbala+ fileName;

                    if (ModelState.IsValid)
                    {
                        db.Entry(ubala).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Msg"] = "Modificado correctamente";
                        ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
                        return View(ubala);
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
                    db.Entry(ubala).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                    ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
                    return View(ubala);
                }
            }
            
            ViewBag.usuario_id = new SelectList(db.User.OrderBy(c => c.username), "id", "username", ubala.usuario_id);
            return View(ubala);
        }

        // GET: Ubala/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubala ubala = db.Ubala.Find(id);
            if (ubala == null)
            {
                return HttpNotFound();
            }
            return View(ubala);
        }

        // POST: Ubala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ubala ubala = db.Ubala.Find(id);
            db.Ubala.Remove(ubala);
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
