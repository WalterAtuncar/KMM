using Data.Util;
using Service.Logic.Contract;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Util;

namespace WebApplication.Areas.GASTO.Controllers
{

    public class CentroCostoController : BaseController
    {

        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        private readonly IUsuarioServiceLogic oIUsuarioServiceLogic;
        private readonly IPerfilServiceLogic oIPerfilServiceLogic;
        public CentroCostoController(ICentroCostoServiceLogic oICentroCostoServiceLogic, IUsuarioServiceLogic oIUsuarioServiceLogic, IPerfilServiceLogic oIPerfilServiceLogic)
        {
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
            this.oIUsuarioServiceLogic = oIUsuarioServiceLogic;
            this.oIPerfilServiceLogic = oIPerfilServiceLogic;
        }

       

        // GET: CentroCosto
        //[STSUrl]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<PartialViewResult> Registrar(string codigo = default(string))
        //{
        //    var modelo = await GetCentroCostoViewModel(codigo);
        //    return PartialView("_Formulario", modelo);
        //}

        public ActionResult GetDropDown(string id, string nombre = "CentroCostoCodigoSap", string @default = null)
        {
            var centroCostoService = new CentroCostoService();
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            var listaCentro = oICentroCostoServiceLogic.Get();
            var list = listaCentro.Where(m => m.Soc == codigoSociedad).ToList().Select(t => new SelectListItem()
            {
                Text = t.Codigo + " - " + t.Descripcion.ToUpper(),
                Value = t.Codigo,
                Selected = t.Codigo == id
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

        [HttpGet]
        public JsonResult GetListCentroCostoCombo()
        {
            var centroCostoService = new CentroCostoService();
            var lista = centroCostoService.Get();
            var list = lista.Select(c => new { c.Codigo, c.Descripcion }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarPost(CentroCostoViewModel modelo)
        {
            var result = await oICentroCostoServiceLogic.Update(modelo.CentroCosto);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetListCentroCosto(string codigo = null, string soc = null, string fechaDesde = "", string fechaHasta = "", int page = 0, int rows = 0, string tipo = null)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Codigo", codigo == "" ? null : codigo);
            parameter.Collection.Add("Sociedad", soc == "" ? null : soc);
            parameter.Collection.Add("FechaDesde", (fechaDesde == string.Empty) || (fechaDesde == null) ? SqlDateTime.MinValue : Convert.ToDateTime(fechaDesde));
            parameter.Collection.Add("FechaHasta", (fechaHasta == string.Empty) || (fechaHasta == null) ? SqlDateTime.MinValue : Convert.ToDateTime(fechaHasta));
            parameter.Collection.Add("FlagTipo", tipo == null ? "" : tipo);

            var listQuery = oICentroCostoServiceLogic.GetLisPaginado(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private async Task<CentroCostoViewModel> GetCentroCostoViewModel(string codigo)
        {
            var modelo = new CentroCostoViewModel();
            modelo.CentroCosto = await oICentroCostoServiceLogic.GetById(codigo);
            modelo.ListaUsuario = oIUsuarioServiceLogic.GetList();
            return modelo;
        }
    }
}