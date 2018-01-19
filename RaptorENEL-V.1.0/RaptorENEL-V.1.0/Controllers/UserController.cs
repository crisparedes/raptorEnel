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
    public class UserController : Controller
    {

        int Paginacion=10;

        private RaptorContext db = new RaptorContext();

        // GET: User
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

            return View(db.User.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,password,is_superuser,username,first_name,last_name,email,is_staff,is_active,date_joined")] User user)
        {
            if (ModelState.IsValid)
            {
                bool userExist = db.User.Any(x => x.username == user.username);
                if (!userExist) { 
                    db.User.Add(user);
                    db.SaveChanges();
                    TempData["Msg"] = "Creado correctamente";
                    return RedirectToAction("Create");
                }else
                {
                    TempData["MsgErr"] = "El nombre de usuario ya existe";
                    return View(user);
                }
            }
            return View(user);
        }
        
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            this.Session["dataEdit"] = user.username;
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,password,is_superuser,username,first_name,last_name,email,is_staff,is_active,date_joined")] User user)
        {
            if (ModelState.IsValid)
            {
                bool userExist = db.User.Any(x => x.username == user.username);
                String usuario = this.Session["dataEdit"].ToString();
                if (!userExist || usuario==user.username)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Msg"] = "Modificado correctamente";
                    return View(user);
                }
                else
                {
                    TempData["MsgErr"] = "El nombre de usuario ya existe";
                    return View(user);
                }
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
