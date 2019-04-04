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
    public class debitoesController : Controller
    {
        private banco_practica_3Entities2 db = new banco_practica_3Entities2();

        // GET: debitoes
        public ActionResult Index()
        {
            var debito = db.debito.Include(d => d.cuenta1);
            return View(debito.ToList());
        }

        // GET:
        public ActionResult Asigna(string codigo)
        {
            if(codigo != null)
            {
                Session["codigo"] = codigo;
                return RedirectToAction("Create");
            }
            return RedirectToAction("AdminInd", "usuarios");
        }

        // GET: debitoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            debito debito = db.debito.Find(id);
            if (debito == null)
            {
                return HttpNotFound();
            }
            return View(debito);
        }

        // GET: debitoes/Create
        public ActionResult Create()
        {
            if (Session["codigo"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "usuarios");

        }

        // POST: debitoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Monto,Descripcion,cuenta")] debito debito)
        {
            if (ModelState.IsValid)
            {
                cuenta cuentica = db.cuenta.Find(debito.cuenta);
                if (cuentica == null) 
                {
                    ViewBag.mensaje = "no";
                    return View();
                }else if (debito.Monto > cuentica.Saldo)
                {
                    ViewBag.mensaje = "si";
                    return View();
                }
                cuentica.Saldo = cuentica.Saldo - debito.Monto;
                db.debito.Add(debito);
                db.Entry(cuentica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminInd", "usuarios");
            }
            
            return View(debito);
        }

        // GET: debitoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            debito debito = db.debito.Find(id);
            if (debito == null)
            {
                return HttpNotFound();
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "Numero", "Numero", debito.cuenta);
            return View(debito);
        }

        // POST: debitoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,Monto,Descripcion,cuenta")] debito debito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cuenta = new SelectList(db.cuenta, "Numero", "Numero", debito.cuenta);
            return View(debito);
        }

        // GET: debitoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            debito debito = db.debito.Find(id);
            if (debito == null)
            {
                return HttpNotFound();
            }
            return View(debito);
        }

        // POST: debitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            debito debito = db.debito.Find(id);
            db.debito.Remove(debito);
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
