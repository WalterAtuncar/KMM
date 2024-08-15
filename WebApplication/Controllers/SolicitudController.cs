using Data.Enum;
using Data.Util;
using Komatsu_SistemaSeguros.STS;
using Service;
using Service.Contracts;
using Service.General;
using Service.Logic.Contract;
using Service.ServiceDTO;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BL = Komatsu.SistemaTaxis.BusinessLogic;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class SolicitudController : Controller
    {
        private readonly IProveedorWebApiServiceLogic oIProveedorWebApiServiceLogic;
        private readonly ISolicitudServiceLogic oISolicitudServiceLogic;
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        private readonly ISociedadServiceLogic oISociedadServiceLogic;
        private readonly IUsersService oIUsersService;
        private readonly IDestinoServiceLogic oIDestinoServiceLogic;
        private readonly IBeneficiadoServiceLogic oIBeneficiadoServiceLogic;
        private readonly IProveedorServiceLogic oIProveedorServiceLogic;

        private readonly ITipoServicioService oITipoServicioService;
        private readonly ITipoDestinoService oITipoDestinoService;
        private readonly ICentroCostoService oICentroCostoService;
        private readonly IMotivoCreacionSolicitudService oIMotivoCreacionSolicitudService;
        private readonly ISolicitudService oISolicitudService;
        private readonly IEmailService oIEmailService;
        private readonly IProveedorWebApiService oIProveedorWebApiService;
       

        public SolicitudController(IProveedorServiceLogic oIProveedorServiceLogic, IProveedorWebApiServiceLogic oIProveedorWebApiServiceLogic, ISolicitudServiceLogic oISolicitudServiceLogic, ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService, IDestinoServiceLogic oIDestinoServiceLogic, IBeneficiadoServiceLogic oIBeneficiadoServiceLogic,
            ITipoServicioService oITipoServicioService, ITipoDestinoService oITipoDestinoService, ICentroCostoService oICentroCostoService, IMotivoCreacionSolicitudService oIMotivoCreacionSolicitudService, ISolicitudService oISolicitudService, IEmailService oIEmailService, IProveedorWebApiService oIProveedorWebApiService)
        {
            this.oIProveedorServiceLogic = oIProveedorServiceLogic;
            this.oIProveedorWebApiServiceLogic = oIProveedorWebApiServiceLogic;
            this.oISolicitudServiceLogic = oISolicitudServiceLogic;
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
            this.oISociedadServiceLogic = oISociedadServiceLogic;
            this.oIUsersService = oIUsersService;
            this.oIDestinoServiceLogic = oIDestinoServiceLogic;
            this.oIBeneficiadoServiceLogic = oIBeneficiadoServiceLogic;

            this.oITipoServicioService = oITipoServicioService;
            this.oITipoDestinoService = oITipoDestinoService;
            this.oICentroCostoService = oICentroCostoService;
            this.oIMotivoCreacionSolicitudService = oIMotivoCreacionSolicitudService;
            this.oISolicitudService = oISolicitudService;
            this.oIEmailService = oIEmailService;
            this.oIProveedorWebApiService = oIProveedorWebApiService;
        }
        // GET: Solicitud
        [STSUrl]
        public ActionResult Index(bool upd = false)
        {
            var modelo = new SolicitudViewModel();
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            modelo.ListaSociedad = oISociedadServiceLogic.Get().ToList();
            var lista =  oICentroCostoServiceLogic.Get();
            modelo.ListaCentroCosto = lista.Where(m => m.Soc == codigoSociedad).ToList();
            modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            if (upd)
            {
                ViewBag.Updated = string.Empty;
            }

            ViewBag.Message = "Se registro correctamente.";
            return View(modelo);

        }
        [STSUrl]
        public ActionResult IndexUsuario(bool upd = false)
        {
            var modelo = new SolicitudViewModel();
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            modelo.ListaSociedad = oISociedadServiceLogic.Get().ToList();
            var lista = oICentroCostoServiceLogic.Get();
            modelo.ListaCentroCosto = lista.Where(m => m.Soc == codigoSociedad).ToList();
            modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m;}).ToList();
            if (upd)
            {
                ViewBag.Updated = string.Empty;
            }
            ViewBag.Message = "Se registro correctamente.";
            return View(modelo);
        }

        public async Task<ActionResult> Ubicacion(int? idSolicitud)
        {
            try
            {
                var modelo = new SolicitudViewModel();
                modelo.Solicitud = await oISolicitudServiceLogic.GetById(idSolicitud);
                ViewBag.Key = await oIProveedorServiceLogic.Key() ?? string.Empty;
                ViewBag.Icono = ConfigurationManager.AppSettings["urlMarker"].ToString();
                ViewBag.Intervalo = ConfigurationManager.AppSettings["setInterval"].ToString();
                return PartialView("_Ubicacion", modelo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ActionResult> Tracking(int idSolicitud)
        {
            var modelo = new SolicitudViewModel();
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(idSolicitud);
            ViewBag.Key = await oIProveedorServiceLogic.Key() ?? string.Empty;
            return PartialView("_Tracking", modelo);
        }

        public ActionResult View(int id)
        {
            var model = new SolicitudService().Find(id);
            return View(model);
        }

        public async Task<JsonResult> GetByID(int id)
        {
            var model = await new SolicitudService().Find(id);
            return Json(model);
        }

        public string ToImage(byte[] byteData)
        {
            //Convert byte arry to base64string   
            string imreBase64Data = Convert.ToBase64String(byteData);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            //Passing image data in viewbag to view  
            return imgDataURL;
        }

        public async Task<ActionResult> VisualizarDetalle(int id)
        {
            var modelo = new SolicitudViewModel();
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(id);
            if (modelo.Solicitud.ProveedorTaxi == null)
            {
                modelo.Solicitud.ProveedorTaxi = new Data.Common.ProveedorTaxi();
                modelo.Solicitud.ProveedorTaxi.RazonSocial = "No asignado";
            }
            if (modelo.Solicitud.Conductor.NombreCompleto == null)
            {
                modelo.Solicitud.Conductor.NombreCompleto = "No asignado";
            }
            if (modelo.Solicitud.Conductor.Documento == null)
            {
                modelo.Solicitud.Conductor.Documento = "No asignado";
            }
            if(modelo.Solicitud.Conductor.Foto == null)
            {
                string absolutePath = AppCode.Util.ServerPath("~/Content/img");

                string rutaCompleta = Path.Combine(absolutePath, "silueta.jpg");

                var file = System.IO.File.ReadAllBytes(rutaCompleta);

                modelo.Solicitud.Conductor.Foto = file;
            }
            if (modelo.Solicitud.Automovil.Placa == null)
            {
                modelo.Solicitud.Automovil.Placa = "No asignado";
            }
            if (modelo.Solicitud.Automovil.Color == null)
            {
                modelo.Solicitud.Automovil.Color = "No asignado";
            }
            if (modelo.Solicitud.Automovil.Modelo == null)
            {
                modelo.Solicitud.Automovil.Modelo = "No asignado";
            }
            if (modelo.Solicitud.Automovil.Foto == null)
            {
                modelo.Solicitud.Automovil.Foto = new byte[0];
            }
            return PartialView("_Detalle", modelo);
        }

        public async Task<ActionResult> Calificacion(int id)
        {
            var modelo = new SolicitudViewModel();
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(id);
            return PartialView("_SolicitudCalificacion", modelo);
        }

        public async Task<ActionResult> CancelarServicio(int idSolicitud)
        {
            var modelo = new SolicitudViewModel();
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(idSolicitud);
            if(modelo.Solicitud.ProveedorTaxi == null)
            {
                modelo.Solicitud.ProveedorTaxi = new Data.Common.ProveedorTaxi();
                modelo.Solicitud.ProveedorTaxi.RazonSocial = "No asignado";
            }
            if(modelo.Solicitud.Conductor.NombreCompleto == null)
            {
                modelo.Solicitud.Conductor.NombreCompleto =  "No asignado";
            }
            modelo.Solicitud.TiempoEspera = Convert.ToDouble(ConfigurationManager.AppSettings["timeMinutes"]);
            modelo.Solicitud.DiferenciaEnMinutos = ObtenerDiferenciaMinutos(modelo.Solicitud.FechaServicio);
            return PartialView("_CancelarServicio", modelo);
        }

        public double ObtenerDiferenciaMinutos(DateTime FechaServicio)
        {
            var fechaServicio = Convert.ToDateTime(FechaServicio);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var FechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            TimeSpan difference = fechaServicio - FechaServidor;
            int hours = (int)difference.TotalHours;
            var minutes = difference.TotalMinutes;
            return minutes;
        }

        public async Task<ActionResult> Registrar()
        {
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
            var listaAprobadores = this.GetUsuariosAprobadores(string.Empty);

            ViewBag.Key = await oIProveedorServiceLogic.Key() ?? string.Empty;
            ViewBag.ListaBeneficiado = listaBeneficado;
            ViewBag.ListaAprobadores = listaAprobadores;

            var minutos = ConfigurationManager.AppSettings["startMinutes"];
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var FechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            DateTime add = FechaServidor.AddMinutes(Double.Parse(minutos));
            ViewBag.Hour = add.Hour + ":" + add.Minute;
            return View();
        }

        public List<BE.UsuarioCentroCosto> GetUsuariosAprobadores(string ceco)
        {
            var listaAprobadores = new BL.Solicitud().GetUsuarioCentroCostoByCeco(ceco).ToList();
            listaAprobadores.Insert(0, new BE.UsuarioCentroCosto() { NombresApellido = "-SELECCIONE-" });

            return listaAprobadores;
        }
        public ActionResult ListaAprobadores_PV(string ceco)
        {
            var listaAprobadores = GetUsuariosAprobadores(ceco);
            ViewBag.ListaAprobadores = listaAprobadores;
            return PartialView("_ListaUsuarioCentroCosto");
        }

        public async Task<ActionResult> Editar(int id)
        {
            var modelo = new SolicitudViewModel();
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(id);
            modelo.ListaTipoDestino = oITipoDestinoService.List();
            modelo.ListaTipoServicio = oITipoServicioService.List();
            modelo.ListaCentroCosto = await oICentroCostoService.List();
            modelo.ListaUsuarios = oIUsersService.GetList();
            modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
            modelo.ListaMotivoCreacionSolicitud = oIMotivoCreacionSolicitudService.List();
            ViewBag.Key = await oIProveedorServiceLogic.Key() ?? string.Empty;

            var listaAprobadores = GetUsuariosAprobadores(modelo.Solicitud.CentroCostoAfectoCodigoSap);
            ViewBag.ListaAprobadores = listaAprobadores;

            var dataSolicitud = new BL.Solicitud().GetSolicitudById(id);
            ViewBag.IdUsuarioAprobador = dataSolicitud["IdUsuarioAprobador"];

            return View(modelo);
        }

        [HttpPost]
        public JsonResult EditarPost(Data.Solicitud element, List<Service.General.Destino> listaDestino, List<TarifaResponse> listaTarifa, int idAprobador)
        {
            var minutos = ConfigurationManager.AppSettings["startMinutes"];
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var fechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            DateTime fechaEstablecida = fechaServidor.AddMinutes(Double.Parse(minutos));
            fechaEstablecida = new DateTime(fechaEstablecida.Year, fechaEstablecida.Month, fechaEstablecida.Day, fechaEstablecida.Hour, fechaEstablecida.Minute, 0);

            Int64 intFechaServicio = Convert.ToInt64(Convert.ToDateTime(element.FechaServicio).ToString("yyyyMMddHHmm"));
            Int64 intFechaEstablecida = Convert.ToInt64(fechaEstablecida.ToString("yyyyMMddHHmm"));

            if (intFechaServicio < intFechaEstablecida)
            {
                return Json(new BE.OperationResult() { Success = false, Message = "La hora no debe ser menor a la establecida" });
            }

            element.Sociedad = UtilSession.Usuario.IdSociedad;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            //element.UsuarioModifico = UtilSession.Usuario.NombreCompleto;
            var usuarioNombreCompleto = UtilSession.Usuario.NombreCompleto;

            int intContador = 0;
            int intIdProveedor = 0;
            BE.Solicitud entSolicitud = new BE.Solicitud();
            List<BE.Destino> lisDestino = new List<BE.Destino>();
            List<BE.SolicitudProveedorTaxi> lisSolicitudProveedorTaxi = new List<BE.SolicitudProveedorTaxi>();

            entSolicitud.ID = element.ID;
            entSolicitud.TipoServicio = (element.TipoServicio == "0" ? null : element.TipoServicio);
            entSolicitud.TipoDestino = element.TipoDestino;
            entSolicitud.DistanciaKilometro = Convert.ToDouble(element.DistanciaKilometro);
            entSolicitud.FechaServicio = Convert.ToDateTime(element.FechaServicio);
            entSolicitud.Sociedad = element.Sociedad;
            entSolicitud.Observaciones = element.Observaciones;
            entSolicitud.CantidadHoras = element.CantidadHoras;
            entSolicitud.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
            //entSolicitud.OrdenServicio = element.NroOrdenServicio == null ? string.Empty : element.NroOrdenServicio;
            entSolicitud.TotalServicio = Convert.ToDouble(element.TotalServicio == null ? default(decimal) : element.TotalServicio);
            entSolicitud.IdTipoServicio = element.IdTipoServicio == 0 ? null : element.IdTipoServicio;
            entSolicitud.IdTipoDestino = element.IdTipoDestino == 0 ? null : element.IdTipoDestino;
            entSolicitud.IdSociedad = element.IdSociedad;
            entSolicitud.IdMotivoCreacionSolicitud = element.IdMotivoCreacionSolicitud == 0 ? null : element.IdMotivoCreacionSolicitud;
            entSolicitud.SituacionServicio = 1;
            entSolicitud.IdUsuarioAprobador = idAprobador;
            entSolicitud.UsuarioModifico = UtilSession.Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;

            intContador = 1;
            intIdProveedor = listaDestino.OrderBy(m => m.IdProveedor).ToList().FirstOrDefault().IdProveedor;
            foreach (Service.General.Destino itemDestino in listaDestino.OrderBy(m => m.IdProveedor).ToList())
            {
                BE.Destino entDestino = new BE.Destino();
                entDestino.DireccionOrigen = itemDestino.DireccionOrigen;
                entDestino.DireccionDestino = itemDestino.DireccionDestino;
                entDestino.NumeroDireccionDestino = itemDestino.NumeroDireccionDestino;
                entDestino.NumeroDireccionOrigen = itemDestino.NumeroDireccionOrigen;
                entDestino.LatitudOrigen = itemDestino.LatitudOrigen;
                entDestino.LatitudDestino = itemDestino.LatitudDestino;
                entDestino.LongitudOrigen = itemDestino.LongitudOrigen;
                entDestino.LongitudDestino = itemDestino.LongitudDestino;
                entDestino.ZonaOrigen = itemDestino.ZonaOrigen;
                entDestino.ZonaDestino = itemDestino.ZonaDestino;
                entDestino.DistanciaDestinoKilometro = Convert.ToDouble(itemDestino.DistanciaDestinoKilometro);
                entDestino.Precio = Convert.ToDouble(itemDestino.Precio);
                entDestino.IdProveedor = itemDestino.IdProveedor;
                entDestino.Negociado = itemDestino.Negociado;
                if (intIdProveedor == itemDestino.IdProveedor)
                {
                    entDestino.Orden = intContador++;
                }
                else
                {
                    intContador = 1;
                    intIdProveedor = itemDestino.IdProveedor;
                    entDestino.Orden = intContador++;
                }
                lisDestino.Add(entDestino);
            }

            foreach (Data.SolicitudProveedorTaxi itemProveedorTaxi in element.SolicitudProveedorTaxi)
            {
                BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi = new BE.SolicitudProveedorTaxi();
                entSolicitudProveedorTaxi.ProveedorTaxiID = itemProveedorTaxi.ProveedorTaxiID;
                entSolicitudProveedorTaxi.PrecioServicio = Convert.ToDouble(itemProveedorTaxi.PrecioServicio);

                foreach (var tarifa in listaTarifa)
                {
                    if (tarifa.FlagMenor == true)
                    {
                        intIdProveedor = tarifa.IdProveedor;
                    }
                }

                if (entSolicitudProveedorTaxi.ProveedorTaxiID == intIdProveedor)
                {
                    entSolicitudProveedorTaxi.Seleccionado = "1";
                }
                else
                {
                    entSolicitudProveedorTaxi.Seleccionado = "0";
                }
                lisSolicitudProveedorTaxi.Add(entSolicitudProveedorTaxi);
            }

            entSolicitud.SolicitudDetalle = new BE.SolicitudDetalle();
            entSolicitud.DestinoList = lisDestino;
            entSolicitud.SolicitudProveedorTaxiList = lisSolicitudProveedorTaxi;

            BE.Solicitud lisSolicitud = new BL.Solicitud().Recuperar(entSolicitud);
            if (lisSolicitud.IdSituacionServicio != 1 && lisSolicitud.SituacionServicio !=1)
            {
                return Json(new BE.OperationResult() { Success = false, Message = "La solicitud no puede ser editada." });
            }
            else
            {
                entSolicitud.SituacionServicio = 1;
                entSolicitud.IdSituacionServicio = 1;
                entSolicitud.IdEstadoServicioProveedor = 1;
            }

            BE.OperationResult objResult = new BL.Solicitud().Actualizar(entSolicitud);

            return Json(objResult);
        }

        public JsonResult GetListBeneficiado()
        {
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
            var result = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_InputListBeneficiado", listaBeneficado);
            return Json(result);
        }

        public JsonResult EliminarBeneficiado(int id, int idSolicitud)
        {
            var result = oIBeneficiadoServiceLogic.Delete(id, idSolicitud);
            return Json(result);
        }

        public JsonResult AgregarBeneficiado(Data.Common.Beneficiado beneficiado)
        {
            var result = oIBeneficiadoServiceLogic.Add(beneficiado);
            return Json(result);
        }

        [STSUrl]
        public async Task<ActionResult> Evaluar(int id)
        {
            var modelo = new SolicitudViewModel();
            var listaBeneficado = oIUsersService.Get().Where(m => m.IdEstado == "AC").OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Usuario() { Nombres = "-SELECCIONE-" });
            modelo.ListaUser = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_ListBeneficiado", listaBeneficado);
            modelo.Solicitud = await oISolicitudServiceLogic.GetById(id).ConfigureAwait(false);
            modelo.Solicitud.CentroCosto.TextoBreve = $"{modelo.Solicitud.CentroCosto.Codigo} - {modelo.Solicitud.CentroCosto.Descripcion}";
            modelo.ListaSolicitudProveedorTaxi = modelo.Solicitud.ListaSolicitudProveedorTaxi;
            return View(modelo);
        }

        public async Task<ActionResult> VisualizarRecorrido(int id)
        {
            var modelo = new SolicitudViewModel();
            modelo.ListaRecorrido = await oIDestinoServiceLogic.GetDestinoBySolicitud(id);
            ViewBag.Key = await oIProveedorServiceLogic.Key();
            return PartialView("_Recorrido", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarPost(Data.Solicitud element, List<Service.General.Destino> listaDestino, List<TarifaResponse> listaTarifa, int idAprobador)
        {
            var minutos = ConfigurationManager.AppSettings["startMinutes"];
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var fechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            DateTime fechaEstablecida = fechaServidor.AddMinutes(Double.Parse(minutos));
            fechaEstablecida = new DateTime(fechaEstablecida.Year, fechaEstablecida.Month, fechaEstablecida.Day, fechaEstablecida.Hour, fechaEstablecida.Minute, 0);

            Int64 intFechaServicio = Convert.ToInt64(Convert.ToDateTime(element.FechaServicio).ToString("yyyyMMddHHmm"));
            Int64 intFechaServicio_ = Convert.ToInt64(Convert.ToDateTime(element.FechaServicio).ToString("yyyyMMdd"));
            Int64 intFechaEstablecida = Convert.ToInt64(fechaEstablecida.ToString("yyyyMMddHHmm"));

            var fechaServidorHoy = Convert.ToInt64(Convert.ToDateTime(fechaServidor).ToString("yyyyMMdd"));

            if (fechaServidorHoy == intFechaServicio_)
            {
                if (intFechaServicio < intFechaEstablecida)
                {
                    return Json(new OperationResult() { Success = false, Message = "La hora no debe ser menor a la establecida" });
                }
            }

            //if (intFechaServicio < intFechaEstablecida)
            //{
            //    return Json(new OperationResult() { Success = false, Message = "La hora no debe ser menor a la establecida" });
            //}

            element.Sociedad = UtilSession.Usuario.IdSociedad;
            var Usuario = UtilSession.Usuario;
            element.UserNameRegistro = Usuario.UserName;

            int intContador = 0;
            int intIdProveedor = 0;
            BE.Solicitud entSolicitud = new BE.Solicitud();
            List<BE.Beneficiado> lisBeneficiado = new List<BE.Beneficiado>();
            List<BE.Destino> lisDestino = new List<BE.Destino>();
            List<BE.SolicitudProveedorTaxi> lisSolicitudProveedorTaxi = new List<BE.SolicitudProveedorTaxi>();
            
            entSolicitud.TipoServicio = (element.TipoServicio == "0" ? null: element.TipoServicio);
            entSolicitud.TipoDestino = element.TipoDestino;
            entSolicitud.DistanciaKilometro = Convert.ToDouble(element.DistanciaKilometro);
            entSolicitud.FechaServicio = Convert.ToDateTime(element.FechaServicio);
            entSolicitud.Sociedad = element.Sociedad;
            entSolicitud.Observaciones = element.Observaciones;
            entSolicitud.CantidadHoras = element.CantidadHoras;
            entSolicitud.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
            entSolicitud.OrdenServicio = element.NroOrdenServicio == null ? string.Empty : element.NroOrdenServicio;
            entSolicitud.TotalServicio = Convert.ToDouble(element.TotalServicio == null ? default(decimal) : element.TotalServicio);
            entSolicitud.IdTipoServicio = element.IdTipoServicio == 0 ? null : element.IdTipoServicio;
            entSolicitud.IdTipoDestino = element.IdTipoDestino == 0 ? null : element.IdTipoDestino;
            entSolicitud.IdSociedad = element.IdSociedad == null ? default(int) : element.IdSociedad;
            entSolicitud.IdMotivoCreacionSolicitud = element.IdMotivoCreacionSolicitud == 0 ? null : element.IdMotivoCreacionSolicitud;
            entSolicitud.SituacionServicio = 1;
            entSolicitud.IdUsuarioAprobador = idAprobador;
            entSolicitud.UsuarioRegistro = Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = Usuario.UserName;
            
            foreach (Data.Beneficiado itemBeneficiado in element.Beneficiado)
            {
                BE.Beneficiado entBeneficiado = new BE.Beneficiado();
                entBeneficiado.UsersID = itemBeneficiado.UsersID;
                entBeneficiado.FlagPrincipal = intContador == 0 ? true : false;
                entBeneficiado.Telefono = itemBeneficiado.Telefono;
                if (entBeneficiado.FlagPrincipal == true)
                {
                    entSolicitud.TelefonoPrincipal = entBeneficiado.Telefono;
                }
                intContador += 1;
                lisBeneficiado.Add(entBeneficiado);
            }
            intContador = 1;
            intIdProveedor = listaDestino.OrderBy(m => m.IdProveedor).ToList().FirstOrDefault().IdProveedor;
            foreach (Service.General.Destino itemDestino in listaDestino.OrderBy(m => m.IdProveedor).ToList())
            {
                BE.Destino entDestino = new BE.Destino();
                entDestino.DireccionOrigen = itemDestino.DireccionOrigen;
                entDestino.DireccionDestino = itemDestino.DireccionDestino;
                entDestino.NumeroDireccionDestino = itemDestino.NumeroDireccionDestino;
                entDestino.NumeroDireccionOrigen = itemDestino.NumeroDireccionOrigen;
                entDestino.LatitudOrigen = itemDestino.LatitudOrigen;
                entDestino.LatitudDestino = itemDestino.LatitudDestino;
                entDestino.LongitudOrigen = itemDestino.LongitudOrigen;
                entDestino.LongitudDestino = itemDestino.LongitudDestino;
                entDestino.ZonaOrigen = itemDestino.ZonaOrigen;
                entDestino.ZonaDestino = itemDestino.ZonaDestino;
                entDestino.DistanciaDestinoKilometro = Convert.ToDouble(itemDestino.DistanciaDestinoKilometro);
                entDestino.Precio = Convert.ToDouble(itemDestino.Precio);
                entDestino.IdProveedor = itemDestino.IdProveedor;
                entDestino.Negociado = itemDestino.Negociado;
                if (intIdProveedor == itemDestino.IdProveedor)
                {
                    entDestino.Orden = intContador++;
                }
                else
                {
                    intContador = 1;
                    intIdProveedor = itemDestino.IdProveedor;
                    entDestino.Orden = intContador++;
                }
                lisDestino.Add(entDestino);
            }

            foreach (Data.SolicitudProveedorTaxi itemProveedorTaxi in element.SolicitudProveedorTaxi)
            {
                BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi = new BE.SolicitudProveedorTaxi();
                entSolicitudProveedorTaxi.ProveedorTaxiID = itemProveedorTaxi.ProveedorTaxiID;
                entSolicitudProveedorTaxi.PrecioServicio = Convert.ToDouble(itemProveedorTaxi.PrecioServicio);

                foreach (var tarifa in listaTarifa)
                {
                    if (tarifa.FlagMenor == true)
                    {
                        intIdProveedor = tarifa.IdProveedor;
                    }
                }

                if (entSolicitudProveedorTaxi.ProveedorTaxiID == intIdProveedor)
                {
                    entSolicitudProveedorTaxi.Seleccionado = "1";
                }
                else
                {
                    entSolicitudProveedorTaxi.Seleccionado = "0";
                }
                lisSolicitudProveedorTaxi.Add(entSolicitudProveedorTaxi);
            }

            entSolicitud.SolicitudDetalle = new BE.SolicitudDetalle();
            entSolicitud.BeneficiadoList = lisBeneficiado;
            entSolicitud.DestinoList = lisDestino;
            entSolicitud.SolicitudProveedorTaxiList = lisSolicitudProveedorTaxi;

            BE.OperationResult objResult = new BL.Solicitud().Grabar(entSolicitud);
           
            if (objResult.Success)
            {
                var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                correo.Responsable = new Data.Common.Usuario();
                correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                var result = await oIProveedorWebApiService.EmailService(correo);

            }
            return Json(objResult);
        }

        [HttpPost]
        public async Task<ActionResult> AprobarPost(int id, int idProveedor)
        {
            var modelo = await oISolicitudServiceLogic.GetServicioRequestBySolicitud(id, idProveedor);
            modelo.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            modelo.UserNameAprobo = UtilSession.Usuario.UserName;

            if (modelo.IdEstado == (int)SolicitudEnum.Aprobado)
            {
                return Json(new OperationResult() { Success = false, Message = "La solicitud ya fue aprobada." });
            }

            var task = await oIProveedorWebApiServiceLogic.SaveService(modelo);
            if (task.Success)
            {
                var correo = await oISolicitudService.GetSolicitudCorreo(id);
                correo.IdTipoCorreo = (int)TipoCorreoEnum.Aprobado;
                correo.Mensaje = ConfigurationManager.AppSettings["mensajeAprobacion"].Replace("{0}", correo.Codigo);
                correo.Responsable = new Data.Common.Usuario();

                var proveedorAprobado = await oIProveedorServiceLogic.GetProveedorById(idProveedor);
                correo.RazonSocialProveedor = proveedorAprobado.RazonSocial;
                correo.RucProveedor = proveedorAprobado.RUC;

                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Aprobado);
                correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                var result = await oIProveedorWebApiService.EmailService(correo);
            }
            return Json(task);
        }

        [HttpPost]
        public async Task<JsonResult> RechazarPost(int id, string m)
        {
            BE.Solicitud entSolicitud = new BE.Solicitud();
            entSolicitud.ID = id;
            entSolicitud.SituacionServicio = 4;
            entSolicitud.IdSituacionServicio = 4;
            entSolicitud.MotivoRechazo = m;
            entSolicitud.FechaCancelacion = DateTime.Now;
            entSolicitud.SolicitudDetalle = new BE.SolicitudDetalle();
            entSolicitud.SolicitudDetalle.IdSituacion = 4;
            entSolicitud.SolicitudDetalle.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.SolicitudDetalle.IdSolicitud = id;
            BE.OperationResult objResult = new BL.Solicitud().Rechazar(entSolicitud);
            if (objResult.Success)
            {
                try
                {
                    var correo = await oISolicitudService.GetSolicitudCorreo(id);
                    correo.IdTipoCorreo = (int)TipoCorreoEnum.Rechazado;
                    correo.Mensaje = ConfigurationManager.AppSettings["mensajeRechazo"].Replace("{0}", correo.Codigo);
                    correo.Responsable = new Data.Common.Usuario();
                    correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Rechazado);
                    correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                    var sendMail = await oIProveedorWebApiService.EmailService(correo);
                    return Json(sendMail);
                }
                catch (Exception ex)
                {
                    return Json(ex);
                }
            }
            return Json(objResult);
        }

        [HttpPost]
        public async Task<JsonResult> CancelarPost(int id, string m)
        {
            BE.Solicitud entSolicitud = new BE.Solicitud();
            entSolicitud.ID = id;
            entSolicitud.SituacionServicio = 3;
            entSolicitud.MotivoCancelacion = m;
            entSolicitud.FechaCancelacion = DateTime.Now;

            entSolicitud.SolicitudDetalle = new BE.SolicitudDetalle();
            entSolicitud.SolicitudDetalle.IdSituacion = 3;
            entSolicitud.SolicitudDetalle.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.SolicitudDetalle.IdSolicitud = id;
            entSolicitud.IdSituacionServicio = 3;
            entSolicitud.IdEstadoServicioProveedor = 8;
            BE.OperationResult objResult = new BL.Solicitud().Cancelar(entSolicitud);
            if (objResult.Success)
            {
                try
                {
                    var correo = await oISolicitudService.GetSolicitudCorreo(id);
                    correo.IdTipoCorreo = (int)TipoCorreoEnum.Anulado;
                    correo.Mensaje = ConfigurationManager.AppSettings["mensajeCancelacion"].Replace("{0}", correo.Codigo);
                    correo.Responsable = new Data.Common.Usuario();
                    //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                    correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Anulado);
                    correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                    var sendMail = await oIProveedorWebApiService.EmailService(correo);
                    return Json(sendMail.ResponseObject);
                } catch(Exception ex)
                {
                    return Json(new OperationResult() { Message = ex.Message });
                }
            }
            return Json(objResult);
        }

        [HttpPost]
        public async Task<JsonResult> TarifaPost(int TipoServicioID, List<Service.General.Destino> ListaDestino)
        {

            var operation = new OperationResult();
            var result = await oIProveedorWebApiServiceLogic.GetListTarifa(new Data.ServiceDTO.TarifaRequest { IdTipoPago = 1, IdTipoServicio = TipoServicioID, ListaDestino = ListaDestino });

            if (result.Count == default(int))
            {
                operation.Success = false;
                operation.Message = "No hay precio";
            }
            else
            {
                operation.Success = true;
                operation.ObjectResult = result.Where(x => x.FlagMenor == true).FirstOrDefault();
            }

            return Json(new { operation, result });
        }

        [HttpPost]
        public JsonResult UpdateCentroCosto(ActualizarCentroCostoSolicitudModel Model)
        {
            var result = oISolicitudServiceLogic.UpdateCentroCosto(Model.ID, Model.CentroCostoAfectoDescripcion);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ValidarOrdenServicio(string ordenServicio)
        {
            var sociedad = UtilSession.Usuario.IdSociedad;
            var result = await oIProveedorWebApiServiceLogic.GetOrdenServicio(ordenServicio, sociedad);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ValidarOrdenServicioLiquidacion(string ordenServicio)
        {
            var sociedad = UtilSession.Usuario.IdSociedad;
            var result = await oIProveedorWebApiServiceLogic.GetOrdenServicioLiquidacion(ordenServicio, sociedad);
            return Json(result);
        }


        [HttpPost]
        public async Task<JsonResult> ValidarCentroCosto(string codigoCentroCosto)
        {
            var result = await  oICentroCostoServiceLogic.ValidarCentroCosto(codigoCentroCosto);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ValidarCalificacion()
        {
            var result = await oISolicitudServiceLogic.GetSolicitudByUsernameRegistro(UtilSession.Usuario.UserName);
            if(result == null)
            {
                result = new Data.Common.Solicitud();
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult ModificarCalificacion(int idSolicitud, int calificacion, string comentario)
        {
            var result = oISolicitudServiceLogic.UpdateCalificacion(idSolicitud, calificacion, comentario);
            return Json(result);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetSolicitudDetalle(int idSolicitud)
        {
            var modelo = new SolicitudViewModel();
            modelo.ListaSolicitudDetalle = await oISolicitudServiceLogic.GetListSolicitudDetalleBySolicitud(idSolicitud);
            return PartialView("_ListSolicitudDetalle", modelo);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetGastoAdicional(int idSolicitud)
        {
            var modelo = new SolicitudViewModel();
            modelo.ListaGastoAdicional = await oISolicitudServiceLogic.GetListGastoAdicionalBySolicitud(idSolicitud);
            return PartialView("_ListGastoAdicional", modelo);
        }

        [HttpGet]
        public JsonResult GetListSolicitud(int? sociedad = null, string fechaDesde = "", string fechaHasta = "", int? situacion = null, string nroSolicitud = null, string centroCosto = null, int page = 0, int rows = 0)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var idPerfil = UtilSession.Usuario.IdPerfil;
            var userNameRegistro = UtilSession.Usuario.UserName;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Sociedad", sociedad == null ? 0 : sociedad.Value);
            parameter.Collection.Add("IdPerfil", idPerfil);
            parameter.Collection.Add("UserNameRegistro", userNameRegistro);
            parameter.Collection.Add("FechaDesde", fechaDesde);
            parameter.Collection.Add("FechaHasta", fechaHasta);
            parameter.Collection.Add("IdSituacionServicio", situacion == null ? 0 : situacion.Value);
            parameter.Collection.Add("NroSolicitud", nroSolicitud);
            parameter.Collection.Add("CentroCosto", centroCosto);

            var listQuery = oISolicitudServiceLogic.GetLisPaginado(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListSolicitudAprobador(int? sociedad = null, string fechaDesde = "", string fechaHasta = "", int? situacion = null, string nroSolicitud = null, string centroCosto = null, int page = 0, int rows = 0)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var idPerfil = UtilSession.Usuario.IdPerfil;
            var userNameRegistro = UtilSession.Usuario.UserName;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Sociedad", sociedad == null ? 0 : sociedad.Value);
            parameter.Collection.Add("IdPerfil", idPerfil);
            parameter.Collection.Add("UserNameRegistro", userNameRegistro);
            parameter.Collection.Add("FechaDesde", fechaDesde);
            parameter.Collection.Add("FechaHasta", fechaHasta);
            parameter.Collection.Add("IdSituacionServicio", situacion == null ? 0 : situacion.Value);
            parameter.Collection.Add("NroSolicitud", nroSolicitud);
            parameter.Collection.Add("CentroCosto", centroCosto);

            var listQuery = oISolicitudServiceLogic.GetListPaginadoAprobador(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CancelarServicioPost(int idServicio, int id)
        {
            string ruc = await oISolicitudServiceLogic.GetRUCByServicio(idServicio);
            OperationResult result = await oIProveedorWebApiServiceLogic.CancelService(new ServicioRequest() { IdServicio = idServicio, RUCProveedor = ruc });
            if (result.Success)
            {
                BE.Solicitud entSolicitud = new BE.Solicitud();
                entSolicitud.ID = id;
                entSolicitud.IdSituacionServicio = 3;
                entSolicitud.IdEstadoServicioProveedor = 8;
                BE.OperationResult objResult = new BL.Solicitud().Actualizar(entSolicitud);
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AnularServicioPost(int idServicio, int id)
        {
            string ruc = await oISolicitudServiceLogic.GetRUCByServicio(idServicio);
            string usuarioRegistro = UtilSession.Usuario.NombreCompleto;
            OperationResult result = await oIProveedorWebApiServiceLogic.AnularService(new ServicioRequest() { ID = id, IdServicio = idServicio, RUCProveedor = ruc, UsuarioRegistro = usuarioRegistro });
            if (result.Success)
            {
                BE.Solicitud entSolicitud = new BE.Solicitud();
                entSolicitud.ID = id;
                entSolicitud.SituacionServicio = 1;
                entSolicitud.IdSituacionServicio = 3;
                entSolicitud.IdServicioProveedor = -1;
                entSolicitud.IdProveedorTaxi = -1;
                entSolicitud.IdEstadoServicioProveedor= 8;
                BE.OperationResult objResult = new BL.Solicitud().Actualizar(entSolicitud);

                var correo = await oISolicitudService.GetSolicitudCorreo(id);
                correo.IdTipoCorreo = (int)TipoCorreoEnum.Anulado;
                correo.Mensaje = ConfigurationManager.AppSettings["mensajeAnulacion"].Replace("{0}", correo.Codigo);
                correo.Responsable = new Data.Common.Usuario();
                correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Anulado);
                correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                var sendMail = await oIProveedorWebApiService.EmailService(correo);
            }
            return Json(result);
        }

        [HttpPost] 
        public async Task<JsonResult> GetStateService(int idServicio)
        {
            var result = await oIProveedorWebApiServiceLogic.GetState(new ServicioRequest() { IdServicio = idServicio });
            return Json(result.ObjectResult);
        }

        [HttpPost]
        public async Task<JsonResult> GetInfoService(int idServicio)
        {
            var result = await oIProveedorWebApiServiceLogic.GetService(new ServicioRequest() { IdServicio = idServicio });
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetTracking(int idServicio, int idEstadoServicio)
        {
            var list = await oIProveedorWebApiServiceLogic.GetTracking(idServicio, idEstadoServicio);
            var result = list.Distinct(new ComparerTracking()).ToList();
            return Json(result);
        }

        #region Liquidación
        public JsonResult GetListModalSolicitudPaginado(int ProveedorTaxiID, string FechaInicio, string FechaFin, int Sociedad, int page, int rows)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("IdProveedor", ProveedorTaxiID);
            parameter.Collection.Add("FechaInicio", Convert.ToDateTime(FechaInicio));
            parameter.Collection.Add("FechaFin", Convert.ToDateTime(FechaFin));
            parameter.Collection.Add("IdSociedad", Sociedad);

            var listQuery = oISolicitudServiceLogic.GetListSolicitudLiquidacion(parameter, out totalRows, out CantidadRegistros);

            //var jsonData = new
            //{
            //    total = 1,
            //    page = 1,
            //    records = listQuery.Count(),
            //    rows = listQuery
            //};

            return Json(listQuery, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSolicitudByLiquidacionID(int LiquidacionID, int page, int rows)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("IdLiquidacion", LiquidacionID);

            var listQuery = oISolicitudServiceLogic.GetSolicitudByLiquidacionID(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Carlos Huallpa
        public JsonResult GetSolicitudBuscarPaginado(int ProveedorTaxiID, string FechaInicio, string FechaFin, int IdSociedad)
        {
            BE.Solicitud objSolicitud = new BE.Solicitud();
            objSolicitud.IdProveedor = ProveedorTaxiID;
            objSolicitud.FechaInicioStr = Convert.ToDateTime(FechaInicio).ToString("yyyyMMdd");
            objSolicitud.FechaFinStr = Convert.ToDateTime(FechaFin).ToString("yyyyMMdd");
            objSolicitud.IdSociedad = IdSociedad;
            var lista = new BL.Solicitud().Paginado(objSolicitud);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Marcos Silverio

        #endregion
    }
}