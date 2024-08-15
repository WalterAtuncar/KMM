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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.GASTO.Models;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using BL_GASTO = GASTO.BusinessLogic;

namespace WebApplication.Areas.GASTO.Controllers
{
    public class DelegacionController : BaseController
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
        public DelegacionController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService)
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


            //var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            var listaBeneficado = oIUsersService.GetBySociedadGastos(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });

            var listaAprobadores = oIUsersService.GetBySociedadGastos(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaAprobadores.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });

            var listaCentroCosto = oICentroCostoServiceLogic.Get().Where(m => m.Soc == "xx-1").ToList();
            listaCentroCosto.Insert(0, new Data.CentroCosto() { Descripcion = "-SELECCIONE-" });

            ViewBag.listaSociedad = listaSociedad;
            ViewBag.ListaCentrosCostos = listaCentroCosto;
            ViewBag.ListaBeneficiado = listaBeneficado;
            ViewBag.CodigoSociedad = codigoSociedad;



            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == 0).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;

            var listaTipoSolicitud = new BL_GASTO.TipoSolicitud.TipoSolicitud().List().ToList();
            listaTipoSolicitud.Insert(0, new BE_GASTO.TipoSolicitud() { Tipo = "-SELECCIONE-" });
            ViewBag.listaTipoSolicitud = listaTipoSolicitud;

            var listaSucursales = new BL_GASTO.Sucursal.Sucursal().List().ToList();
            listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursales;

            return View(modelo);
        }
        [HttpGet]
        public JsonResult GetListDelegacion(Int64? idusuario = null, int? idsituacionservicio = null, string fechaDesde = "", string fechaHasta = "", string idcentrocosto = null, string idsociedad = null, int? idsucursal = null, string anioejercicio = null, int? idtipo = null, string codigo = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;
            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.UserName = strUserNameRegistro;
            entSolicitud.IdUsuario = idusuario == null ? 0 : idusuario;
            entSolicitud.IdSituacionServicio = idsituacionservicio == null ? 0 : (int)idsituacionservicio;
            entSolicitud.IdSucursal = idsucursal == null ? 0 : (int)idsucursal;
            entSolicitud.IdSociedad = idsociedad == null ? "" : idsociedad;
            entSolicitud.AnioEjercicio = anioejercicio == null ? "" : anioejercicio;
            entSolicitud.IdTipo = idtipo == null ? 0 : (int)idtipo;
            entSolicitud.Codigo = codigo;

            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.CentroCostoAfectoCodigoSap = idcentrocosto == null ? "" : idcentrocosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;

            var lista = new BL_GASTO.Solicitud.Solicitud().PaginadoAprobacionDelegacion(entSolicitud, out intTotalRows, out intCantidadRegistros);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,

            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public async Task<PartialViewResult> Confirmacion()
        {
            var modelo = new SolicitudViewModel();
            modelo.NombreCompleto = UtilSession.Usuario.NombreCompleto;
            modelo.Email = UtilSession.Usuario.Email;
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            var listaBeneficado = oIUsersService.GetBySociedadGastos(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });

            ViewBag.ListaBeneficiado = listaBeneficado;

            return PartialView("_Confirmacion", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> AprobarDelegacion(string lista,string docnewaprobador)
        {
            string[] arrLista = lista.Split(',');
            var retorno = new { Success = false, Datos = "", Error = "" };

            for (int i = 0; i <= arrLista.Length - 1; i++)
            {
               var oSolicitud = await oISolicitudService_Gasto.AprobarDelegacion(int.Parse(arrLista[i]), docnewaprobador);
               
               retorno = new { Success = true, Datos = "", Error = "" };

            }

            return Json(retorno);
        }
    }
}