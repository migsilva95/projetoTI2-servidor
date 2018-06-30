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
    public class SistemasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sistemas
        public ActionResult Index()
        {

            // obtém os sistemas de um utilizador
            var sistemas = db.Sistemas
                .Include(m => m.Utilizador)
                .ToList();

            return View(sistemas);
        }

        // GET: Sistemas/Create
        public ActionResult Create()
        {
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome");
            return View();
        }

        // POST: Sistemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,UtilizadoresFK")] Sistemas sistemas)
        {
            if (ModelState.IsValid)
            {
                db.Sistemas.Add(sistemas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", sistemas.UtilizadoresFK);
            return View(sistemas);
        }

        // GET: Sistemas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sistemas sistemas = db.Sistemas.Find(id);
            if (sistemas == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", sistemas.UtilizadoresFK);
            return View(sistemas);
        }

        // POST: Sistemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,UtilizadoresFK")] Sistemas sistemas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sistemas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", sistemas.UtilizadoresFK);
            return View(sistemas);
        }

        // GET: Sistemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sistemas sistemas = db.Sistemas.Find(id);
            if (sistemas == null)
            {
                return HttpNotFound();
            }
            return View(sistemas);
        }

        // POST: Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sistemas sistemas = db.Sistemas.Find(id);
            db.Sistemas.Remove(sistemas);
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