using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu_SistemaSeguros.STS;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BL = Komatsu.SistemaTaxis.BusinessLogic;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class PaginadoController : Controller
    {
        // GET: Paginado
        [HttpGet]
        public JsonResult GetListSolicitud(int? sociedad = null, string fechaDesde = "", string fechaHasta = "", int? situacion = null, string nroSolicitud = null, string centroCosto = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE.Solicitud entSolicitud = new BE.Solicitud();
            entSolicitud.IdSociedad = sociedad == null ? 0 : sociedad.Value;
            entSolicitud.IdPerfil = intIdPerfil;
            entSolicitud.UserNameRegistro = strUserNameRegistro;
            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.IdSituacionServicio = situacion == null ? 0 : situacion.Value;
            entSolicitud.CodigoSolicitud = nroSolicitud;
            entSolicitud.CentroCosto = centroCosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;
            var lista = new BL.Solicitud().List(entSolicitud, out intTotalRows, out intCantidadRegistros);

            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListSolicitudAprobador(int? sociedad = null, string fechaDesde = "", string fechaHasta = "", int? situacion = null, string nroSolicitud = null, string centroCosto = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE.Solicitud entSolicitud = new BE.Solicitud();
            entSolicitud.IdSociedad = sociedad == null ? 0 : sociedad.Value;
            entSolicitud.Sociedad = sociedad == null ? string.Empty : sociedad.Value.ToString();
            entSolicitud.IdPerfil = intIdPerfil;
            entSolicitud.UserNameRegistro = strUserNameRegistro;
            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.IdSituacionServicio = situacion == null ? 0 : situacion.Value;
            entSolicitud.CodigoSolicitud = nroSolicitud;
            entSolicitud.CentroCosto = centroCosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;
            var lista = new BL.Solicitud().AprobadorList(entSolicitud, out intTotalRows, out intCantidadRegistros);

            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region Liquidación
        [HttpGet]
        public JsonResult GetListLiquidacionPaginado(int Estado, int ProveedorTaxiID, string FechaInicio, string FechaFin, int page, int rows, string sidx = "", string sord = "desc")
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            BE.Liquidacion entLiquidacion = new BE.Liquidacion();
            entLiquidacion.Estado = Estado;
            entLiquidacion.ProveedorTaxiID = ProveedorTaxiID;
            entLiquidacion.FechaInicio = Convert.ToDateTime(FechaInicio);
            entLiquidacion.FechaFin = Convert.ToDateTime(FechaFin);
            entLiquidacion.PageSize = rows;
            entLiquidacion.PageIndex = page;
            var lista = new BL.Liquidacion().List(entLiquidacion, out intTotalRows, out intCantidadRegistros);

            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}