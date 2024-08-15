using Data.Common;
using Modelo.General;
using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Service.Contracts;
using Service.Contracts.Proveedor;
using Service.Logic;
using Service.Logic.Contract;
using Service.Services;
using Servicio;
using Servicio.Contract;
using Servicio.Logic;
using Servicio.Logic.Contract;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiAplications.Util;


namespace WebApiAplications.Controllers
{
    //[Authorize]
    [RoutePrefix("api/servicio")]
    public class ServicioController : ApiController
    {
        private readonly IServicioLogic oIServicioLogic;
        private readonly IServicioData oIServicioData;
        private readonly ISolicitudServiceLogic oISolicitudServiceLogic;
        private readonly ISolicitudService oISolicitudService;
        private readonly IProveedorService oIProveedorService;
        private readonly IProveedorServiceLogic oIProveedorServiceLogic;
        private readonly IEmailService oIEmailService;
        public ServicioController()
        {

            if (oIServicioLogic == null) { oIServicioLogic = new ServicioLogic(new ServicioData(),new ProveedorData()); }
            if(oIServicioData == null) { oIServicioData = new ServicioData(); }
            if (oISolicitudServiceLogic == null) { oISolicitudServiceLogic = new SolicitudServiceLogic(new SolicitudService(), new ProveedorService()); }
            if (oISolicitudService == null) { oISolicitudService = new SolicitudService(); }
            if (oIProveedorService == null) { oIProveedorService = new ProveedorService(); }
            if (oIEmailService == null) { oIEmailService = new EmailService(); }
            if (oIProveedorServiceLogic == null) { oIProveedorServiceLogic = new ProveedorServiceLogic(new ProveedorService()); }
        }
        public ServicioController(IServicioLogic oIServicioLogic, IServicioData oIServicioData, ISolicitudServiceLogic oISolicitudServiceLogic, ISolicitudService oISolicitudService, IProveedorServiceLogic oIProveedorServiceLogic, IProveedorService oIProveedorService, IEmailService oIEmailService)
        {
            this.oIServicioLogic = oIServicioLogic;
            this.oIServicioData = oIServicioData;
            this.oISolicitudServiceLogic = oISolicitudServiceLogic;
            this.oISolicitudService = oISolicitudService;
            this.oIProveedorService = oIProveedorService; ;
            this.oIProveedorServiceLogic = oIProveedorServiceLogic;
            this.oIEmailService = oIEmailService;
        }

        [Route("emailService")]
        [HttpPost]
        public async Task<IHttpActionResult> EmailService([FromBody] Correo modelo)
        {
            var result = await oIServicioLogic.EmailService(modelo);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Json(result);
            }
        }

        [Route("saveService")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveService([FromBody]ServicioRequest modelo)
        {
            var opertation = await oIServicioLogic.SaveService(modelo);
            var result = (ServicioResponse)opertation.ObjectResult;
            if (opertation.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(message: opertation.Message);
            } 
        }

        [Route("getInfoService")]
        [HttpPost]
        public async Task<IHttpActionResult> GetInfoService([FromBody]ServicioRequest modelo)
        {
            var opertation = await oIServicioLogic.GetInfoService(modelo);
            var result = (ServicioResponse)opertation.ObjectResult;
            if (opertation.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(message: opertation.Message);
            }
        }

        [Route("getStateService")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStateService([FromBody]ServicioRequest modelo)
        {
            var opertation = await oIServicioLogic.GetEstado(modelo);
            var result = (ServicioResponse)opertation.ObjectResult;
            if (opertation.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(message: opertation.Message);
            }
        }

        [Route("cancelService")]
        [HttpPost]
        public async Task<IHttpActionResult> CancelService([FromBody]ServicioRequest modelo)
        {
            var operation = await oIServicioLogic.CancelarService(modelo);
            if (operation.Success)
            {
                return Ok(new { Mensaje = "Se cancelo correctamente el servicio", Confirmacion = true });
            }
            return BadRequest(message : operation.Message); 
        }

        [Route("anularService")]
        [HttpPost]
        public async Task<IHttpActionResult> AnularService([FromBody]ServicioRequest modelo)
        {
            var operation = await oIServicioLogic.AnularService(modelo);
            if (operation.Success)
            {
                return Ok(new { Success = true, Mensaje = "Se anulo correctamente el servicio", Confirmacion = true });
            }
            return BadRequest(message: operation.Message);  
        }

        //SERVICIO APROBACION INTEGRADO
        [Route("postAprobarService")]
        [HttpPost]
        public async Task<IHttpActionResult> postAprobarService(AprobacionData dataURL)
        {
            int id = dataURL.id;
            int idProveedor = dataURL.idProveedor;
            string UsuarioRegistro = dataURL.UsuarioRegistro;
            string UserNameAprobo = dataURL.UserNameAprob;
            var respuesta = new object();
            var modelo = await oISolicitudServiceLogic.GetServicioRequestBySolicitud(id, idProveedor);
            modelo.UsuarioRegistro = UsuarioRegistro;
            modelo.UserNameAprobo = UserNameAprobo;

            if (modelo.IdEstado == (int)SolicitudEnum.Aprobado)
            {
                respuesta = BadRequest(message: "La solicitud ya fue aprobada.");
            }
            var task = new OperationResult();
            try
            {
                var opertation = await oIServicioLogic.SaveService(modelo);
                var result = (ServicioResponse)opertation.ObjectResult;
                if (opertation.Success)
                {
                    task.Success = true;
                    task.ObjectResult = opertation.ObjectResult;
                    if (task.Success)
                    {
                        var correo = await oISolicitudService.GetSolicitudCorreo(id);
                        correo.IdTipoCorreo = (int)TipoCorreoEnum.Aprobado;
                        correo.Mensaje = ConfigurationManager.AppSettings["mensajeAprobacion"].Replace("{0}", correo.Codigo);
                        correo.Responsable = new Modelo.General.Usuario();

                        var proveedorAprobado = await oIProveedorServiceLogic.GetProveedorById(idProveedor);
                        correo.RazonSocialProveedor = proveedorAprobado.RazonSocial;
                        correo.RucProveedor = proveedorAprobado.RUC;

                        correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Aprobado);
                        correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                        var rpta = await oIServicioLogic.EmailService(correo);

                        respuesta = Ok(task.ObjectResult);
                    }
                }
                else
                {
                    task.Success = false;
                    task.Message = opertation.Message;
                    respuesta = BadRequest(message: opertation.Message);
                }
            }
            catch (Exception ex)
            {
                task.Success = false;
                task.ExceptionError = ex;
                task.Message = ex.Message;
                respuesta = BadRequest(message: "Error al aprobar");
            }
            return (IHttpActionResult)respuesta;
        }

        [Route("getAprobarService")]
        [HttpGet]
        public async Task<IHttpActionResult> getAprobarService(int id, int idProveedor,string UsuarioRegistro, string UserNameAprobo)
        {
            var respuesta = new object();
            var modelo = await oISolicitudServiceLogic.GetServicioRequestBySolicitud(id, idProveedor);
            modelo.UsuarioRegistro = UsuarioRegistro;
            modelo.UserNameAprobo = UserNameAprobo;

            if (modelo.IdEstado == (int)SolicitudEnum.Aprobado)
            {
                respuesta = BadRequest(message: "La solicitud ya fue aprobada.");
            }
            var task = new OperationResult();
            try
            {
                var opertation = await oIServicioLogic.SaveService(modelo);
                var result = (ServicioResponse)opertation.ObjectResult;
                if (opertation.Success)
                {
                        task.Success = true;
                        task.ObjectResult = opertation.ObjectResult;
                        if (task.Success)
                        {
                            var correo = await oISolicitudService.GetSolicitudCorreo(id);
                            correo.IdTipoCorreo = (int)TipoCorreoEnum.Aprobado;
                            correo.Mensaje = ConfigurationManager.AppSettings["mensajeAprobacion"].Replace("{0}", correo.Codigo);
                            correo.Responsable = new Modelo.General.Usuario();

                            var proveedorAprobado = await oIProveedorServiceLogic.GetProveedorById(idProveedor);
                            correo.RazonSocialProveedor = proveedorAprobado.RazonSocial;
                            correo.RucProveedor = proveedorAprobado.RUC;

                            correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(id, null, (int)TipoCorreoEnum.Aprobado);
                            correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
                            var rpta = await oIServicioLogic.EmailService(correo);

                        respuesta = Ok(task.ObjectResult);
                        }
                }
                else
                {
                    task.Success = false;
                    task.Message = opertation.Message;
                    respuesta = BadRequest(message: opertation.Message);
                }
            }
            catch (Exception ex)
            {
                task.Success = false;
                task.ExceptionError = ex;
                task.Message = ex.Message;
                respuesta = BadRequest(message: "Error al aprobar" );
            }
            return (IHttpActionResult)respuesta;
        }

        //SERVICIO RECHAZAR INTEGRADO
        [Route("postRechazarService")]
        [HttpPost]
        public async Task<IHttpActionResult> postRechazarService(RechazoData dataURL)
        {
            var respuesta = new object();
            SolicitudRechazo entSolicitud = new SolicitudRechazo();
            entSolicitud.ID = dataURL.id;
            entSolicitud.SituacionServicio = 4;
            entSolicitud.IdSituacionServicio = 4;
            entSolicitud.MotivoRechazo = dataURL.m;
            entSolicitud.FechaCancelacion = DateTime.Now;
            entSolicitud.SolicitudDetalle = new SolicitudDetalle();
            entSolicitud.SolicitudDetalle.IdSituacion = 4;
            entSolicitud.SolicitudDetalle.UsuarioRegistro = dataURL.NombreCompleto;
            entSolicitud.SolicitudDetalle.IdSolicitud = dataURL.id;
            var objResult = await oISolicitudServiceLogic.Rechazar(entSolicitud);
            if (objResult.Success)
            {
                try
                {
                    var correo = await oISolicitudService.GetSolicitudCorreo(dataURL.id);
                    correo.IdTipoCorreo = (int)TipoCorreoEnum.Rechazado;
                    correo.Mensaje = ConfigurationManager.AppSettings["mensajeRechazo"].Replace("{0}", correo.Codigo);
                    correo.Responsable = new Modelo.General.Usuario();
                    correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(dataURL.id, null, (int)TipoCorreoEnum.Rechazado);
                    correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(dataURL.id);
                    var sendMail = await oIServicioLogic.EmailService(correo);
                    objResult.Message = "Solicitud rechazada correctamente.";
                    respuesta = Ok(objResult);
                }
                catch (Exception ex)
                {
                    respuesta = BadRequest(message: objResult.Message);
                }
            } else
            {
                respuesta = BadRequest(message: objResult.Message);
            }
            return (IHttpActionResult)respuesta;
        }

        //GASTOS VE  VIAJE
        [Route("emailService_GV")]
        [HttpPost]
        public async Task<IHttpActionResult> EmailService_GV([FromBody] Correo_GV modelo)
        {
            var result = await oIServicioLogic.EmailService_GV(modelo);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Json(result);
            }
        }
    }
}
