using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Usac_Banco.Models;

namespace Usac_Banco.Controllers.Tests
{
    [TestClass()]
    public class usuariosControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var controlador = new usuariosController();
            var login = new Login();
            login.codigo = 1;
            login.usua = "jonna123";
            login.pass = "1234";

            var result = controlador.Login(login) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void RegistroTest()
        {
            var controlador = new usuariosController();
            var user = new usuario();
            user.nombre = "javier";
            user.apellido = "gomez";
            user.usua = "jgomez1";
            user.pass = "1234";
            user.correo = "jgomez@gmail.com";

            var result = controlador.Create(user) as ViewResult;

            Assert.AreEqual("Info", result.ViewName);
        }
    }
}