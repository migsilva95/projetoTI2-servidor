using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoTI2_servidor.Models;

namespace ProjetoTI2_servidor.Controllers
{
    public class PlanetasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Planetas
        public ActionResult Index()
        {

            // obtém os planetas de um sistema
            var planetas = db.Planetas
                           .Include(p => p.Sistema);


            return View(planetas.ToList());
        }

        // GET: Planetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planetas planetas = db.Planetas.Find(id);
            if (planetas == null)
            {
                return HttpNotFound();
            }
            return View(planetas);
        }

        // GET: Planetas/Create
        public ActionResult Create()
        {
            ViewBag.SistemasFK = new SelectList(db.Sistemas, "ID", "Nome");
            return View();
        }

        // POST: Planetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,SistemasFK")] Planetas planetas)
        {
            if (ModelState.IsValid)
            {
                db.Planetas.Add(planetas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SistemasFK = new SelectList(db.Sistemas, "ID", "Nome", planetas.SistemasFK);
            return View(planetas);
        }

        // GET: Planetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planetas planetas = db.Planetas.Find(id);
            if (planetas == null)
            {
                return HttpNotFound();
            }
            ViewBag.SistemasFK = new SelectList(db.Sistemas, "ID", "Nome", planetas.SistemasFK);
            return View(planetas);
        }

        // POST: Planetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,SistemasFK")] Planetas planetas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planetas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SistemasFK = new SelectList(db.Sistemas, "ID", "Nome", planetas.SistemasFK);
            return View(planetas);
        }

        // GET: Planetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Planetas planetas = db.Planetas.Find(id);
            if (planetas == null)
            {
                return HttpNotFound();
            }
            return View(planetas);
        }

        // POST: Planetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Planetas planetas = db.Planetas.Find(id);
            db.Planetas.Remove(planetas);
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