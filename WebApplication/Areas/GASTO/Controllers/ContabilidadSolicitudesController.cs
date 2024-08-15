using GASTO.Service.Contracts;
using GASTO.Service.Logic;
using GASTO.Service.Logic.Contract;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.ServiceDTO.Response;
using GASTO.Service.Services;
using Komatsu_SistemaSeguros.STS;
using Newtonsoft.Json;
using Service.Contracts;
using Service.Logic.Contract;
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
    public class ContabilidadSolicitudesController : BaseController
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

        public ContabilidadSolicitudesController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService)
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
            modelo.IdPerfil = UtilSession.Usuario.IdPerfil;
            var codigoSociedad = UtilSession.Usuario.IdSociedad;


            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "", NombreCO = "-SELECCIONE-" });


            //var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            var listaBeneficado = oIUsersService.GetBySociedadGastos(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });

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
        [HttpPost]
        public async Task<JsonResult> RegistrarDetRendicionSolicitudPost(BE_GASTO.DetRendicionSolicitud element)
        {

            //element.IdSociedad = UtilSession.Usuario.IdSociedad.ToString();
            var Usuario = UtilSession.Usuario;

            element.IdUsuarioM = UtilSession.Usuario.Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjuntoDG"];
            BE.OperationResult objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Grabar(element);

            if (objResult.Success)
            {
                Session["ListaAdjuntoDG"] = null;
                //var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                //correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                //correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                //correo.Responsable = new Data.Common.Usuario();
                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                //correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                //correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                //var result = await oIProveedorWebApiService.EmailService(correo);

            }
            return Json(objResult);
        }

        public ActionResult ListaImpuestos_PV(int IdTipoDocumento, int idtipo)
        {
            var lstDocumentoImpuestos = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().Where(x => x.IdTipoDocumento == (long)IdTipoDocumento).ToList();
            var lstDocumentoImpuestos2 = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().ToList();
            var lstImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
            var lstTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).Where(x => x.IdTipoDocumento == IdTipoDocumento).ToList();
            ViewBag.IdClase = "";
            if (lstTipoDocumento.Count > 0)
            {
                ViewBag.IdClase = lstTipoDocumento[0].Clase;
            }

            var listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos2
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();
            if (IdTipoDocumento != 0)
            {
                listaImpuesto.Clear();
                listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();
            }

            ViewBag.IdImpuestoFirst = listaImpuesto.FirstOrDefault().IdImpuesto;
            ViewBag.listaImpuesto = listaImpuesto;

            return PartialView("_ListaImpuestos");
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarDetRendicionSolicitudPost(BE_GASTO.DetRendicionSolicitud element)
        {
            //element.IdSociedad = UtilSession.Usuario.IdSociedad.ToString();
            var Usuario = UtilSession.Usuario;
            element.IdUsuarioM = UtilSession.Usuario.Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjuntoDG"];
            BE.OperationResult objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Actualizar(element);

            if (objResult.Success)
            {
                Session["ListaAdjuntoDG"] = null;
                //var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                //correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                //correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                //correo.Responsable = new Data.Common.Usuario();
                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                //correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                //correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                //var result = await oIProveedorWebApiService.EmailService(correo);

            }
            return Json(objResult);
        }

        public DetalleSolicitudViewModel getDetalleSolicitud(int IdSolicitud)
        {
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud;
            var modelo = new DetalleSolicitudViewModel();
            entSolicitud.IdSolicitud = IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().RecuperarDetalle(entSolicitud);
            modelo.IdSociedad = entSolicitud.IdSociedad;
            modelo.NumeroDocumento = entSolicitud.NumeroDocumento;
            modelo.FechaRegistroStr = entSolicitud.FechaRegistroStr;
            modelo.TotalRendidoStr = entSolicitud.TotalRendidoStr;
            modelo.FechaContabilidadStr = entSolicitud.FechaContabilidadStr;

            modelo.NomApe = entSolicitud.NomApe;
            modelo.CECO = entSolicitud.CECO;
            modelo.Tipo = entSolicitud.Tipo;
            modelo.Moneda = entSolicitud.Moneda;
            modelo.Aprobador = entSolicitud.Aprobador;
            modelo.Destino = entSolicitud.Destino;
            modelo.IdSolicitud = entSolicitud.IdSolicitud;
            modelo.Sucursal = entSolicitud.Sucursal;
            modelo.IdTipo = entSolicitud.IdTipo;
            modelo.FechaInicioStr = entSolicitud.FechaInicioStr;
            modelo.FechaFinStr = entSolicitud.FechaFinStr;
            modelo.IdMoneda = entSolicitud.IdMoneda;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.IdDestino = entSolicitud.IdDestino;
            modelo.Motivo = entSolicitud.Motivo;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.ImporteSolicitud = entSolicitud.ImporteSolicitud;
            modelo.ImporteSolicitudStr = entSolicitud.ImporteSolicitudStr;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            modelo.TipoReembolso = entSolicitud.TipoReembolso;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.NombreFile = entSolicitud.NombreFile;
            modelo.CtaBancaria = entSolicitud.CtaBancaria;
            modelo.Codigo = entSolicitud.Codigo;
            modelo.NombreCO = entSolicitud.NombreCO;
            ViewBag.IdTipo = entSolicitud.IdTipo;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            ViewBag.IdDestino = entSolicitud.IdDestino;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;

            return modelo;
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetalleRendirGastoByIdSolicitud(int IdSolicitud)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdRendicionSolicitud = IdSolicitud;
            entDetRendicionSolicitud.PageSize = 1;
            entDetRendicionSolicitud.PageIndex = 10;
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            modelo.IdSolicitud = IdSolicitud;
            BE_GASTO.RendicionSolicitud entRendicion = new BE_GASTO.RendicionSolicitud();
            entRendicion.IdSolicitud = IdSolicitud;

            modelo.RendicionSolicitud = new BL_GASTO.RendicionSolicitud.RendicionSolicitud().RecuperarRendicion(entRendicion);
            if (modelo.RendicionSolicitud == null)
            {
                modelo.RendicionSolicitud = new BE_GASTO.RendicionSolicitud();
                modelo.RendicionSolicitud.EstadoRendicion = "Pendiente Rendir";
            }
            return PartialView("_ListDetalleRendicionGastos", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> PagarRendicionPost(int? IdSolicitud = null, string comentario = null, int? envia = null)
        {
            BE_GASTO.RendicionSolicitud entRendicionSolicitud = new BE_GASTO.RendicionSolicitud();
            entRendicionSolicitud.IdSolicitud = IdSolicitud == null ? 0 : (int)IdSolicitud;
            entRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            entRendicionSolicitud.Comentario = comentario == null ? "" : comentario;
            entRendicionSolicitud.Envia = envia == null ? 0 : (int)envia;
            entRendicionSolicitud.CodigoLiquidacion = "";
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.Solicitud.Solicitud().RendicionSolicitud(entRendicionSolicitud);
            return Json(objResult);
        }
        [HttpPost]
        public async Task<JsonResult> ValidarOrdenServicio(string ordenServicio, string sociedad)
        {
            //sociedad = UtilSession.Usuario.IdSociedad;
            var result = await oIProveedorGastoWebApiServiceLogic.GetOrdenServicio(ordenServicio, sociedad);
            return Json(result);
        }

        //[HttpPost]
        //public async Task<JsonResult> SolicitudOrdenServicioInvalida(int idsolicitud)
        //{
        //    bool success = true;
        //    BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
        //    entSolicitud.IdSolicitud = idsolicitud;
        //    entSolicitud = new BL_GASTO.Solicitud.Solicitud().RecuperarDetalle(entSolicitud);
        //    string idsociedad = entSolicitud.IdSociedad;
        //    List<BE_GASTO.DetRendicionSolicitud> objLista = (List<BE_GASTO.DetRendicionSolicitud>)new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().PaginadoAll(idsolicitud);
        //    foreach (BE_GASTO.DetRendicionSolicitud item in objLista)
        //    {
        //        var result = await oIProveedorGastoWebApiServiceLogic.GetOrdenServicio(item.OrdenServicio, idsociedad);
        //        success=result.Success;
        //        if (success == false) break;
        //    }
        //    return Json(success);
        //}

        [HttpPost]
        public async Task<PartialViewResult> RegistroDetalleGastoSolicitud(int IdDetRendicionSolicitud = default(int), string CORREL = default(string), int IdMoneda = default(int), int idtipo = default(int), string idsociedad = default(string))
        {

            var modelo = new DetalleSolicitudViewModel();
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud;
            entDetRendicionSolicitud.CORREL = CORREL;


            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.IdDetRendicionSolicitud = 0;
            var listaTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).ToList().Where(x => x.Estado == 1).ToList();
            listaTipoDocumento.Insert(0, new BE_GASTO.TipoDocumento() { Denominacion = "-SELECCIONE-" });
            ViewBag.listaTipoDocumento = listaTipoDocumento;

            var listaTipoGastoViaje = new BL_GASTO.TipoGastosViaje.TipoGastosViaje().List().ToList().Where(x => x.Estado == "A").ToList();
            listaTipoGastoViaje.Insert(0, new BE_GASTO.TipoGastosViaje() { TipoGastoViaje = "-SELECCIONE-" });
            ViewBag.listaTipoGastoViaje = listaTipoGastoViaje;

            var listaMoneda = new BL_GASTO.TipoMoneda.TipoMoneda().List().ToList();
            ViewBag.listaMoneda = listaMoneda;

            var lstPais = new BL_GASTO.Pais.Pais().List().Where(x => x.Estado == "A" && x.IdPais != 1).ToList();
            var listaPais = from p in lstPais
                            orderby p.Descripcion ascending
                            select p;
            ViewBag.listaPais = listaPais;

            List<BE_GASTO.Tratamiento> listaTratamiento = new List<BE_GASTO.Tratamiento>();
            BE_GASTO.Tratamiento tratamiento = new BE_GASTO.Tratamiento();
            tratamiento.ID = "Empresa";
            tratamiento.Denominacion = "Empresa";
            listaTratamiento.Add(tratamiento);
            ViewBag.listaTratamiento = listaTratamiento;
            ViewBag.IsExterno = "0";

            var listaCentrosCostos = new List<Data.CentroCosto>();
            listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == idsociedad).ToList();
            listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == idsociedad).ToList().Select(t => new Data.CentroCosto()
            {
                Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                Codigo = t.Codigo,
            }).ToList();
            listaCentrosCostos.Insert(0, new Data.CentroCosto() { Codigo = "0", Descripcion = "-SELECCIONE-" });
            ViewBag.ListaCentrosCostos = listaCentrosCostos;
            modelo.DetRendicionSolicitud.CentroCostoAfectoCodigoSap = "0";
            if (IdDetRendicionSolicitud != default(int))
            {
                modelo.DetRendicionSolicitud = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalle(entDetRendicionSolicitud);
                ViewBag.IsExterno = modelo.DetRendicionSolicitud.Externo ? "1" : "0";

                if (modelo.DetRendicionSolicitud.NroComprobante != null || modelo.DetRendicionSolicitud.NroComprobante != "")
                {
                    string[] nro = modelo.DetRendicionSolicitud.NroComprobante.Split('-');
                    modelo.DetRendicionSolicitud.NroIzquierda = nro[0];
                    modelo.DetRendicionSolicitud.NroCentro = nro[1];
                    if (modelo.DetRendicionSolicitud.IdTipoDocumento != 11 && modelo.DetRendicionSolicitud.IdTipoDocumento != 27 && modelo.DetRendicionSolicitud.IdTipoDocumento != 28 && modelo.DetRendicionSolicitud.IdTipoDocumento != 29 && modelo.DetRendicionSolicitud.IdTipoDocumento != 30)
                    {
                        modelo.DetRendicionSolicitud.NroDerecha = nro[2];
                    }
                }
                modelo.DetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud;
                modelo.DetRendicionSolicitud.CORREL = CORREL;

                var lstDocumentoImpuestos = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().Where(x => x.IdTipoDocumento == (long)modelo.DetRendicionSolicitud.IdTipoDocumento).ToList();
                var lstDocumentoImpuestos2 = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().ToList();
                var lstImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
                var lstTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).Where(x => x.IdTipoDocumento == modelo.DetRendicionSolicitud.IdTipoDocumento).ToList();
                ViewBag.IdClase = "";
                if (lstTipoDocumento.Count > 0)
                {
                    ViewBag.IdClase = lstTipoDocumento[0].Clase;
                }

                var listaImpuesto = (from i in lstImpuesto
                                     join id in lstDocumentoImpuestos2
                                    on i.IdImpuesto equals id.IdImpuesto
                                     select new
                                     {
                                         i.IdImpuesto,
                                         i.DescripcionImpuesto,
                                     }).Distinct().ToList();

                listaImpuesto.Clear();
                listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();




                ViewBag.IdImpuestoFirst = modelo.DetRendicionSolicitud.IdImpuesto;
                ViewBag.listaImpuesto = listaImpuesto;
                if (modelo.DetRendicionSolicitud.NombreFile.Trim() == "")
                {
                    Session["ListaAdjuntoDG"] = null;
                }
            }
            else
            {
                Session["ListaAdjuntoDG"] = null;
                var listaImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
                ViewBag.IdImpuestoFirst = listaImpuesto.FirstOrDefault().IdImpuesto;
                ViewBag.listaImpuesto = listaImpuesto;
                modelo.DetRendicionSolicitud.BaseInafectaStr = "0";
                modelo.DetRendicionSolicitud.TipoCambio = 1;
                if (IdMoneda != default(int)) modelo.DetRendicionSolicitud.IdMoneda = IdMoneda;
            }
            return PartialView("_FormularioDetalleGasto", modelo);
        }

        public async Task<PartialViewResult> Confirmacion()
        {
            return PartialView("_Confirmacion");
        }
        public async Task<PartialViewResult> ConfirmacionRechazo(int IdSolicitud, string cod)
        {
            ViewBag.IdSolicitudRechazo = IdSolicitud;
            ViewBag.CodigoRechazo = cod;
            return PartialView("_ConfirmacionRechazo");
        }

        public async Task<PartialViewResult> SolicitudAprobacionExterior()
        {
            return PartialView("_BuscarSolicitudExteriorModal");
        }

        public List<Data.CentroCosto> GetCentrosCostos(string codigoSociedad)
        {
            var listaCentoCosto = oICentroCostoServiceLogic.Get().Where(m => m.Soc == codigoSociedad).OrderBy(x => x.Descripcion).Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            listaCentoCosto.Insert(0, new Data.CentroCosto() { Descripcion = "-SELECCIONE-" });
            return listaCentoCosto;
        }

        [HttpPost]
        public async Task<PartialViewResult> GetHistorialByIdSolicitud(int IdSolicitud)
        {
            var modelo = new ViewModelHistorialLogs();
            modelo.ListaHistorialLogs = await oIHistorialLogService.GetHistorialByIdSolicitud(IdSolicitud);
            return PartialView("_ListHistorialEstadosSolicitud", modelo);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetSolicitudDetalleByIdSolicitud(int IdSolicitud)
        {
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud;
            var modelo = new DetalleSolicitudViewModel();

            entSolicitud.IdSolicitud = IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().RecuperarDetalle(entSolicitud);
            modelo.NomApe = entSolicitud.NomApe;
            modelo.CECO = entSolicitud.CECO;
            modelo.Tipo = entSolicitud.Tipo;
            modelo.Moneda = entSolicitud.Moneda;
            modelo.Aprobador = entSolicitud.Aprobador;
            modelo.Destino = entSolicitud.Destino;
            modelo.IdSolicitud = entSolicitud.IdSolicitud;
            modelo.Sucursal = entSolicitud.Sucursal;
            modelo.IdTipo = entSolicitud.IdTipo;
            modelo.FechaInicioStr = entSolicitud.FechaInicioStr;
            modelo.FechaFinStr = entSolicitud.FechaFinStr;
            modelo.IdMoneda = entSolicitud.IdMoneda;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.IdDestino = entSolicitud.IdDestino;
            modelo.Motivo = entSolicitud.Motivo;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.ImporteSolicitud = entSolicitud.ImporteSolicitud;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            modelo.TipoReembolso = entSolicitud.TipoReembolso;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.CtaBancaria = entSolicitud.CtaBancaria;
            modelo.Codigo = entSolicitud.Codigo;
            modelo.NombreFile = entSolicitud.NombreFile;
            ViewBag.IdTipo = entSolicitud.IdTipo;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            ViewBag.IdDestino = entSolicitud.IdDestino;

            return PartialView("_FormularioDetalle", modelo);

        }

        public ActionResult ListaCentrosCostos(string codsociedad)
        {
            var ListaCentrosCostos = GetCentrosCostos(codsociedad).ToList();
            ViewBag.ListaCentrosCostos = ListaCentrosCostos;
            return PartialView("_ListaCentroCosto");
        }
        [HttpPost]
        public async Task<bool> SolicitudOrdenServicioInvalida(string ordenservicio, string idsociedad)
        {
            bool success = true;
            var result = await oIProveedorGastoWebApiServiceLogic.GetOrdenServicio(ordenservicio, idsociedad);
            success = result.Success;
            return success;
        }

        private string EsPersonaNatural(bool externo, string ruc)
        {
            string natural = "";
            if (externo == true)
            {
                if (ruc.Trim().Length == 8) natural = "X";
                if (ruc.Substring(0, 2) == "10") natural = "X";
            }
            return natural;
        }

        [HttpPost]
        public async Task<JsonResult> AprobarPost(string lista)
        {
            string[] arrLista = lista.Split(',');
            List<SolicitudAprobarSapViewModel> listaAprobar = new List<SolicitudAprobarSapViewModel>();
            SolicitudAprobarSapViewModel oEnviarAprobar = new SolicitudAprobarSapViewModel();
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            bool validaregla = false;

            List<AnticipoApi> listaEnviarSapAnticipo = new List<AnticipoApi>();
            List<LiquidacionApi> listaEnviarSapLiquidacion = new List<LiquidacionApi>();
            List<CajaApi> listaEnviarSapCaja = new List<CajaApi>();
            List<TarjetaApi> listaEnviarSapTarjeta = new List<TarjetaApi>();
            AnticipoApi oAnticipoEnviarSap = new AnticipoApi();
            LiquidacionApi oLiquidacionEnviarSap = new LiquidacionApi();
            CajaApi oCajaEnviarSap = new CajaApi();
            TarjetaApi oTarjetaEnviarSap = new TarjetaApi();

            BE_GASTO.Solicitud oSolicitud = new BE_GASTO.Solicitud();
            DateTime fecharegistro = DateTime.Now;

            bool Error_Servicio = false;
            bool flag_Repite = false;
            string Error_ServicioMessage = "";
            string IdAlternativo = "";

            BE.OperationResult objResult = null;
            for (int i = 0; i <= arrLista.Length - 1; i++)
            {
                oSolicitud = await oISolicitudService_Gasto.GetDatoSolicitudByID(int.Parse(arrLista[i]));
                oEnviarAprobar = new SolicitudAprobarSapViewModel();
                oEnviarAprobar.IdSolicitud = int.Parse(arrLista[i]);
                oEnviarAprobar.Codigo = oSolicitud.Codigo;
                oEnviarAprobar.IdSituacionServicio = oSolicitud.IdSituacionServicio;
                oEnviarAprobar.IdTipo = (int)oSolicitud.IdTipo;
                oEnviarAprobar.CuentaContable = "";
                oEnviarAprobar.CodigoLiquidacion = "";
                oEnviarAprobar.ConDetalle = false;
                oEnviarAprobar.listaDetRendicionSolicitud = new List<BE_GASTO.DetRendicionSolicitud>();

                if (oEnviarAprobar.IdSituacionServicio == 3 && oEnviarAprobar.IdTipo == 1)
                {
                    oAnticipoEnviarSap = new AnticipoApi();
                    oAnticipoEnviarSap.BUKRS = oSolicitud.IdSociedad;
                    oAnticipoEnviarSap.VIAJE = oSolicitud.Codigo;
                    oAnticipoEnviarSap.CORREL = "01";
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    if (oUsuario.ID == 0) oUsuario.IDAlternativo = "";
                    oAnticipoEnviarSap.LIFNR = oUsuario.IDAlternativo;
                    oAnticipoEnviarSap.WAERS = oSolicitud.Moneda;
                    oAnticipoEnviarSap.WRBTR = (float)oSolicitud.ImporteSolicitud;
                    oAnticipoEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                    fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                    oAnticipoEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                    oEnviarAprobar.EnviarSap = true;
                    oEnviarAprobar.ConDetalle = false;
                    listaEnviarSapAnticipo.Add(oAnticipoEnviarSap);
                }
                else if ((oEnviarAprobar.IdSituacionServicio == 10 && oEnviarAprobar.IdTipo == 1) || (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 2))
                {

                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oLiquidacionEnviarSap = new LiquidacionApi();
                            oLiquidacionEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oLiquidacionEnviarSap.VIAJE = oSolicitud.Codigo;
                            oLiquidacionEnviarSap.LIFNR = IdAlternativo;
                            oLiquidacionEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oLiquidacionEnviarSap.LIFNR2 = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oLiquidacionEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            oLiquidacionEnviarSap.BLDAT2 = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oLiquidacionEnviarSap.CORREL = item.CORREL;
                            oLiquidacionEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oLiquidacionEnviarSap.NAME2 = item.Nombre2;
                            oLiquidacionEnviarSap.NAME3 = item.Nombre3;
                            oLiquidacionEnviarSap.NAME4 = item.Nombre4;
                            oLiquidacionEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";



                            oLiquidacionEnviarSap.BLART = item.Clase;
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oLiquidacionEnviarSap.XBLNR = nrocomprobante;
                            oLiquidacionEnviarSap.WAERS = oSolicitud.Moneda;
                            oLiquidacionEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oLiquidacionEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oLiquidacionEnviarSap.MWSKZ = item.Impuesto;
                            oLiquidacionEnviarSap.ZE_CLASGTO = item.CodigoGastoViaje;
                            oLiquidacionEnviarSap.HKONT = item.CuentaContable;
                            oLiquidacionEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oLiquidacionEnviarSap.AUFNR = item.OrdenServicio;
                            oLiquidacionEnviarSap.GLOSA = "";
                            oLiquidacionEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oLiquidacionEnviarSap.ZE_AOR = oEnviarAprobar.IdTipo == 1 ? "A" : "R";
                            oLiquidacionEnviarSap.SGTXT = item.GlosaDetalle;
                            oLiquidacionEnviarSap.TRATA = "";
                            oLiquidacionEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oLiquidacionEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oLiquidacionEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";
                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            listaEnviarSapLiquidacion.Add(oLiquidacionEnviarSap);
                        }
                    }
                }
                else if (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 3) //CAJA
                {
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oCajaEnviarSap = new CajaApi();
                            oCajaEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oCajaEnviarSap.FONDNR = oSolicitud.Codigo;
                            oCajaEnviarSap.CORREL = item.CORREL;
                            oCajaEnviarSap.LIFNR_A = IdAlternativo;
                            oCajaEnviarSap.FECLIQ = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oCajaEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oCajaEnviarSap.LIFNR_P = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oCajaEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oCajaEnviarSap.NAME2 = item.Nombre2;
                            oCajaEnviarSap.NAME3 = item.Nombre3;
                            oCajaEnviarSap.NAME4 = item.Nombre4;
                            oCajaEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";
                            oCajaEnviarSap.BLART = item.Clase;
                            oCajaEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oCajaEnviarSap.XBLNR = nrocomprobante;
                            oCajaEnviarSap.WAERS = oSolicitud.Moneda;
                            oCajaEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oCajaEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oCajaEnviarSap.MWSKZ = item.Impuesto;
                            oCajaEnviarSap.CLASGTO = item.CodigoGastoViaje;
                            oCajaEnviarSap.HKONT = item.CuentaContable;
                            oCajaEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oCajaEnviarSap.AUFNR = item.OrdenServicio;
                            oCajaEnviarSap.GLOSA = "";
                            oCajaEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oCajaEnviarSap.AOR = "";


                            oCajaEnviarSap.SGTXT = item.GlosaDetalle;
                            oCajaEnviarSap.TRATA = "";
                            //oCajaEnviarSap.CALLE = "";
                            //oCajaEnviarSap.POBLA = "";
                            //oCajaEnviarSap.PAIS = "";
                            oCajaEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oCajaEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oCajaEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";

                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            listaEnviarSapCaja.Add(oCajaEnviarSap);
                        }
                    }
                }
                else if (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 4)  // TARJETA
                {
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oTarjetaEnviarSap = new TarjetaApi();
                            oTarjetaEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oTarjetaEnviarSap.TCRENR = oSolicitud.Codigo;
                            oTarjetaEnviarSap.CORREL = item.CORREL;
                            oTarjetaEnviarSap.LIFNR_A = IdAlternativo;
                            oTarjetaEnviarSap.FECLIQ = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oTarjetaEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oTarjetaEnviarSap.LIFNR_P = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oTarjetaEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oTarjetaEnviarSap.NAME2 = item.Nombre2;
                            oTarjetaEnviarSap.NAME3 = item.Nombre3;
                            oTarjetaEnviarSap.NAME4 = item.Nombre4;
                            oTarjetaEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";
                            oTarjetaEnviarSap.BLART = item.Clase;
                            oTarjetaEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oTarjetaEnviarSap.XBLNR = nrocomprobante;
                            oTarjetaEnviarSap.WAERS = oSolicitud.Moneda;
                            oTarjetaEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oTarjetaEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oTarjetaEnviarSap.MWSKZ = item.Impuesto;
                            oTarjetaEnviarSap.HKONT = item.CuentaContable;
                            oTarjetaEnviarSap.CLASGTO = item.CodigoGastoViaje;
                            oTarjetaEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oTarjetaEnviarSap.AUFNR = item.OrdenServicio;
                            oTarjetaEnviarSap.GLOSA = "";
                            oTarjetaEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oTarjetaEnviarSap.AOR = "T";


                            oTarjetaEnviarSap.SGTXT = item.GlosaDetalle;
                            oTarjetaEnviarSap.TRATA = "";
                            //oTarjetaEnviarSap.CALLE = "";
                            //oTarjetaEnviarSap.POBLA = "";
                            //oTarjetaEnviarSap.PAIS = "";
                            oTarjetaEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oTarjetaEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oTarjetaEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";

                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            listaEnviarSapTarjeta.Add(oTarjetaEnviarSap);
                        }
                    }
                }
                else
                {
                    oEnviarAprobar.ConDetalle = false;
                    oEnviarAprobar.EnviarSap = false;
                }
                listaAprobar.Add(oEnviarAprobar);
            }
            if (listaEnviarSapAnticipo != null && listaEnviarSapAnticipo.Count > 0)
            {
                var listaresponse = new List<AnticipoApi>();
                var return_lista = await oIProveedorGastoWebApiService.PostEnviarAnticipo(listaEnviarSapAnticipo);
                if (return_lista.Success)
                {
                    listaresponse = (List<AnticipoApi>)return_lista.ResponseObject;
                    if (listaresponse != null && listaresponse.Count > 0)
                    {
                        for (int i = 0; i <= listaAprobar.Count - 1; i++)
                        {
                            foreach (var item in listaresponse)
                            {
                                if (item.BELNR != "")
                                {
                                    if (item.VIAJE == listaAprobar[i].Codigo)
                                    {
                                        listaAprobar[i].CuentaContable = item.BELNR;
                                        listaAprobar[i].EnviadoSap = true;
                                    }
                                }
                                else
                                {
                                    Error_Servicio = true;
                                    if (!flag_Repite)
                                    {

                                        Error_ServicioMessage += item.MENSAJE;
                                    }
                                    flag_Repite = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Error_Servicio = true;
                        Error_ServicioMessage = "No se generaron los registros enviados";
                    }
                }
                else
                {
                    Error_Servicio = true;
                    Error_ServicioMessage = "Error al retornar los datos enviados";
                }
            }

            if (listaEnviarSapLiquidacion != null && listaEnviarSapLiquidacion.Count > 0)
            {
                var listaresponse = new List<LiquidacionApi>();
                var return_lista = await oIProveedorGastoWebApiService.PostEnviarLiquidacion(listaEnviarSapLiquidacion);
                if (return_lista.Success)
                {
                    listaresponse = (List<LiquidacionApi>)return_lista.ResponseObject;
                    if (listaresponse != null && listaresponse.Count > 0)
                    {
                        string BELNR = "", BELNRR = "", BELNRC = "";
                        bool enviar = false;
                        for (int i = 0; i <= listaAprobar.Count - 1; i++)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.BELNR != "" && (item.BELNRC != "" || item.BELNRR != ""))
                                        {
                                            if (item.VIAJE == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                            {
                                                BELNR = item.BELNR;
                                                BELNRC = item.BELNRC;
                                                BELNRR = item.BELNRR;
                                                enviar = true;
                                            }
                                        }
                                        else
                                        {
                                            Error_Servicio = true;
                                            Error_ServicioMessage = item.MENSAJE;
                                            if (item.CORREL == "0001" && !flag_Repite)
                                            {

                                                Error_ServicioMessage += item.MENSAJE;
                                            }
                                            else
                                            {
                                                flag_Repite = true;
                                            }
                                        }
                                    }
                                    listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = BELNR;
                                    BELNR = "";
                                }

                                if (enviar)
                                {
                                    listaAprobar[i].CodigoLiquidacion = BELNRC;
                                    listaAprobar[i].CodigoReembolso = BELNRR;
                                    listaAprobar[i].EnviadoSap = true;
                                }
                                enviar = false;
                                BELNRR = "";
                                BELNRC = "";
                            }

                        }
                    }
                    else
                    {
                        Error_Servicio = true;
                        Error_ServicioMessage = "No se generaron los registros enviados";
                    }
                }
                else
                {
                    Error_Servicio = true;
                    Error_ServicioMessage = "Error al retornar los datos enviados";
                }
            }

            if (listaEnviarSapCaja != null && listaEnviarSapCaja.Count > 0)
            {
                var listaresponse = new List<CajaApi>();
                var return_lista = await oIProveedorGastoWebApiService.PostEnviarCaja(listaEnviarSapCaja);
                if (return_lista.Success)
                {
                    listaresponse = (List<CajaApi>)return_lista.ResponseObject;
                    if (listaresponse != null && listaresponse.Count > 0)
                    {
                        string BELNRR = "", BELNRC = "";
                        bool enviar = false;
                        for (int i = 0; i <= listaAprobar.Count - 1; i++)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.BELNR_LIQ != "" && item.BELNR_COMP != "")
                                        {
                                            if (item.FONDNR == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                            {
                                                BELNRC = item.BELNR_LIQ;
                                                BELNRR = item.BELNR_COMP;
                                                enviar = true;
                                            }
                                        }
                                        else
                                        {
                                            Error_Servicio = true;
                                            Error_ServicioMessage = item.MENSAJE;
                                            if (item.CORREL == "0001" && !flag_Repite)
                                            {
                                                Error_ServicioMessage += item.MENSAJE;
                                            }
                                            else
                                            {
                                                flag_Repite = true;
                                            }
                                        }
                                    }
                                    listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = BELNRC;
                                    BELNRC = "";
                                }

                                if (enviar)
                                {
                                    listaAprobar[i].CodigoLiquidacion = BELNRC;
                                    listaAprobar[i].CodigoReembolso = BELNRR;
                                    listaAprobar[i].EnviadoSap = true;

                                }
                                enviar = false;
                                BELNRR = "";
                                BELNRC = "";
                            }
                        }
                    }
                    else
                    {
                        Error_Servicio = true;
                        Error_ServicioMessage = "No se generaron los registros enviados";
                    }
                }
                else
                {
                    Error_Servicio = true;
                    Error_ServicioMessage = "Error al retornar los datos enviados";
                }
            }

            if (listaEnviarSapTarjeta != null && listaEnviarSapTarjeta.Count > 0)
            {
                var listaresponse = new List<TarjetaApi>();
                var return_lista = await oIProveedorGastoWebApiService.PostEnviarTarjeta(listaEnviarSapTarjeta);
                if (return_lista.Success)
                {
                    listaresponse = (List<TarjetaApi>)return_lista.ResponseObject;
                    if (listaresponse != null && listaresponse.Count > 0)
                    {
                        string BELNRR = "", BELNRC = "";
                        bool enviar = false;
                        for (int i = 0; i <= listaAprobar.Count - 1; i++)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.BELNR_LIQ != "" && item.BELNR_COMP != "")
                                        {
                                            if (item.TCRENR == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                            {

                                                BELNRC = item.BELNR_LIQ;
                                                BELNRR = item.BELNR_COMP;
                                                enviar = true;
                                            }
                                        }
                                        else
                                        {
                                            Error_Servicio = true;
                                            Error_ServicioMessage = item.MENSAJE;
                                            if (item.CORREL == "0001" && !flag_Repite)
                                            {

                                                Error_ServicioMessage += item.MENSAJE;
                                            }
                                            else
                                            {
                                                flag_Repite = true;
                                            }
                                        }
                                    }
                                    listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = BELNRC;
                                    BELNRC = "";
                                }

                                if (enviar)
                                {
                                    listaAprobar[i].CodigoLiquidacion = BELNRC;
                                    listaAprobar[i].CodigoReembolso = BELNRR;
                                    listaAprobar[i].EnviadoSap = true;
                                }
                                enviar = false;
                                BELNRR = "";
                                BELNRC = "";
                            }
                        }
                    }
                    else
                    {
                        Error_Servicio = true;
                        Error_ServicioMessage = "No se generaron los registros enviados";
                    }
                }
                else
                {
                    Error_Servicio = true;
                    Error_ServicioMessage = "Error al retornar los datos enviados";
                }

            }
            if (!Error_Servicio)
            {
                foreach (var item in listaAprobar)
                {
                    entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                    entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                    entSolicitud.IdSolicitud = (long)item.IdSolicitud;
                    entSolicitud.CuentaContable = item.CuentaContable;
                    entSolicitud.CodigoLiquidacion = item.CodigoLiquidacion;
                    entSolicitud.CodigoReembolso = item.CodigoReembolso;
                    if (!item.EnviarSap)
                    {
                        entSolicitud.CuentaContable = "";
                        entSolicitud.CodigoLiquidacion = "";
                        entSolicitud.CodigoReembolso = "";
                        if (!item.ConDetalle)
                            objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                    }
                    else
                    {
                        if (item.EnviadoSap)
                        {
                            bool ExisteCodigoLiquidacionVacionEnLista = false;

                            if (item.IdTipo == 1 && item.IdSituacionServicio == 3)
                            {
                                objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                            }
                            else
                            {
                                if ((item.CodigoLiquidacion != null && item.CodigoLiquidacion != "") || item.CodigoReembolso != null && item.CodigoReembolso != "")
                                {
                                    if (item.listaDetRendicionSolicitud != null && item.listaDetRendicionSolicitud.Count > 0)
                                    {
                                        foreach (var itemdetalle in item.listaDetRendicionSolicitud)
                                        {
                                            if (itemdetalle.CodigoLiquidacion == null || itemdetalle.CodigoLiquidacion.Trim() == "")
                                            {
                                                ExisteCodigoLiquidacionVacionEnLista = true;
                                            }
                                        }
                                        foreach (var itemdetalle in item.listaDetRendicionSolicitud)
                                        {
                                            if (itemdetalle.CodigoLiquidacion != null && itemdetalle.CodigoLiquidacion != "" && !ExisteCodigoLiquidacionVacionEnLista)
                                            {
                                                objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidadDetalle(itemdetalle);
                                            }
                                        }
                                    }
                                    if (!ExisteCodigoLiquidacionVacionEnLista)
                                    {
                                        objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (objResult == null)
                {
                    objResult = new BE.OperationResult();
                    objResult.NewID = 2;
                    objResult.Message = Error_ServicioMessage;
                    objResult.Success = true;
                }
                else
                {
                    objResult.Message = "";
                }
            }
            return Json(objResult);
        }

        [HttpPost]
        public async Task<JsonResult> AprobarPost2(string lista)
        {
            string[] arrLista = lista.Split(',');
            List<SolicitudAprobarSapViewModel> listaAprobar = new List<SolicitudAprobarSapViewModel>();
            SolicitudAprobarSapViewModel oEnviarAprobar = new SolicitudAprobarSapViewModel();

            List<BE_GASTO.ResponseSolicitudSap> listaResponseSolicitudSap = new List<BE_GASTO.ResponseSolicitudSap>();
            BE_GASTO.ResponseSolicitudSap oResponseSolicitudSap = new BE_GASTO.ResponseSolicitudSap();
            BE_GASTO.ResponseSolicitudRendicionSap oResponseSolicitudRendicionSap = new BE_GASTO.ResponseSolicitudRendicionSap();

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            //bool validaregla = false;

            AnticipoApi oAnticipoEnviarSap = new AnticipoApi();
            LiquidacionApi oLiquidacionEnviarSap = new LiquidacionApi();
            CajaApi oCajaEnviarSap = new CajaApi();
            TarjetaApi oTarjetaEnviarSap = new TarjetaApi();

            BE_GASTO.Solicitud oSolicitud = new BE_GASTO.Solicitud();
            DateTime fecharegistro = DateTime.Now;

            //bool Error_Servicio = false;
            //bool flag_Repite = false;
            //string Error_ServicioMessage = "";
            string IdAlternativo = "";

            BE.OperationResult objResult = null;
            for (int i = 0; i <= arrLista.Length - 1; i++)
            {
                oSolicitud = await oISolicitudService_Gasto.GetDatoSolicitudByID(int.Parse(arrLista[i]));
                oEnviarAprobar = new SolicitudAprobarSapViewModel();
                oEnviarAprobar.IdSolicitud = int.Parse(arrLista[i]);
                oEnviarAprobar.Codigo = oSolicitud.Codigo;
                oEnviarAprobar.IdSituacionServicio = oSolicitud.IdSituacionServicio;
                oEnviarAprobar.IdTipo = (int)oSolicitud.IdTipo;
                oEnviarAprobar.CuentaContable = "";
                oEnviarAprobar.CodigoLiquidacion = "";
                oEnviarAprobar.TipoSolicitud = oSolicitud.TipoSolicitud;
                oEnviarAprobar.ConDetalle = false;
                oEnviarAprobar.ExisteOrdenSolicitudInvalida = false;
                oEnviarAprobar.listaDetRendicionSolicitud = new List<BE_GASTO.DetRendicionSolicitud>();
                oEnviarAprobar.listaEnviarSapAnticipo = new List<AnticipoApi>();
                oEnviarAprobar.listaEnviarSapLiquidacion = new List<LiquidacionApi>();
                oEnviarAprobar.listaEnviarSapCaja = new List<CajaApi>();
                oEnviarAprobar.listaEnviarSapTarjeta = new List<TarjetaApi>();

                if (oEnviarAprobar.IdSituacionServicio == 3 && oEnviarAprobar.IdTipo == 1)
                {
                    oAnticipoEnviarSap = new AnticipoApi();
                    oAnticipoEnviarSap.BUKRS = oSolicitud.IdSociedad;
                    oAnticipoEnviarSap.VIAJE = oSolicitud.Codigo;
                    oAnticipoEnviarSap.CORREL = "01";
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    //EDIAZ INI 13.02.2020, se cambia IDAlternativo por dni
                    //if (oUsuario.ID == 0) oUsuario.IDAlternativo = "";
                    //oAnticipoEnviarSap.LIFNR = oUsuario.IDAlternativo;
                    if (oUsuario.ID == 0)
                    {
                        oUsuario.IDAlternativo = "";
                        oUsuario.NumeroDocumento = "";
                    }
                    oAnticipoEnviarSap.LIFNR = oUsuario.NumeroDocumento;
                    //EDIAZ FIN 13.02.2020
                    oAnticipoEnviarSap.WAERS = oSolicitud.Moneda;
                    oAnticipoEnviarSap.WRBTR = (float)oSolicitud.ImporteSolicitud;
                    oAnticipoEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                    fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                    oAnticipoEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                    oEnviarAprobar.EnviarSap = true;
                    oEnviarAprobar.ConDetalle = false;
                    entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                    entDetRendicionSolicitud.EnvioAnticipo = true;
                    oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                    oEnviarAprobar.listaEnviarSapAnticipo.Add(oAnticipoEnviarSap);
                }
                else if ((oEnviarAprobar.IdSituacionServicio == 10 && oEnviarAprobar.IdTipo == 1) || (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 2))
                {

                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    //EDIAZ INI 13.02.2020, se setea a variable IdAlternativo el DNI
                    //if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    if (oUsuario.ID == 0)
                    {
                        IdAlternativo = "";
                    }
                    else
                    {
                        IdAlternativo = oUsuario.NumeroDocumento;
                    }
                    //EDIAZ FIN 13.02.2020
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oLiquidacionEnviarSap = new LiquidacionApi();
                            oLiquidacionEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oLiquidacionEnviarSap.VIAJE = oSolicitud.Codigo;
                            oLiquidacionEnviarSap.LIFNR = IdAlternativo;
                            oLiquidacionEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oLiquidacionEnviarSap.LIFNR2 = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oLiquidacionEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            oLiquidacionEnviarSap.BLDAT2 = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oLiquidacionEnviarSap.CORREL = item.CORREL;
                            oLiquidacionEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oLiquidacionEnviarSap.NAME2 = item.Externo == true ? item.Nombre2 : "";
                            oLiquidacionEnviarSap.NAME3 = item.Externo == true ? item.Nombre3 : "";
                            oLiquidacionEnviarSap.NAME4 = item.Externo == true ? item.Nombre4 : "";
                            oLiquidacionEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";

                            oLiquidacionEnviarSap.BLART = item.Clase;
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oLiquidacionEnviarSap.XBLNR = nrocomprobante;
                            oLiquidacionEnviarSap.WAERS = oSolicitud.Moneda;
                            oLiquidacionEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oLiquidacionEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oLiquidacionEnviarSap.MWSKZ = item.Impuesto;
                            oLiquidacionEnviarSap.ZE_CLASGTO = item.CodigoGastoViaje;
                            oLiquidacionEnviarSap.HKONT = item.CuentaContable;
                            oLiquidacionEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oLiquidacionEnviarSap.AUFNR = item.OrdenServicio;
                            oLiquidacionEnviarSap.GLOSA = "";
                            oLiquidacionEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oLiquidacionEnviarSap.ZE_AOR = oEnviarAprobar.IdTipo == 1 ? "A" : "R";
                            oLiquidacionEnviarSap.SGTXT = item.GlosaDetalle;
                            oLiquidacionEnviarSap.TRATA = item.IdTipoDocumento == 23 ? item.Tratamiento : "";
                            oLiquidacionEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oLiquidacionEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oLiquidacionEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";
                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            entDetRendicionSolicitud.EnvioAnticipo = false;
                            entDetRendicionSolicitud.TipoDocumento = item.TipoDocumento;
                            entDetRendicionSolicitud.NroComprobante = item.NroComprobante;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            if (item.OrdenServicio != "")
                                oEnviarAprobar.ExisteOrdenSolicitudInvalida = await SolicitudOrdenServicioInvalida(oSolicitud.IdSociedad, item.OrdenServicio);
                            if (!oEnviarAprobar.ExisteOrdenSolicitudInvalida)
                                oEnviarAprobar.listaEnviarSapLiquidacion.Add(oLiquidacionEnviarSap);
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else if (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 3) //CAJA
                {
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    //EDIAZ INI 13.02.2020, se setea IDAlternativo por DNI
                    //if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    if (oUsuario.ID == 0)
                    {
                        IdAlternativo = "";
                    }
                    else
                    {
                        IdAlternativo = oUsuario.NumeroDocumento;
                    }
                    //EDIAZ FIN 13.02.2020
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oCajaEnviarSap = new CajaApi();
                            oCajaEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oCajaEnviarSap.FONDNR = oSolicitud.Codigo;
                            oCajaEnviarSap.CORREL = item.CORREL;
                            oCajaEnviarSap.LIFNR_A = IdAlternativo;
                            oCajaEnviarSap.FECLIQ = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oCajaEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oCajaEnviarSap.LIFNR_P = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oCajaEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oCajaEnviarSap.NAME2 = item.Externo == true ? item.Nombre2 : "";
                            oCajaEnviarSap.NAME3 = item.Externo == true ? item.Nombre3 : "";
                            oCajaEnviarSap.NAME4 = item.Externo == true ? item.Nombre4 : "";
                            oCajaEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";
                            oCajaEnviarSap.BLART = item.Clase;
                            oCajaEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oCajaEnviarSap.XBLNR = nrocomprobante;
                            oCajaEnviarSap.WAERS = oSolicitud.Moneda;
                            oCajaEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oCajaEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oCajaEnviarSap.MWSKZ = item.Impuesto;
                            oCajaEnviarSap.CLASGTO = item.CodigoGastoViaje;
                            oCajaEnviarSap.HKONT = item.CuentaContable;
                            oCajaEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oCajaEnviarSap.AUFNR = item.OrdenServicio;
                            oCajaEnviarSap.GLOSA = "";
                            oCajaEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oCajaEnviarSap.AOR = "";
                            oCajaEnviarSap.SGTXT = item.GlosaDetalle;
                            oCajaEnviarSap.TRATA = item.IdTipoDocumento == 23 ? item.Tratamiento : "";
                            oCajaEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oCajaEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oCajaEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";
                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            entDetRendicionSolicitud.EnvioAnticipo = false;
                            entDetRendicionSolicitud.TipoDocumento = item.TipoDocumento;
                            entDetRendicionSolicitud.NroComprobante = item.NroComprobante;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            oEnviarAprobar.listaEnviarSapCaja.Add(oCajaEnviarSap);
                        }
                    }
                }
                else if (oEnviarAprobar.IdSituacionServicio == 4 && oEnviarAprobar.IdTipo == 4)  // TARJETA
                {
                    BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)oSolicitud.IdUsuario);
                    //EDIAZ FINI 13.02.2020, se setea a IDAlternativo el DNI
                    //if (oUsuario.ID == 0) IdAlternativo = ""; else IdAlternativo = oUsuario.IDAlternativo;
                    if (oUsuario.ID == 0)
                    {
                        IdAlternativo = "";
                    }
                    else
                    {
                        IdAlternativo = oUsuario.NumeroDocumento;
                    }
                    //FIN EDIAZ 13.02.2020
                    oEnviarAprobar.ConDetalle = true;
                    var listaDetalleRendicion = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalleLista(oEnviarAprobar.IdSolicitud);
                    if (listaDetalleRendicion != null && listaDetalleRendicion.Count > 0)
                    {
                        foreach (BE_GASTO.DetRendicionSolicitud item in listaDetalleRendicion)
                        {
                            oTarjetaEnviarSap = new TarjetaApi();
                            oTarjetaEnviarSap.BUKRS = oSolicitud.IdSociedad;
                            oTarjetaEnviarSap.TCRENR = oSolicitud.Codigo;
                            oTarjetaEnviarSap.CORREL = item.CORREL;
                            oTarjetaEnviarSap.LIFNR_A = IdAlternativo;
                            oTarjetaEnviarSap.FECLIQ = (DateTime.Parse(item.FechaStr)).ToString("dd.MM.yyyy");
                            oTarjetaEnviarSap.BUPLA = oSolicitud.CodigoLocal;
                            oTarjetaEnviarSap.LIFNR_P = item.Externo == true ? "" : item.CodigoSAPProveedor;
                            oTarjetaEnviarSap.NAME1 = item.Externo == true ? item.RazonSocial : "";
                            oTarjetaEnviarSap.NAME2 = item.Externo == true ? item.Nombre2 : "";
                            oTarjetaEnviarSap.NAME3 = item.Externo == true ? item.Nombre3 : "";
                            oTarjetaEnviarSap.NAME4 = item.Externo == true ? item.Nombre4 : "";
                            oTarjetaEnviarSap.STCD1 = item.Externo == true ? item.IdTipoDocumento == 23 ? item.RUT : item.RUC : "";
                            oTarjetaEnviarSap.BLART = item.Clase;
                            oTarjetaEnviarSap.BLDAT = fecharegistro.ToString("dd.MM.yyyy");
                            string nrocomprobante = item.NroComprobante;
                            int idtipodocumento = (int)item.IdTipoDocumento;
                            string[] nro = nrocomprobante.Split('-');
                            switch (idtipodocumento)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 5:
                                case 6:
                                case 10:
                                case 12:
                                case 14:
                                case 23:
                                    nrocomprobante = nro[1] + "-" + nro[2];
                                    break;
                                case 11:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                    nrocomprobante = nro[0] + "-" + nro[1];
                                    break;
                            }
                            oTarjetaEnviarSap.XBLNR = nrocomprobante;
                            oTarjetaEnviarSap.WAERS = oSolicitud.Moneda;
                            oTarjetaEnviarSap.WRBTR = float.Parse(item.ImporteStr);
                            oTarjetaEnviarSap.BINAF = float.Parse(item.BaseInafectaStr);
                            oTarjetaEnviarSap.MWSKZ = item.Impuesto;
                            oTarjetaEnviarSap.HKONT = item.CuentaContable;
                            oTarjetaEnviarSap.CLASGTO = item.CodigoGastoViaje;
                            oTarjetaEnviarSap.KOSTL = item.CentroCostoAfectoCodigoSap;
                            oTarjetaEnviarSap.AUFNR = item.OrdenServicio;
                            oTarjetaEnviarSap.GLOSA = "";
                            oTarjetaEnviarSap.STKZN = EsPersonaNatural(item.Externo, item.IdTipoDocumento == 23 ? item.RUT : item.RUC);
                            oTarjetaEnviarSap.AOR = "";
                            oTarjetaEnviarSap.SGTXT = item.GlosaDetalle;
                            oTarjetaEnviarSap.TRATA = item.IdTipoDocumento == 23 ? item.Tratamiento : "";
                            oTarjetaEnviarSap.CALLE = item.IdTipoDocumento == 23 ? item.Calle : "";
                            oTarjetaEnviarSap.POBLA = item.IdTipoDocumento == 23 ? item.Poblacion : "";
                            oTarjetaEnviarSap.PAIS = item.IdTipoDocumento == 23 ? item.CodAbreviatura : "";
                            fecharegistro = (DateTime)oSolicitud.FechaRegistro;
                            oEnviarAprobar.EnviarSap = true;
                            entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                            entDetRendicionSolicitud.IdDetRendicionSolicitud = item.IdDetRendicionSolicitud;
                            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                            entDetRendicionSolicitud.IdTipo = oEnviarAprobar.IdTipo;
                            entDetRendicionSolicitud.CORREL = item.CORREL;
                            entDetRendicionSolicitud.EnvioAnticipo = false;
                            entDetRendicionSolicitud.TipoDocumento = item.TipoDocumento;
                            entDetRendicionSolicitud.NroComprobante = item.NroComprobante;
                            oEnviarAprobar.listaDetRendicionSolicitud.Add(entDetRendicionSolicitud);
                            oEnviarAprobar.listaEnviarSapTarjeta.Add(oTarjetaEnviarSap);
                        }
                    }
                }
                else
                {
                    oEnviarAprobar.ConDetalle = false;
                    oEnviarAprobar.EnviarSap = false;
                }
                //if (!oEnviarAprobar.ExisteOrdenSolicitudInvalida)
                listaAprobar.Add(oEnviarAprobar);
            }

            for (int i = 0; i <= listaAprobar.Count - 1; i++)
            {
                //incicio envio anticipo
                if (listaAprobar[i].listaEnviarSapAnticipo != null && listaAprobar[i].listaEnviarSapAnticipo.Count > 0)
                {
                    var listaresponse = new List<AnticipoApi>();
                    var return_lista = await oIProveedorGastoWebApiService.PostEnviarAnticipo(listaAprobar[i].listaEnviarSapAnticipo);
                    if (return_lista.Success)
                    {
                        listaresponse = (List<AnticipoApi>)return_lista.ResponseObject;
                        if (listaresponse != null && listaresponse.Count > 0)
                        {
                            foreach (var item in listaresponse)
                            {
                                if (item.VIAJE == listaAprobar[i].Codigo)
                                {
                                    if (item.BELNR != "")
                                    {
                                        listaAprobar[i].CuentaContable = item.BELNR;
                                        listaAprobar[i].EnviadoSap = true;
                                        listaAprobar[i].Error_Servicio = false;
                                        listaAprobar[i].Error_ServicioMessage = item.MENSAJE;
                                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = false;
                                        //listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = item.MENSAJE;
                                        //NEY TANGOA 18/09/2020
                                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = item.MENSAJEC;
                                    }
                                    else
                                    {
                                        listaAprobar[i].EnviadoSap = false;
                                        listaAprobar[i].Error_Servicio = true;
                                        listaAprobar[i].Error_ServicioMessage = item.MENSAJE;
                                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                                        //listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = item.MENSAJE;
                                        //NEY TANGOA 18/09/2020
                                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = item.MENSAJEC;
                                    }
                                }
                            }
                        }
                        else
                        {
                            listaAprobar[i].Error_Servicio = true;
                            listaAprobar[i].Error_ServicioMessage = "No se generaron los registros enviados";
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "No se generaron los registros enviados";
                        }
                    }
                    else
                    {
                        listaAprobar[i].Error_Servicio = true;
                        listaAprobar[i].Error_ServicioMessage = "Error al retornar los datos enviados";
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "Error al retornar los datos enviados";
                    }
                } // fin envio anticipo
                //incio envio liquidacin anticipo y reembolso
                if (listaAprobar[i].listaEnviarSapLiquidacion != null && listaAprobar[i].listaEnviarSapLiquidacion.Count > 0)
                {
                    var listaresponse = new List<LiquidacionApi>();
                    var return_lista = await oIProveedorGastoWebApiService.PostEnviarLiquidacion(listaAprobar[i].listaEnviarSapLiquidacion);
                    if (return_lista.Success)
                    {
                        listaresponse = (List<LiquidacionApi>)return_lista.ResponseObject;
                        if (listaresponse != null && listaresponse.Count > 0)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.VIAJE == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                        {
                                            if (item.BELNR != "" && (item.BELNRC != "" || item.BELNRR != ""))
                                            {
                                                listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = item.BELNR;
                                                listaAprobar[i].CodigoLiquidacion = item.BELNRC;
                                                listaAprobar[i].CodigoReembolso = item.BELNRR;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = false;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC;
                                                listaAprobar[i].EnviadoSap = true;
                                            }
                                            else
                                            {
                                                listaAprobar[i].EnviadoSap = false;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = true;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            listaAprobar[i].EnviadoSap = false;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "No se generaron los registros enviados";
                        }
                    }
                    else
                    {
                        listaAprobar[i].EnviadoSap = false;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "Error al retornar los datos enviados";
                    }
                }
                //fin envio liquidacin anticipo y reembolso
                //inicio envio caja
                if (listaAprobar[i].listaEnviarSapCaja != null && listaAprobar[i].listaEnviarSapCaja.Count > 0)
                {
                    var listaresponse = new List<CajaApi>();
                    string json = JsonConvert.SerializeObject(listaAprobar[i].listaEnviarSapCaja);
                    var return_lista = await oIProveedorGastoWebApiService.PostEnviarCaja(listaAprobar[i].listaEnviarSapCaja);
                    if (return_lista.Success)
                    {
                        listaresponse = (List<CajaApi>)return_lista.ResponseObject;
                        if (listaresponse != null && listaresponse.Count > 0)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.FONDNR == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                        {
                                            if (item.BELNR_LIQ != "" && item.BELNR_COMP != "")
                                            {
                                                listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = item.BELNR_LIQ;
                                                listaAprobar[i].CodigoLiquidacion = item.BELNR_LIQ;
                                                listaAprobar[i].CodigoReembolso = item.BELNR_COMP;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = false;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC;
                                                listaAprobar[i].EnviadoSap = true;
                                            }
                                            else
                                            {
                                                listaAprobar[i].EnviadoSap = false;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = true;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC == null ? item.MENSAJE : item.MENSAJEC;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            listaAprobar[i].EnviadoSap = false;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "No se generaron los registros enviados";
                        }
                    }
                    else
                    {
                        listaAprobar[i].EnviadoSap = false;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "Error al retornar los datos enviados";
                    }
                }
                //fin envio caja
                //inicio envio tarjeta
                if (listaAprobar[i].listaEnviarSapTarjeta != null && listaAprobar[i].listaEnviarSapTarjeta.Count > 0)
                {
                    var listaresponse = new List<TarjetaApi>();
                    var return_lista = await oIProveedorGastoWebApiService.PostEnviarTarjeta(listaAprobar[i].listaEnviarSapTarjeta);
                    if (return_lista.Success)
                    {
                        listaresponse = (List<TarjetaApi>)return_lista.ResponseObject;
                        if (listaresponse != null && listaresponse.Count > 0)
                        {
                            if (listaAprobar[i].listaDetRendicionSolicitud != null && listaAprobar[i].listaDetRendicionSolicitud.Count > 0)
                            {
                                for (int x = 0; x <= listaAprobar[i].listaDetRendicionSolicitud.Count - 1; x++)
                                {
                                    foreach (var item in listaresponse)
                                    {
                                        if (item.TCRENR == listaAprobar[i].Codigo && item.CORREL == listaAprobar[i].listaDetRendicionSolicitud[x].CORREL)
                                        {
                                            if (item.BELNR_LIQ != "" && item.BELNR_COMP != "")
                                            {
                                                listaAprobar[i].listaDetRendicionSolicitud[x].CodigoLiquidacion = item.BELNR_LIQ;
                                                listaAprobar[i].CodigoLiquidacion = item.BELNR_LIQ;
                                                listaAprobar[i].CodigoReembolso = item.BELNR_COMP;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = false;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC;
                                                listaAprobar[i].EnviadoSap = true;
                                            }
                                            else
                                            {
                                                listaAprobar[i].EnviadoSap = false;
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_Servicio = true;
                                                //listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJE;
                                                //NEY TANGOA 18/09/2020
                                                listaAprobar[i].listaDetRendicionSolicitud[x].Error_ServicioMessage = item.MENSAJEC;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            listaAprobar[i].EnviadoSap = false;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                            listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "No se generaron los registros enviados";
                        }
                    }
                    else
                    {
                        listaAprobar[i].EnviadoSap = false;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_Servicio = true;
                        listaAprobar[i].listaDetRendicionSolicitud[0].Error_ServicioMessage = "Error al retornar los datos enviados";
                    }
                }
                //fin envio tarjeta
            } // fin lista
              //if (!Error_Servicio)
              //{
            listaResponseSolicitudSap = new List<BE_GASTO.ResponseSolicitudSap>();
            foreach (var item in listaAprobar)
            {
                entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                entSolicitud.IdSolicitud = (long)item.IdSolicitud;
                entSolicitud.CuentaContable = item.CuentaContable;
                entSolicitud.CodigoLiquidacion = item.CodigoLiquidacion;
                entSolicitud.CodigoReembolso = item.CodigoReembolso;

                oResponseSolicitudSap = new BE_GASTO.ResponseSolicitudSap();
                oResponseSolicitudSap.Codigo = item.Codigo;
                oResponseSolicitudSap.TipoSolicitud = item.TipoSolicitud;
                oResponseSolicitudSap.CabeceraOk = "Error";
                oResponseSolicitudSap.MensajeSolicitud = item.Error_ServicioMessage;
                oResponseSolicitudSap.listaResponseSolicitudRendicionSap = new List<BE_GASTO.ResponseSolicitudRendicionSap>();
                if (!item.EnviarSap)
                {
                    entSolicitud.CuentaContable = "";
                    entSolicitud.CodigoLiquidacion = "";
                    entSolicitud.CodigoReembolso = "";
                    if (!item.ConDetalle)
                    {
                        objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                        oResponseSolicitudSap.MensajeSolicitud = objResult.NewID == 1 ? item.Error_ServicioMessage : "Error al actualizar los datos de cabecera";
                        oResponseSolicitudSap.CabeceraOk = objResult.NewID == 1 ? "Ok" : "Error";
                        if (item.Error_ServicioMessage == null || item.Error_ServicioMessage == "") oResponseSolicitudSap.MensajeSolicitud = "Solicitud aprobada correctamente";


                        //----
                        //oResponseSolicitudSap.listaResponseSolicitudRendicionSap = new List<BE_GASTO.ResponseSolicitudRendicionSap>();
                        //oResponseSolicitudRendicionSap = new BE_GASTO.ResponseSolicitudRendicionSap();
                        //oResponseSolicitudRendicionSap.Correlativo = "112";
                        //oResponseSolicitudRendicionSap.TipoDocumento = "factuara";
                        //oResponseSolicitudRendicionSap.NroDocumento = "43423432";
                        //oResponseSolicitudRendicionSap.MensajeRendicion = "mesanesf";
                        //oResponseSolicitudRendicionSap.DetalleOk = "Error";
                        //oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                        //oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                        //oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                        //oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                        //--

                    }
                }
                else
                {
                    if (item.EnviadoSap)
                    {
                        bool ExisteCodigoLiquidacionVacionEnLista = false;

                        if (item.IdTipo == 1 && item.IdSituacionServicio == 3)
                        {
                            objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                            oResponseSolicitudSap.MensajeSolicitud = objResult.NewID == 1 ? item.Error_ServicioMessage : "Error al actualizar los datos de cabecera";
                            oResponseSolicitudSap.CabeceraOk = objResult.NewID == 1 ? "Ok" : "Error";
                            oResponseSolicitudSap.listaResponseSolicitudRendicionSap = new List<BE_GASTO.ResponseSolicitudRendicionSap>();
                        }
                        else
                        {
                            oResponseSolicitudSap.listaResponseSolicitudRendicionSap = new List<BE_GASTO.ResponseSolicitudRendicionSap>();

                            if ((item.CodigoLiquidacion != null && item.CodigoLiquidacion != "") || item.CodigoReembolso != null && item.CodigoReembolso != "")
                            {
                                if (item.listaDetRendicionSolicitud != null && item.listaDetRendicionSolicitud.Count > 0)
                                {
                                    foreach (var itemdetalle in item.listaDetRendicionSolicitud)
                                    {
                                        if (itemdetalle.CodigoLiquidacion == null || itemdetalle.CodigoLiquidacion.Trim() == "")
                                        {
                                            ExisteCodigoLiquidacionVacionEnLista = true;
                                        }
                                    }

                                    foreach (var itemdetalle in item.listaDetRendicionSolicitud)
                                    {
                                        oResponseSolicitudRendicionSap = new BE_GASTO.ResponseSolicitudRendicionSap();
                                        oResponseSolicitudRendicionSap.Correlativo = itemdetalle.CORREL;
                                        oResponseSolicitudRendicionSap.TipoDocumento = itemdetalle.TipoDocumento;
                                        oResponseSolicitudRendicionSap.NroDocumento = itemdetalle.NroComprobante;
                                        oResponseSolicitudRendicionSap.MensajeRendicion = itemdetalle.Error_ServicioMessage;
                                        oResponseSolicitudRendicionSap.DetalleOk = "Error";
                                        if (itemdetalle.CodigoLiquidacion != null && itemdetalle.CodigoLiquidacion != "" && !ExisteCodigoLiquidacionVacionEnLista)
                                        {
                                            objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidadDetalle(itemdetalle);
                                            oResponseSolicitudRendicionSap.MensajeRendicion = objResult.NewID == 1 ? itemdetalle.Error_ServicioMessage : "Error al actualizar los datos del detalle";
                                            oResponseSolicitudRendicionSap.DetalleOk = objResult.NewID == 1 ? "Ok" : "Error";
                                            oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                                        }
                                    }
                                }
                                if (!ExisteCodigoLiquidacionVacionEnLista)
                                {
                                    objResult = new BL_GASTO.Solicitud.Solicitud().AprobarContabilidad(entSolicitud);
                                    oResponseSolicitudSap.MensajeSolicitud = objResult.NewID == 1 ? item.Error_ServicioMessage : "Error al actualizar los datos de cabecera";
                                    oResponseSolicitudSap.CabeceraOk = objResult.NewID == 1 ? "Ok" : "Error";
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var itemdetalle in item.listaDetRendicionSolicitud)
                        {
                            oResponseSolicitudRendicionSap = new BE_GASTO.ResponseSolicitudRendicionSap();
                            oResponseSolicitudRendicionSap.Correlativo = itemdetalle.CORREL;
                            oResponseSolicitudRendicionSap.TipoDocumento = itemdetalle.TipoDocumento;
                            oResponseSolicitudRendicionSap.NroDocumento = itemdetalle.NroComprobante;
                            oResponseSolicitudRendicionSap.MensajeRendicion = itemdetalle.Error_ServicioMessage;
                            oResponseSolicitudRendicionSap.DetalleOk = "Error";
                            //oResponseSolicitudRendicionSap.DetalleOk = itemdetalle.Error_ServicioMessage;
                            oResponseSolicitudSap.listaResponseSolicitudRendicionSap.Add(oResponseSolicitudRendicionSap);
                        }
                    }
                }
                listaResponseSolicitudSap.Add(oResponseSolicitudSap);
            }
            return Json(listaResponseSolicitudSap, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaEstadosPorTipo_PV(int idtipo)
        {
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == idtipo).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;
            return PartialView("_ListaEstadosPorTipo");
        }
        [HttpPost]
        public async Task<JsonResult> RechazarPost(int idsolicitud)
        {

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            entSolicitud.IdSolicitud = idsolicitud;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.Solicitud.Solicitud().RechazarContabilidad(entSolicitud);
            return Json(objResult);
        }

        [HttpPost]
        public async Task<JsonResult> CambiarEstadoSolicitudPost(int id, int idestado)
        {

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            entSolicitud.IdSolicitud = id;
            entSolicitud.IdSituacionServicio = idestado;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.Solicitud.Solicitud().CambiarEstadoSolicitud(entSolicitud);
            return Json(objResult);
        }


        [HttpGet]
        public JsonResult GetListDetalleRendirGasto(int? IdSolicitud = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.DetRendicionSolicitud entRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entRendicionSolicitud.PageSize = rows;
            entRendicionSolicitud.PageIndex = page;
            entRendicionSolicitud.IdRendicionSolicitud = (long)IdSolicitud;

            var lista = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Paginado(entRendicionSolicitud, out intTotalRows, out intCantidadRegistros);
            var listafull = GetListDetalleRendirGastofull(IdSolicitud, page, rows);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,
                rowsfull = listafull
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDetalleRendirGastofull(int? IdSolicitud = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.DetRendicionSolicitud entRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entRendicionSolicitud.PageSize = 999;
            entRendicionSolicitud.PageIndex = 1;
            entRendicionSolicitud.IdRendicionSolicitud = (long)IdSolicitud;

            var lista = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Paginado(entRendicionSolicitud, out intTotalRows, out intCantidadRegistros);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerTipoCambioRFC(string fecha)
        {
            var tipoCambio = new TipoCambioApi();
            var response = new TipoCambioApi();
            tipoCambio.KURST = "M";
            tipoCambio.FCURR = "USD";
            tipoCambio.TCURR = "PEN";
            tipoCambio.GDATU = (DateTime.Parse(fecha)).ToString("dd.MM.yyyy");
            var dict_lista = await oIProveedorGastoWebApiService.PostTipoCambio(tipoCambio);
            if (dict_lista.Success)
            {
                var lista = (List<TipoCambioApi>)dict_lista.ResponseObject;
                if (lista != null && lista.Count > 0)
                {
                    response = lista[0];
                }
            }
            var jsonData = new
            {
                Success = (response != null ? true : false),
                TipoCambio = response
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public async Task<JsonResult> EliminarDetalleGastoPost(int id)
        {

            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = id;
            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Eliminar(entDetRendicionSolicitud);
            return Json(objResult);
        }

        public async Task<PartialViewResult> ConfirmacionEliminarDetalleGasto(int id)
        {
            ViewBag.hdfIdDetRendicionSolicitudEliminar = id;
            return PartialView("_ConfirmacionDeleteDetalleGasto");
        }

        private async Task<BE_GASTO.Solicitud> GetSolicitud(int IdSolicitud)
        {

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);
            return entSolicitud;
        }

        public async Task<PartialViewResult> ConfirmacionCambioEstado(int id)
        {
            var entidad = await GetSolicitud(id);
            SolicitudCambioEstado model = new SolicitudCambioEstado();
            model.IdSolicitud = id;
            model.IdTipo = (int)entidad.IdTipo;
            model.IdSituacionServicio = entidad.IdSituacionServicio;
            model.Codigo = entidad.Codigo == "" ? entidad.IdSolicitud.ToString() : entidad.Codigo;
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == model.IdTipo).ToList();
            ViewBag.listaEstados = listaEstados;
            return PartialView("_ConfirmacionCambioEstado", model);
        }

        public PartialViewResult BuscarSolicitudExterior()
        {
            return PartialView("_BuscarSolicitudExteriorModal");
        }

        [HttpPost]
        public async Task<JsonResult> ValidarDatosProveedor(string sociedad, string documento)
        {
            var response = new List<ProveedorResponse>();
            if (documento != "")
            {
                var result = await oIProveedorGastoWebApiService.GetDatosProveedor(sociedad, documento);
                if (result.Success)
                {
                    response = (List<ProveedorResponse>)result.ResponseObject;
                }
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> ValidarDatosProveedorExterno(string documento)
        {
            var response = new BE_GASTO.Proveedor();
            if (documento != "")
            {
                var result = await oISolicitudServiceLogic_Gasto.ValidarDatosProveedorExterno(documento);
                if (result != null)
                {
                    response = result;
                }
            }
            var jsonData = new
            {
                Success = (response != null ? true : false),
                respuesta = response
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListSolicitudAprobacion(Int64? idusuario = null, int? idsituacionservicio = null, string fechaDesde = "", string fechaHasta = "", string idcentrocosto = null, string idsociedad = null, int? idsucursal = null, string anioejercicio = null, int? idtipo = null, string codigo = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;
            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;
            string UsuarioSociedad = UtilSession.Usuario.IdSociedad;

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

            var lista = new BL_GASTO.Solicitud.Solicitud().PaginadoAprobacionContabilidad(entSolicitud, UsuarioSociedad ,out intTotalRows, out intCantidadRegistros);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,

            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaAdjuntoDG_PV(int? IdDetRendicionSolicitud = null)
        {
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud == null ? 0 : (int)IdDetRendicionSolicitud;
            entDetRendicionSolicitud = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalle(entDetRendicionSolicitud);
            var modelo = new DetalleSolicitudViewModel();
            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
            if (entDetRendicionSolicitud != null)
            {
                if (entDetRendicionSolicitud.NombreFile != null || entDetRendicionSolicitud.NombreFile != "")
                {
                    BE_GASTO.Adjunto adjunto = new BE_GASTO.Adjunto() { Alias = entDetRendicionSolicitud.NombreFile, NombreAdjunto = entDetRendicionSolicitud.NombreFile, PathAdjunto = entDetRendicionSolicitud.RutaCompletaFile };
                    List<BE_GASTO.Adjunto> adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };
                    Session["ListaAdjuntoDG"] = adjuntoList;
                    modelo.DetRendicionSolicitud.ListaAdjunto.Add(adjunto);
                }
            }
            return PartialView("_AttachmentDetalleGasto", modelo);
        }


        public ActionResult RemoveFileDG()
        {
            var adjuntoList = new List<BE_GASTO.Adjunto>();

            var modelo = new DetalleSolicitudViewModel()
            {

            };
            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
            modelo.DetRendicionSolicitud.ListaAdjunto = adjuntoList;

            Session["ListaAdjuntoDG"] = adjuntoList;

            return PartialView("_AttachmentDetalleGasto", modelo);
        }

        public ActionResult UploadFileDG(HttpPostedFileBase file, int IdDetRendicionSolicitud)
        {
            try
            {
                string absolutePath = AppCode.Util.ServerPath("~/FilesDetallesGastos");
                var nombreArchivo = file.FileName.Split('.')[0];

                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }

                string alias = string.Concat(nombreArchivo, '-', IdDetRendicionSolicitud, Path.GetExtension(file.FileName));
                string rutaCompleta = Path.Combine(absolutePath, alias);
                if (System.IO.File.Exists(rutaCompleta))
                {
                    return Json(new { code = 3, message = "El archivo ya existe por favor ingrese uno nuevo", partial = string.Empty });
                }
                else
                {
                    
                    file.SaveAs(rutaCompleta);

                    var adjunto = new BE_GASTO.Adjunto() { Alias = alias, NombreAdjunto = file.FileName, PathAdjunto = rutaCompleta };

                    var adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };

                    Session["ListaAdjuntoDG"] = adjuntoList;

                    var modelo = new DetalleSolicitudViewModel();
                    modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                    modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
                    modelo.DetRendicionSolicitud.ListaAdjunto = adjuntoList;
                    //{ ListaAdjunto = adjuntoList };


                    var view = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_AttachmentDetalleGasto", modelo);

                    return Json(new { code = 1, message = String.Empty, partial = view });
                }


            }
            catch (Exception ex)
            {

                return Json(new { code = 0, message = ex.Message, partial = string.Empty });
            }
        }

        [HttpGet]
        public ActionResult DescargarExcel(int IdSolicitud)
        {
            var arrayFileDetalleRendicion = GetReportExcel(IdSolicitud);
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            string rutaDocumento = Path.Combine(rutaDestinoPDF, "DetalleRendicionGastoExcel.xls");
            GuardarArchivo(arrayFileDetalleRendicion, "DetalleRendicionGastoExcel.xls");
            return File(rutaDocumento, "application/xls", "DetalleRendicionGastoExcel.xls");
        }


        [HttpGet]
        public ActionResult DescargarPDF(bool? pdf, int IdSolicitud)
        {
            var arrayFileDetalleRendicion = GetReportPDF(IdSolicitud);
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            string rutaDocumento = Path.Combine(rutaDestinoPDF, "DetalleRendicionGastoPDF.pdf");
            GuardarArchivo(arrayFileDetalleRendicion, "DetalleRendicionGastoPDF.pdf");
            return File(rutaDocumento, "application/pdf", "DetalleRendicionGastoPDF.pdf");
        }
        private byte[] GetReportPDF(int IdSolicitud)
        {
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            List<BE_GASTO.DetRendicionSolicitud> objLista = (List<BE_GASTO.DetRendicionSolicitud>)new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().PaginadoAll(IdSolicitud);
            string strPath = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReporteDetalleRendicion.rdlc"));
            string[] arrDataSet = new string[] { "ReportDetalleRendicion" };
            object[] arrDataSource = new object[] { objLista };
            var parameters = new Dictionary<string, string>() {
                { "pUsuario", modelo.NomApe},
                { "pRUC", modelo.NumeroDocumento},
                { "pCECO", modelo.CECO=="" ? "------" : modelo.CECO},
                { "pCodigo", modelo.Codigo},
                { "pFecha", modelo.FechaRegistroStr},
                { "pGastoTotal", modelo.TotalRendidoStr},
                { "pTotalAsignado", modelo.ImporteSolicitudStr},
                { "pCodigoOS", "------"},
                { "pSociedad", modelo.NombreCO},
                { "pAprobador", modelo.Aprobador},
                { "pMotivo",  modelo.Motivo=="" ? "------" : modelo.Motivo},
                { "pFechaDel", modelo.FechaInicioStr},
                { "pFechaAl", modelo.FechaFinStr},
                { "pFechaContabilidad", modelo.FechaContabilidadStr=="01/01/1900" ? "------" : modelo.FechaContabilidadStr}, //EDIAZ 11.03.2020, validacion pq la fecha a veces llegaba en NULL
                { "pSucursal", modelo.Sucursal},
                { "pTipo", modelo.Tipo}
            };
            byte[] bytFile = new Export().CreatePdf(strPath, arrDataSet, arrDataSource, parameters);
            return bytFile;
        }

        private byte[] GetReportExcel(int IdSolicitud)
        {
            //var modelo = new DetalleSolicitudViewModel();
            //modelo = getDetalleSolicitud(IdSolicitud);
            List<BE_GASTO.DetRendicionSolicitud> objLista = (List<BE_GASTO.DetRendicionSolicitud>)new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().PaginadoAll(IdSolicitud);
            string strPath = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReporteDetalleRendicionXLS.rdlc"));
            string[] arrDataSet = new string[] { "ReportDetalleRendicion" };
            object[] arrDataSource = new object[] { objLista };
            byte[] bytFile = new Export().CreateExcel(strPath, arrDataSet, arrDataSource, null);
            return bytFile;
        }
        private void GuardarArchivo(byte[] arrayByte, string nombreArchivo)
        {
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            if (!Directory.Exists(rutaDestinoPDF))
            {
                Directory.CreateDirectory(rutaDestinoPDF);
            }
            string rutaDocumento = Path.Combine(rutaDestinoPDF, nombreArchivo);
            if (System.IO.File.Exists(rutaDocumento))
            {
                System.IO.File.Delete(rutaDocumento);
            }
            using (var fs = System.IO.File.Create(rutaDocumento))
            {
                fs.Write(arrayByte, 0, arrayByte.Length);
                fs.Dispose();
            }
        }
        [HttpPost]
        public async Task<JsonResult> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion)
        {
            var result = await oISolicitudServiceLogic_Gasto.ValidarNumeroComprobanteDuplicado(ruc, idtipodocumento, numerocomprobante, idrendicion);
            return Json(result);
        }


    }
}