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
    public class PerguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Perguntas
        public ActionResult Index()
        {

            // obtém as perguntas de um planeta
            var perguntas = db.Perguntas.Where(s => s.Utilizador.Username.Equals(User.Identity.Name))
                           .Include(p => p.Utilizador);


            return View(perguntas.ToList());
        }

        // GET: Perguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perguntas perguntas = db.Perguntas.Find(id);
            if (perguntas == null)
            {
                return HttpNotFound();
            }
            return View(perguntas);
        }

        // GET: Perguntas/Create
        public ActionResult Create()
        {
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome");
            return View();
        }

        // POST: Perguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Pergunta,SistemasFK")] Perguntas perguntas)
        {
            if (ModelState.IsValid)
            {
                db.Perguntas.Add(perguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", perguntas.UtilizadoresFK);
            return View(perguntas);
        }

        // GET: Perguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perguntas perguntas = db.Perguntas.Find(id);
            if (perguntas == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", perguntas.UtilizadoresFK);
            return View(perguntas);
        }

        // POST: Perguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Pergunta,UtilizadoresFK")] Perguntas perguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", perguntas.UtilizadoresFK);
            return View(perguntas);
        }

        // GET: Perguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perguntas perguntas = db.Perguntas.Find(id);
            if (perguntas == null)
            {
                return HttpNotFound();
            }
            return View(perguntas);
        }

        // POST: Perguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perguntas perguntas = db.Perguntas.Find(id);
            db.Perguntas.Remove(perguntas);
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