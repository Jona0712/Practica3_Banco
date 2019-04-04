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
        byte[] a = new byte[1];
        private banco_practica_3Entities2 db = new banco_practica_3Entities2();

        // GET: creditoes
        public ActionResult Index()
        {
            if (Session["codigo"] != null)
            {
                a[0] = 2;
                var credito = db.credito.Where(i => i.estado == a);
                return View(credito.ToList());
            }
            return RedirectToAction("Login", "usuarios");
        }

        // GET: creditoes/Details/5
        public ActionResult Details(int? id, bool si)
        {
            if (Session["codigo"] != null)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                credito credi = db.credito.Find(id);
                if (credi == null)
                {
                    return HttpNotFound();
                }
                if (si)
                {
                    cuenta cuentica = db.cuenta.Find(credi.cuenta);
                    if (cuentica == null)
                    {
                        return HttpNotFound();
                    }
                    cuentica.Saldo = cuentica.Saldo + credi.Monto;
                    a[0] = 1; //aceptado
                    credi.estado = a;
                    db.Entry(cuentica).State = EntityState.Modified;
                    db.Entry(credi).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    a[0] = 3; //rechazada
                    credi.estado = a;
                    db.Entry(credi).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "usuarios");
        }

        // GET: creditoes/Create
        public ActionResult Create()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login", "usuarios");
        }

        // POST: creditoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Monto,Descripcion,estado,cuenta")] credito credito)
        {
            a[0] = 2; // 2 sin resolver
            credito.estado = a;
            if (ModelState.IsValid)
            {
                db.credito.Add(credito);
                db.SaveChanges();
                return RedirectToAction("Index","usuarios");
            }
            return RedirectToAction("Index");
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
