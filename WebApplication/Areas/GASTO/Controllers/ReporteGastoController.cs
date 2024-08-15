using GASTO.Service.Contracts;
using GASTO.Service.Logic;
using GASTO.Service.Logic.Contract;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.ServiceDTO.Response;
using GASTO.Service.Services;
using Komatsu_SistemaSeguros.STS;
using Newtonsoft.Json;
using Service.Contracts;
using Service.Logic;
using Service.Logic.Contract;
using Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication.Areas.GASTO.Models;
using WebApplication.Models;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using BL_GASTO = GASTO.BusinessLogic;
using SolicitudViewModel = WebApplication.Areas.GASTO.Models.SolicitudViewModel;

namespace WebApplication.Areas.GASTO.Controllers
{
    [STSAuthorization]
    public class ReporteGastoController : BaseController
    {
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        private readonly ISociedadServiceLogic oISociedadServiceLogic;
        private readonly IUsersService oIUsersService;
        private readonly ISolicitudService_Gasto oISolicitudService_Gasto;
        private readonly IProveedorGastoWebApiService oIProveedorGastoWebApiService;
        private readonly IUsersGastoService oIUsersGastoService;
        private readonly ISolicitudServiceLogic_Gasto oISolicitudServiceLogic_Gasto;
        private readonly IProveedorGastoWebApiServiceLogic oIProveedorGastoWebApiServiceLogic;
        private readonly IHistorialLogService oIHistorialLogService;
        private readonly IReporteServiceLogic oIReporteServiceLogic;

        public ReporteGastoController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService,
            IReporteServiceLogic oIReporteServiceLogic)
        {
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
            this.oISociedadServiceLogic = oISociedadServiceLogic;
            this.oIUsersService = oIUsersService;
            this.oIHistorialLogService = new HistorialLogService();
            this.oISolicitudService_Gasto = new SolicitudService_Gasto();
            this.oIProveedorGastoWebApiService = new ProveedorGastoWebApiService();
            this.oIUsersGastoService = new UsersGastoService();
            this.oIProveedorGastoWebApiServiceLogic = new ProveedorGastoWebApiServiceLogic();
            this.oISolicitudServiceLogic_Gasto = new SolicitudServiceLogic_Gasto();
            this.oIReporteServiceLogic = oIReporteServiceLogic;
        }

        [STSUrl]
        public ActionResult Index()
        {

            var modelo = new SolicitudViewModel();
            modelo.NombreCompleto = UtilSession.Usuario.NombreCompleto;
            modelo.Email = UtilSession.Usuario.Email;
            var codigoSociedad = UtilSession.Usuario.IdSociedad;


            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "", NombreCO = "-SELECCIONE-" });

            ViewBag.listaSociedad = listaSociedad;

            return View(modelo);

        }

        public FileResult ExportarExcel(string IdSolicitud, string fechaInicio, string fechaFin, string sociedad)
        {

            var file = GetReportAuditoria(IdSolicitud, fechaInicio, fechaFin, sociedad);

            return File(file, MediaTypeNames.Application.Octet, "ReporteAuditoria.xls");
        }

        public FileResult ExportarExcelGR()
        {

            var file = GetReporteRepresentacion();

            return File(file, MediaTypeNames.Application.Octet, "ReporteRepresentacion.xls");
        }

        public FileResult ExportarExcelDE(int anio)
        {

            var file = GetReporteDetallado(anio);

            return File(file, MediaTypeNames.Application.Octet, "ReporteDetallado.xls");
        }

        [HttpGet]
        private byte[] GetReportAuditoria(string IdSolicitud, string fechaInicio, string fechaFin, string sociedad)
        {
            fechaInicio = (fechaInicio != "undefined" && fechaInicio != "" && fechaInicio != null) ? fechaInicio.Split('/')[2] + "-" + fechaInicio.Split('/')[1] + "-" + fechaInicio.Split('/')[0] + " 00:00:00" : fechaInicio;
            fechaFin = (fechaFin != "undefined" && fechaFin != "" && fechaFin != null) ? fechaFin.Split('/')[2] + "-" + fechaFin.Split('/')[1] + "-" + fechaFin.Split('/')[0] + " 23:59:59" : fechaFin;

            var lista = oIReporteServiceLogic.GetListReporteAuditoria(IdSolicitud , fechaInicio , fechaFin , sociedad);
            var path = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReporteAuditoria.rdlc"));
            //var dataSet = new string[] { "ReportLiquidacionDS" };
            var dataSet = new string[] { "ReporteAuditoria" };
            var dataSource = new object[] { lista };
            var file = new Export().CreateExcel(path, dataSet, dataSource);

            return file;
        }

        [HttpGet]
        private byte[] GetReporteRepresentacion()
        {
            
            var lista = oIReporteServiceLogic.GetListReporteRepresentacion();
            var path = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReportGastoRepresentacion.rdlc"));
            //var dataSet = new string[] { "ReportLiquidacionDS" };
            var dataSet = new string[] { "ReporteRepresentacion" };
            var dataSource = new object[] { lista };
            var file = new Export().CreateExcel(path, dataSet, dataSource);

            return file;
        }

        [HttpGet]
        private byte[] GetReporteDetallado(int anio)
        {

            var lista = oIReporteServiceLogic.GetListReporteDetallado(anio);
            var path = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReportGastoDetallado.rdlc"));
            //var dataSet = new string[] { "ReportLiquidacionDS" };
            var dataSet = new string[] { "ReporteGastoDetallado" };
            var dataSource = new object[] { lista };
            var file = new Export().CreateExcel(path, dataSet, dataSource);

            return file;
        }

        //[HttpGet]
        //public JsonResult GetReporteAuditoriaDetallado(string IdSolicitud)
        //{
        //    var lista = oIReporteServiceLogic.GetListReporteAuditoria(IdSolicitud);
        //    var jsonData = new
        //    {
        //        //total = intTotalRows,
        //        //page,
        //        //records = intCantidadRegistros,
        //        rows = lista,

        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
    }
}