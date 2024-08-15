using GASTO.Service;
using GASTO.Service.Contracts;
using GASTO.Service.Logic;
using GASTO.Service.Logic.Contract;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.ServiceDTO.Response;
using GASTO.Service.Services;
using Komatsu_SistemaSeguros.STS;
using Service.Contracts;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
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
    public class AprobacionJefesController : BaseController
    {
        private readonly IProveedorGastoWebApiService oIProveedorGastoWebApiService;
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        private readonly ISociedadServiceLogic oISociedadServiceLogic;
        private readonly IUsersService oIUsersService;
        private readonly IProveedorGastoWebApiServiceLogic oIProveedorGastoWebApiServiceLogic;
        private readonly ISolicitudServiceLogic_Gasto oISolicitudServiceLogic_Gasto;
        private readonly IHistorialLogService oIHistorialLogService;

        public AprobacionJefesController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService)
        {
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
            this.oISociedadServiceLogic = oISociedadServiceLogic;
            this.oIUsersService = oIUsersService;
            this.oIProveedorGastoWebApiService = new ProveedorGastoWebApiService();
            this.oIHistorialLogService = new HistorialLogService();
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


            var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
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

            //var lista = oICentroCostoServiceLogic.Get();
            //modelo.ListaCentroCosto = lista.Where(m => m.Soc == codigoSociedad).ToList();
            //modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            return View(modelo);
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
        public async Task<PartialViewResult> ConfirmarRevocar(int id)
        {
            ViewBag.ID = id;
            return PartialView("_ConfirmacionRevocar");
        }
        [HttpPost]
        public async Task<PartialViewResult> Confirmacion(int IdDestinoAprobador)
        {
            var modelo = new SolicitudViewModel();
            modelo.IdDestinoBag = IdDestinoAprobador;
            return PartialView("_Confirmacion", modelo);
        }
        [HttpPost]
        public async Task<PartialViewResult> Delegar(int IdUsuario)
        {
            IdUsuario = UtilSession.Usuario.Id;
            var modelo = new DelegacionesViewModel();
            modelo.IdUsuario = IdUsuario;
            modelo.Delegador = UtilSession.Usuario.NombreCompleto;
            return PartialView("_ListaDelegaciones", modelo);
        }

        public async Task<PartialViewResult> ConfirmacionRechazo(int IdSolicitud, string cod)
        {
            ViewBag.IdSolicitudRechazo = IdSolicitud;
            ViewBag.CodigoRechazo = cod;
            return PartialView("_ConfirmacionRechazo");
        }

        [HttpPost]
        public async Task<PartialViewResult> RegistroDetalleGastoSolicitud(int IdDetRendicionSolicitud = default(int), int IdMoneda = default(int), int idtipo = default(int), string idsociedad = default(string))
        {
            var modelo = new DetalleSolicitudViewModel();
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud;
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

        [HttpPost]
        public async Task<PartialViewResult> RegistrarDelegar(int id = default(int))
        {
            var modelo = new DelegacionesViewModel();
            BE_GASTO.Delegaciones entDelegacion = new BE_GASTO.Delegaciones();
            entDelegacion.ID = id;
            var listaDelegarA = oIUsersService.GetBySociedad(UtilSession.Usuario.IdSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaDelegarA.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
            ViewBag.ListaDelegarA = listaDelegarA;
            if (id != default(int))
            {
                entDelegacion = new BL_GASTO.Delegaciones.Delegaciones().RecuperarDetalle(entDelegacion);
                modelo.ID = id;
                modelo.IdUsuarioDelegado = entDelegacion.IdUsuarioDelegado;
                modelo.FechaDesdeStr = entDelegacion.FechaDesdeStr;
                modelo.FechaHastaStr = entDelegacion.FechaHastaStr;
                modelo.Comentarios = entDelegacion.Comentarios;
                modelo.Estado = entDelegacion.Estado;

            }
            else
            {
                modelo.ID = 0;
                modelo.IdUsuarioDelegado = 0;
                modelo.FechaDesdeStr = DateTime.Now.ToString("dd/MM/yyyy");
                modelo.FechaHastaStr = DateTime.Now.ToString("dd/MM/yyyy");
                modelo.Estado = 1;
            }
            return PartialView("_FormularioDelegar", modelo);
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

        public async Task<PartialViewResult> ConfirmacionEliminarDetalleGasto(int id)
        {
            ViewBag.hdfIdDetRendicionSolicitudEliminar = id;
            return PartialView("_ConfirmacionDeleteDetalleGasto");
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

        [HttpPost]
        public async Task<JsonResult> RegistrarDelegarPost(BE_GASTO.Delegaciones element)
        {
            var Usuario = UtilSession.Usuario;
            element.IdUsuario = Usuario.Id;
            element.UsuarioRegistro = Usuario.NombreCompleto;
            element.UserNameRegistro = Usuario.UserName;
            element.FechaDesde = element.FechaDesde;
            element.FechaHasta = element.FechaHasta;
            BE.OperationResult objResult = new BL_GASTO.Delegaciones.Delegaciones().Grabar(element);
            return Json(objResult);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarDelegarPost(BE_GASTO.Delegaciones element)
        {
            var Usuario = UtilSession.Usuario;
            element.IdUsuario = Usuario.Id;
            element.UsuarioRegistro = Usuario.NombreCompleto;
            element.UserNameRegistro = Usuario.UserName;
            element.FechaDesde = element.FechaDesde;
            element.FechaHasta = element.FechaHasta;
            BE.OperationResult objResult = new BL_GASTO.Delegaciones.Delegaciones().Actualizar(element);
            return Json(objResult);
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
        [HttpPost]
        public async Task<JsonResult> RevocarPost(int id)
        {

            BE_GASTO.Delegaciones entDelegaciones = new BE_GASTO.Delegaciones();
            entDelegaciones.ID = id;
            entDelegaciones.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entDelegaciones.UserNameRegistro = UtilSession.Usuario.UserName;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.Delegaciones.Delegaciones().Revocar(entDelegaciones);
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
        public JsonResult GetListDelegaciones(int? IdUsuario = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.Delegaciones entDelegacion = new BE_GASTO.Delegaciones();
            entDelegacion.PageSize = rows;
            entDelegacion.PageIndex = page;
            entDelegacion.IdUsuario = (int)IdUsuario;

            var lista = new BL_GASTO.Delegaciones.Delegaciones().Paginado(entDelegacion, out intTotalRows, out intCantidadRegistros);
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
        public async Task<PartialViewResult> GetHistorialByIdSolicitud(int IdSolicitud)
        {
            var modelo = new ViewModelHistorialLogs();
            modelo.ListaHistorialLogs = await oIHistorialLogService.GetHistorialByIdSolicitud(IdSolicitud);
            return PartialView("_ListHistorialEstadosSolicitud", modelo);
        }

        public async Task<PartialViewResult> SolicitudAprobacionExterior()
        {
            return PartialView("_BuscarSolicitudExteriorModal");
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
            modelo.NombreFile = entSolicitud.NombreFile;
            modelo.Codigo = entSolicitud.Codigo;
            ViewBag.IdTipo = entSolicitud.IdTipo;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            ViewBag.IdDestino = entSolicitud.IdDestino;

            return PartialView("_FormularioDetalle", modelo);

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

        private async Task<BE_GASTO.Solicitud> GetSolicitud(int IdSolicitud)
        {

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);
            return entSolicitud;
        }

        public List<Data.CentroCosto> GetCentrosCostos(string codigoSociedad)
        {
            var listaCentoCosto = oICentroCostoServiceLogic.Get().Where(m => m.Soc == codigoSociedad).OrderBy(x => x.Descripcion).Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            listaCentoCosto.Insert(0, new Data.CentroCosto() { Descripcion = "-SELECCIONE-" });
            return listaCentoCosto;
        }


        public ActionResult ListaCentrosCostos(string codsociedad)
        {
            var ListaCentrosCostos = GetCentrosCostos(codsociedad).ToList();
            ViewBag.ListaCentrosCostos = ListaCentrosCostos;
            return PartialView("_ListaCentroCosto");
        }
        [HttpPost]
        public async Task<JsonResult> AprobarPost(string lista, int? _iddestino = null)
        {


            //string appSetting1 = ConfigurationManager.AppSettings["correoEmisor"];
            //string appSetting2 = ConfigurationManager.AppSettings["contraseniaEmisor"];
            //BE.OperationResult operationResult = null;
            //try
            //{
            //    string[] strArray = lista.Split(',');
            //    BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            //    entSolicitud.IdDestino = _iddestino;
            //    entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            //    entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            //    for (int index = 0; index <= strArray.Length - 1; ++index)
            //    {
            //        entSolicitud.IdSolicitud = long.Parse(strArray[index]);
            //        BE_GASTO.Solicitud solicitud = new BL_GASTO.Solicitud.Solicitud().DatosEmail((int)entSolicitud.IdSolicitud);
            //        MimeMessage message = new MimeMessage();
            //        message.From.Add((InternetAddress)MailboxAddress.Parse(appSetting1));
            //        message.To.Add((InternetAddress)MailboxAddress.Parse(solicitud.CorreoA));
            //        message.Subject = "[Portal de Gastos] - Aprobación de " + solicitud.TipoC + " N° " + solicitud.Codigo + " - Colaborador " + solicitud.ColaboradorC;
            //        message.Body = (MimeEntity)new TextPart(TextFormat.Html)
            //        {
            //            Text = ("Estimado Aprobador, <br><br>Se informa sobre la " + solicitud.TipoC + " N° " + solicitud.Codigo + " del colaborador " + solicitud.ColaboradorC + " para su respectiva aprobacion. <br><br><br>Para acceder a la plataforma ingrese al link: <a href='https://ca.kmmp.com.pe/'>https://ca.kmmp.com.pe/</a><br><br>Saludos, <br>Cuentas por Pagar <br>Portal de Gastos")
            //        };
            //        SmtpClient smtpClient = new SmtpClient();
            //        using (smtpClient)
            //        {
            //            smtpClient.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls, new CancellationToken());
            //            smtpClient.Authenticate(appSetting1, appSetting2, new CancellationToken());
            //            smtpClient.Send(message, new CancellationToken(), (ITransferProgress)null);
            //            smtpClient.Disconnect(true, new CancellationToken());
            //        }
            //        operationResult = new BL_GASTO.Solicitud.Solicitud().Aprobar(entSolicitud);
            //    }
            //    return await Task.FromResult<JsonResult>(this.Json((object)operationResult));
            //}
            //catch (Exception ex)
            //{
            //    operationResult.Success = false;
            //    operationResult.Message = ex.Message;
            //    return await Task.FromResult<JsonResult>(this.Json((object)operationResult));
            //}

            string CorreoCopia = ConfigurationManager.AppSettings["correoCopia"].ToString();
            string[] CorreoCopia_ = CorreoCopia.Split(',');
            string CorreoCopiaKMA = ConfigurationManager.AppSettings["correoCopiaKMA"].ToString();
            string[] CorreoCopiaKMA_ = CorreoCopia.Split(',');

            string[] arrLista = lista.Split(',');
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            BE_GASTO.Solicitud entSolicitudExt = new BE_GASTO.Solicitud();

            //NT 26/12/2023
            entSolicitud.IdSolicitud = (long)Convert.ToInt32(arrLista[0]);
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);

            entSolicitud.IdDestino = _iddestino;
            entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;


            BE.OperationResult objResult = null;
            for (int i = 0; i <= arrLista.Length - 1; i++)
            {
                if (entSolicitud.IdDestinoPais == 1)
                {
                    entSolicitud.IdSolicitud = long.Parse(arrLista[i]);
                    objResult = new BL_GASTO.Solicitud.Solicitud().Aprobar(entSolicitud);
                }
                else
                {
                    entSolicitudExt = new BL_GASTO.Solicitud.Solicitud().ValidarAprobadorExterior(entSolicitud);
                    if (entSolicitudExt.IdUsuario != null)
                    {
                        entSolicitud.IdSolicitud = long.Parse(arrLista[i]);         
                        objResult = new BL_GASTO.Solicitud.Solicitud().Aprobar(entSolicitud);
                    }
                    else
                    {
                        objResult.Success = false;
                        objResult.Message = "No se encontro aprobador para el CECO : " + entSolicitud.CentroCostoAfectoCodigoSap;
                    }
                    
                }
                
            }

            entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);

            var correo = await oISolicitudServiceLogic_Gasto.GetSolicitudCorreo((int)entSolicitud.IdSolicitud);
            correo.Responsable = new BE_GASTO.Usuario();
            correo.Responsable.Email = Util.UtilSession.Usuario.Email;
            correo.IdTipoCorreo = (int)entSolicitud.IdTipo;
            correo.ListaSoporteAdministrativo = correo.ListaSoporteAdministrativo ?? new List<BE_GASTO.Usuario>();
            correo.ListaBeneficiado = await oISolicitudServiceLogic_Gasto.GetListBeneficiadoBySolicitud(Convert.ToInt32(entSolicitud.IdSolicitud));

            if (entSolicitud.IdSociedad == "PE03")
            {
                foreach (var correos in CorreoCopiaKMA_)
                {
                    correo.ListaSoporteAdministrativo.Add(new BE_GASTO.Usuario { Email = correos });
                }
            }
            else
            {
                foreach (var correos in CorreoCopia_)
                {
                    correo.ListaSoporteAdministrativo.Add(new BE_GASTO.Usuario { Email = correos });
                }
            }

            
            
            var result = await oIProveedorGastoWebApiService.EmailService(correo);

            return Json(objResult);

        }

        public PartialViewResult BuscarSolicitudExterior()
        {
            return PartialView("_BuscarSolicitudExteriorModal");
        }
        [HttpGet]
        public JsonResult GetListSolicitudAprobacion(Int64? idusuario = null, int? idsituacionservicio = null, string fechaDesde = "", string fechaHasta = "", string idcentrocosto = null, string idsociedad=null, int? idsucursal=null, string anioejercicio = null, int? idtipo = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intIdDestino = 0;
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

            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.CentroCostoAfectoCodigoSap = idcentrocosto == null ? "" : idcentrocosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;

            var lista = new BL_GASTO.Solicitud.Solicitud().PaginadoAprobacion(entSolicitud, UsuarioSociedad, out intTotalRows, out intCantidadRegistros,out intIdDestino);
            int iddestinobag = 1;
            if (lista.Count>0)
            {
                iddestinobag = lista.FirstOrDefault().IdDestinoBag;
            }
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,
                iddestino = iddestinobag
            };
            ViewBag.IdDestinoAprobar = iddestinobag;
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
                { "pFechaContabilidad", modelo.FechaContabilidadStr},
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