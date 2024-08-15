using Komatsu_SistemaSeguros.STS;
using Service.Contracts;
using Service.Logic.Contract;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Util;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class ReporteController : Controller
    {
        private readonly IUsersService oIUsersService;
        private readonly IUsersServiceLogic oIUsersServiceLogic;
        private readonly IReporteServiceLogic oIReporteServiceLogic;
        private readonly ICentroCostoService oICentroCostoService;
        private readonly ILiquidacionServiceLogic _LiquidacionServiceLogic;
        private readonly ICentroCostoServiceLogic _ICentroCostoServiceLogic;

        public ReporteController(IUsersServiceLogic oIUsersServiceLogic, IReporteServiceLogic oIReporteServiceLogic, IUsersService oIUsersService, ICentroCostoService oICentroCostoService,
            ILiquidacionServiceLogic _LiquidacionServiceLogic, ICentroCostoServiceLogic _ICentroCostoServiceLogic)
        {
            this.oIUsersServiceLogic = oIUsersServiceLogic;
            this.oIReporteServiceLogic = oIReporteServiceLogic;
            this.oIUsersService = oIUsersService;
            this.oICentroCostoService = oICentroCostoService;

            this._LiquidacionServiceLogic = _LiquidacionServiceLogic;
            this._ICentroCostoServiceLogic = _ICentroCostoServiceLogic;
        }

        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        [STSUrl]
        public ActionResult General()
        {
            return View();
        }

        [STSUrl]
        public ActionResult Detallado()
        {
            var model = new ReporteViewModel();
            return View(model);
        }

        public JsonResult GastoTotalByResponsable(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteServiceLogic.GetGastoTotal(idResponsable, idSociedad, fechaInicio, fechaFin);
            return Json(result);
        }

        public JsonResult DetalleGastoTotalByResponsable(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteServiceLogic.GetDetalleGastoTotal(idResponsable, idSociedad, fechaInicio, fechaFin);
            return Json(result);
        }

        public JsonResult PenalidadTiempoEsperaByResponsable(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteServiceLogic.GetPenalidadTiempoEspera(idResponsable, idSociedad, fechaInicio, fechaFin);
            return Json(result);
        }

        public JsonResult ComparacionMensualByResponsable(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteServiceLogic.GetComparacionMensual(idResponsable, idSociedad, fechaInicio, fechaFin);
            return Json(result);
        }

        public JsonResult GetDropDown(string sociedad)
        {
            var model = new ReporteViewModel();
            var listaBeneficado = oIUsersService.Get().Where(m => m.CodigoSociedad == sociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            var resultBene = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_ListaBeneficiado", listaBeneficado);
            model.ListaBeneficiado = resultBene;

            var lista = oICentroCostoService.Get();

            var listaCentroCosto = lista.Where(m => m.Soc == sociedad).ToList();
            var resultCeco = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_ListaCentroCosto", listaCentroCosto);
            model.ListaCentroCosto = resultCeco;
            return Json(model);
        }

        public ActionResult VizorReporte()
        {
            return PartialView("_VizorReporte");
        }

        public PartialViewResult GetReporteDetallado(string listaCentroCosto, string listaUsuario)
        {
            var modelo = new LiquidacionViewModel();
            modelo.Liquidacion = new Data.Common.Liquidacion();
            modelo.Liquidacion.ListaSolicitud = oIReporteServiceLogic.GetListReporteDetallado(listaCentroCosto, listaUsuario);
            return PartialView("_ListaSolicitud", modelo);
        }

        public FileResult ExportarExcel(string listaUsuario , string listaCentroCosto)
        {
            string correoProveedor = string.Empty;

            var file = GetReport(listaUsuario, listaCentroCosto);

            return File(file, MediaTypeNames.Application.Octet, "ReporteDetallado.xls");
        }

        private byte[] GetReport(string listaUsuario, string listaCentroCosto)
        {
            var lista = oIReporteServiceLogic.GetListReporteDetallado(listaCentroCosto, listaUsuario);
            var path = Server.MapPath(string.Concat("~/Report/RDLC/ReportLiquidacion.rdlc"));
            //var dataSet = new string[] { "ReportLiquidacionDS" };
            var dataSet = new string[] { "ReportLiquidacionDetalle" };
            var dataSource = new object[] { lista };
            var file = new Export().CreateExcel(path, dataSet, dataSource);

            return file;
        }

        public FileResult GetFile(int idResponsable)
        {
            var file = GetReportPDF(idResponsable);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }

        private byte[] GetReportPDF(int idResponsable)
        {
            var listaGastoTotal = oIReporteServiceLogic.GetGastoTotal(idResponsable, null, null, null);
            var listaDetalleGastoTotal = oIReporteServiceLogic.GetDetalleGastoTotal(idResponsable, null, null, null);
            var listaPenalidadTiempoEspera = oIReporteServiceLogic.GetPenalidadTiempoEspera(idResponsable, null, null, null);
            var listaComparacionMensual = oIReporteServiceLogic.GetComparacionMensual(idResponsable, null, null, null);
            var listaAhorroProveedor = oIReporteServiceLogic.GetListAhorroProveedor(idResponsable, null, null, null);
            //var lista = oIReporteServiceLogic.GetListReporteDetallado(listaCentroCosto, listaUsuario);
            var path = Server.MapPath(string.Concat("~/Report/RDLC/ReportGastoTotal.rdlc"));
            var dataSet = new string[] { "ReporteGastoTotalDS", "ReporteDetalleGastoTotalDS", "ReportePenalidadTiempoDS", "ReporteComparacionMensualDS", "ReporteAhorroProveedorDS" };
            var dataSource = new object[] { listaGastoTotal, listaDetalleGastoTotal, listaPenalidadTiempoEspera, listaComparacionMensual, listaAhorroProveedor };
            var file = new Export().CreatePdf(path, dataSet, dataSource);

            return file;
        }
    }
}