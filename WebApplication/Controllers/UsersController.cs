using Data;
using Data.Util;
using Komatsu_SistemaSeguros.STS;
using Service;
using Service.Logic.Contract;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Util;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class UsersController : Controller
    {
        // GET: Users
        UsersService servicio = new UsersService();
        CentroCostoService servicioCentroCosto = new CentroCostoService();
        PerfilService servicioPerfil = new PerfilService();

        private readonly IUsuarioServiceLogic oIUsuarioServiceLogic;
        private readonly IUsuarioCentroCostoServiceLogic oIUsuarioCentroCostoServiceLogic;
        private readonly IPerfilServiceLogic oIPerfilServiceLogic;
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;

        public UsersController(IUsuarioServiceLogic oIUsuarioServiceLogic, IUsuarioCentroCostoServiceLogic oIUsuarioCentroCostoServiceLogic, IPerfilServiceLogic oIPerfilServiceLogic, ICentroCostoServiceLogic oICentroCostoServiceLogic)
        {
            this.oIUsuarioServiceLogic = oIUsuarioServiceLogic;
            this.oIUsuarioCentroCostoServiceLogic = oIUsuarioCentroCostoServiceLogic;
            this.oIPerfilServiceLogic = oIPerfilServiceLogic;
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
        }

        [STSUrl]
        public ActionResult Index()
        {
            ViewBag.ListaPerfil = oIPerfilServiceLogic.GetList();
            UserModel user = UtilSession.Usuario;
            var modelo = new UserViewModel();
            var lista = oICentroCostoServiceLogic.Get();
            modelo.ListaCentroCosto = lista.ToList();
            modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            ViewBag.CurrentUser = user;
            return View(modelo);
        }

        public ActionResult GetDropDown(string id, string nombre = "UsersId", string @default = null)
        {
            var usersService = new UsersService();
            var list = usersService.Get().OrderBy(x => x.Apellidos).Select(t => new SelectListItem()
            {
                Text = $"{t.Apellidos} , {t.Nombres}",
                Value = t.ID.ToString(),
                Selected = t.ID.ToString() == id
            }).ToList();
            if (@default != null)
                list.Insert(0, new SelectListItem()
                {
                    Selected = id == "",
                    Value = "",
                    Text = @default
                });
            return View("_DropDown", Tuple.Create<IEnumerable<SelectListItem>, string>(list, nombre));
        }

        public async Task<PartialViewResult> Registrar(int id = default(int))
        {
            var modelo = await GetUsuarioViewModel(id);
            return PartialView("_Formulario", modelo);
        }

        [HttpPost]
        public JsonResult RegistrarPost(UsuarioViewModel modelo)
        {
            var result = new OperationResult();
            UserModel CurrentUser = UtilSession.Usuario;
            if (modelo.Usuario.ID == default(int))
            {
                //result = servicio.Add(Usuario);
            }
            else
            {
                result = oIUsuarioServiceLogic.Modificar(modelo.Usuario , CurrentUser.UserName);
            }

            return Json(result);
        }

        private async Task<UsuarioViewModel> GetUsuarioViewModel(int id)
        {
            var modelo = new UsuarioViewModel();
            modelo.ListaCentroCosto = ListaCentroCosto();
            modelo.ListaPerfil = servicioPerfil.GetList();

            if (id != default(int))
            {
                modelo.Usuario = oIUsuarioServiceLogic.GetById(id);
                var listaCentroCostoUsuario = await oIUsuarioCentroCostoServiceLogic.GetUsuarioCentroCostoById(id);
                modelo.ListaCentroCostoByUsuario = listaCentroCostoUsuario.Select(m => m.CodigoCentroCosto).ToList();
            }

            return modelo;

        }

        private List<CentroCosto> ListaCentroCosto()
        {
            var query = servicioCentroCosto.Get();
            var lista = query.ToList().Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            return lista;
        }


        //public JsonResult GetListUsers(int page, int rows)
        //{
        //    int totalRows = 0;
        //    int CantidadRegistros = 0;

        //    var listQuery = servicio.GetLisPaginado(rows, page, out totalRows, out CantidadRegistros);

        //    var jsonData = new
        //    {
        //        total = totalRows,
        //        page,
        //        records = CantidadRegistros,
        //        rows = listQuery
        //    };

        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetListUsers(string nombres = null, string apellidos = null, string email = null, string codigocentrocosto = null, string numerodocumento = null, int? idperfil = null, int page = 0, int rows = 0)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Nombres", nombres);
            parameter.Collection.Add("Apellidos", apellidos);
            parameter.Collection.Add("Email", email);
            parameter.Collection.Add("CodigoCentroCosto", codigocentrocosto);
            parameter.Collection.Add("NumeroDocumento", numerodocumento);
            parameter.Collection.Add("IdPerfil", idperfil == null ? 0 : idperfil);

            var listQuery = servicio.GetLisPaginado(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListJson()
        {
            var users = servicio.Get();
            var list = new List<StAutocomplete>();
            foreach (var t in users)
            {
                list.Add(new StAutocomplete()
                {
                    value = $"{t.Apellidos} , {t.Nombres}",
                    data = new { dni = "12345678", celular = "987654321" }
                });
            }
            //var users = new List<string>() {
            //    "hola1","hola2","hola3"
            //};
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        struct StAutocomplete
        {
            public string value { get; set; }
            public object data { get; set; }
        }
    }
}