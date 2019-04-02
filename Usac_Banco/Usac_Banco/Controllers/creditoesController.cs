using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Usac_Banco;

namespace Usac_Banco.Controllers
{
    public class creditoesController : Controller
    {
        private banco_practica_3Entities db = new banco_practica_3Entities();

        // GET: creditoes
        public ActionResult Index()
        {
            var credito = db.credito.Include(c => c.cuenta1);
            return View(credito.ToList());
        }

        // GET: creditoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            return View(credito);
        }

        // GET: creditoes/Create
        public ActionResult Create()
        {
            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero");
            return View();
        }

        // POST: creditoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,Monto,Descripcion,estado,cuenta")] credito credito)
        {
            if (ModelState.IsValid)
            {
                db.credito.Add(credito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero", credito.cuenta);
            return View(credito);
        }

        // GET: creditoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero", credito.cuenta);
            return View(credito);
        }

        // POST: creditoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,Monto,Descripcion,estado,cuenta")] credito credito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "codigo", "Numero", credito.cuenta);
            return View(credito);
        }

        // GET: creditoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            credito credito = db.credito.Find(id);
            if (credito == null)
            {
                return HttpNotFound();
            }
            return View(credito);
        }

        // POST: creditoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            credito credito = db.credito.Find(id);
            db.credito.Remove(credito);
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
