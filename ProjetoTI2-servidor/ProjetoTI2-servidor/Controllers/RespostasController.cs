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
    public class RespostasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Respostas
        public ActionResult Index()
        {

            // obtém as respostas de uma pergunta
            var respostas = db.Respostas.Where(s => s.Utilizador.Username.Equals(User.Identity.Name))
                           .Include(p => p.Pergunta)
                           .Include(p => p.Utilizador);


            return View(respostas.ToList());
        }

        // GET: Respostas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respostas respostas = db.Respostas.Find(id);
            if (respostas == null)
            {
                return HttpNotFound();
            }
            return View(respostas);
        }

        // GET: Respostas/Create
        public ActionResult Create()
        {
            ViewBag.PerguntasFK = new SelectList(db.Perguntas, "ID", "Pergunta");
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome");
            return View();
        }

        // POST: Respostas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Resposta,RespostaCerta,PerguntasFK,UtilizadoresFK")] Respostas respostas)
        {
            if (ModelState.IsValid)
            {
                db.Respostas.Add(respostas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PerguntasFK = new SelectList(db.Perguntas, "ID", "Pergunta", respostas.PerguntasFK);
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", respostas.UtilizadoresFK);
            return View(respostas);
        }

        // GET: Respostas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respostas respostas = db.Respostas.Find(id);
            if (respostas == null)
            {
                return HttpNotFound();
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", respostas.UtilizadoresFK);
            return View(respostas);
        }

        // POST: Respostas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Resposta,RespostaCerta,PerguntasFK,UtilizadoresFK")] Respostas respostas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respostas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PerguntasFK = new SelectList(db.Perguntas, "ID", "Pergunta", respostas.PerguntasFK);
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Nome", respostas.PerguntasFK);
            return View(respostas);
        }

        // GET: Respostas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respostas respostas = db.Respostas.Find(id);
            if (respostas == null)
            {
                return HttpNotFound();
            }
            return View(respostas);
        }

        // POST: Respostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respostas respostas = db.Respostas.Find(id);
            db.Respostas.Remove(respostas);
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