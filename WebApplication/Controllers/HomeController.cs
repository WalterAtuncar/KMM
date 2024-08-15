using Komatsu_SistemaSeguros.STS;
using Service.Logic.Contract;
using System.Collections.Generic;
using System.Net.Mime;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models;
using WebApplication.Util;

namespace WebApplication.Controllers
{
    [STSAuthorization(Order = 1), STSAcceso(Order = 2)]
    public class HomeController : Controller
    {
        private readonly IUsuarioServiceLogic oIUsuarioServiceLogic;
        private readonly IMenuPaginasServiceLogic oMenuPaginasServiceLogic;

        public HomeController(IUsuarioServiceLogic oIUsuarioServiceLogic, IMenuPaginasServiceLogic oMenuPaginasServiceLogic)
        {
            this.oIUsuarioServiceLogic = oIUsuarioServiceLogic;
            this.oMenuPaginasServiceLogic = oMenuPaginasServiceLogic;
        }

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            UserModel user = UtilSession.Usuario;
            
            PaginaViewModel paginaView = new PaginaViewModel();
            paginaView = ObtenerMenuPagina();

            return View(paginaView);
        }

        public ActionResult Inicio()
        {
            UserModel user = UtilSession.Usuario;

            PaginaViewModel paginaView = new PaginaViewModel();
            paginaView = ObtenerMenuPagina();
            
            return View(paginaView);
        }

        public PaginaViewModel ObtenerMenuPagina()
        {
            UserModel user = UtilSession.Usuario;
            var paginaView = new PaginaViewModel();

            paginaView.ListarMenu = oMenuPaginasServiceLogic.ListaMenus(user.UserName);
            paginaView.ListarPaginas = oMenuPaginasServiceLogic.ListaPaginas(user.UserName);

         
            Session["paginaView"] = null;
            Session.Remove("paginaView");

            if (Session["paginaView"] == null)
                Session["paginaView"] = paginaView;
            return paginaView;
        }

        public FileResult Exportar()
        {
            var lista = new List<Data.Common.Perfil>();
            lista.Add(new Data.Common.Perfil() { ID = 1 , Nombre = "Prueba1"});
            lista.Add(new Data.Common.Perfil() { ID = 2, Nombre = "Prueba2" });
            lista.Add(new Data.Common.Perfil() { ID = 3, Nombre = "Prueba3" });


            var path = Server.MapPath(string.Concat("~/Report/RDLC/ReportHome.rdlc"));
            var dataSet = new string[] { "ReportHomeDS" };
            var dataSource = new object[] { lista };

            var file = new Export().CreateExcel(path, dataSet, dataSource);
            return File(file, MediaTypeNames.Application.Octet, "Hola.xls");
        }

        private ActionResult RedirectLogin(UserModel usuario)
        {
            return View("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Usuario");
        }
    }
}