﻿using System;
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
        private banco_practica_3Entities db = new banco_practica_3Entities();

        // GET: debitoes
        public ActionResult Index()
        {
            var debito = db.debito.Include(d => d.credito1);
            return View(debito.ToList());
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
            ViewBag.credito = new SelectList(db.credito, "codigo", "Descripcion");
            return View();
        }

        // POST: debitoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,Monto,Descripcion,credito")] debito debito)
        {
            if (ModelState.IsValid)
            {
                db.debito.Add(debito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.credito = new SelectList(db.credito, "codigo", "Descripcion", debito.credito);
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
            ViewBag.credito = new SelectList(db.credito, "codigo", "Descripcion", debito.credito);
            return View(debito);
        }

        // POST: debitoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,Monto,Descripcion,credito")] debito debito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(debito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.credito = new SelectList(db.credito, "codigo", "Descripcion", debito.credito);
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