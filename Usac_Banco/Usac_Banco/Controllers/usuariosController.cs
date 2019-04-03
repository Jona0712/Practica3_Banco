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

        // GET: usuarios
        public ActionResult Index()
        {
            //return View(db.usuario.ToList());
            if (Session["codigo"] != null)
            {
                usuario usu = db.usuario.Find(Session["codigo"]);
                ViewBag.codigo = usu.codigo.ToString();
                ViewBag.nombre = usu.nombre + " " + usu.apellido;
                //ViewBag.cuenta = numero de cuenta
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
                    //Session["usuario"] = usuario.nombre +" "+usuario.apellido;
                    //Session["usuario"] = usuario.usua; numero de cuenta
                    return RedirectToAction("Index");
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,apellido,usua,correo,pass")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
