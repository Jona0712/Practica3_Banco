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
    public class cuentasController : Controller
    {
        private banco_practica_3Entities2 db = new banco_practica_3Entities2();

        // GET: cuentas
        public ActionResult Index()
        {
            var cuenta = db.cuenta.Include(c => c.usuario);
            return View(cuenta.ToList());
        }

        // GET: cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuenta cuenta = db.cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        /* GET: cuentas/Create
        public ActionResult Create()
        {
            ViewBag.usua = new SelectList(db.usuario, "codigo", "nombre");
            return View();
        }*/

        // POST: cuentas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Create([Bind(Include = "Saldo,usua")] cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.cuenta.Add(cuenta);
                db.SaveChanges();
                return true;
            }

            //ViewBag.usua = new SelectList(db.usuario, "codigo", "nombre", cuenta.usua);
            return false;
        }

        // GET: cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuenta cuenta = db.cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.usua = new SelectList(db.usuario, "codigo", "nombre", cuenta.usua);
            return View(cuenta);
        }

        // POST: cuentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Numero,Saldo,usua")] cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usua = new SelectList(db.usuario, "codigo", "nombre", cuenta.usua);
            return View(cuenta);
        }

        // GET: cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cuenta cuenta = db.cuenta.Find(id);
            if (cuenta == null)
            {
                return HttpNotFound();
            }
            return View(cuenta);
        }

        // POST: cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cuenta cuenta = db.cuenta.Find(id);
            db.cuenta.Remove(cuenta);
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
