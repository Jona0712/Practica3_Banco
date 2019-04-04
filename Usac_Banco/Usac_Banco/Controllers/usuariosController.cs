using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Usac_Banco.Models;

namespace Usac_Banco.Controllers
{
    public class usuariosController : Controller
    {
        private banco_practica_3Entities2 db = new banco_practica_3Entities2();

        public ActionResult AdminInd()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                return View();
            }
            return RedirectToAction("Login");
        }
        //Get 
        public ActionResult Transferencia()
        {
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                cuenta cu = db.cuenta.Where(s=> s.usua == usu.codigo).First();
                ViewBag.saldo = cu.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transferencia(Transferencia model) {
            if (ModelState.IsValid)
            {
                cuenta c = db.cuenta.Find(model.cuenta2);
                if (c != null)
                {
                    cuenta saldo = db.cuenta.Find(model.cuenta1);
                    if (model.monto <= saldo.Saldo) {
                        c.Saldo = c.Saldo + model.monto;
                        saldo.Saldo = saldo.Saldo - model.monto;
                        db.Entry(c).State = EntityState.Modified;
                        db.Entry(saldo).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("TransExito");
                    }
                }
                ViewBag.msg = "Datos Incorrectos";
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                cuenta cu = db.cuenta.Where(s => s.usua == usu.codigo).First();
                ViewBag.saldo = cu.Saldo.ToString();
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult TransExito() {
            return View();
        }
        // GET: usuarios
        public ActionResult Index()
        {
            //return View(db.usuario.ToList());
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET: usuarios
        public ActionResult Info(int codigo)
        {
            if (codigo > 0)
            {
                usuario usu = db.usuario.Find(codigo);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre;
                ViewBag.cuenta = db.cuenta.Where(i => i.usua == usu.codigo).First().Numero.ToString();
                return View();
            }
            return RedirectToAction("Login");
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //get
        public ActionResult Login()
        {
            if(Session["codigo"] != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                usuario usuario = db.usuario.Find(model.codigo);
                if (usuario != null && model.usua == usuario.usua && model.pass == usuario.pass)
                {
                    Session["codigo"] = usuario.codigo;

                    if (usuario.rol == 2)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("AdminInd");
                    }
                }
                return View();
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["codigo"] = null;
            return RedirectToAction("Login");
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            usuario.rol = 2;
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                cuentasController cuenCon = new cuentasController();
                cuenta cuenti = new cuenta();
                cuenti.Saldo = 0;
                int codi = db.usuario.Where(i => i.usua == usuario.usua).First().codigo;
                cuenti.usua = codi;
                if (cuenCon.Create(cuenti))
                {
                    return RedirectToAction("Info", new { codigo = codi });
                }
            }

            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
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
