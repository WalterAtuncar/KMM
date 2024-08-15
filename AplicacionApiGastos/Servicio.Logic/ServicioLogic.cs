using Common.Enum;
using Common.Util;
using Modelo.General;
using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Modelo.XML.ArchivoPlano;
using Modelo.XML.Transaccion;
using Newtonsoft.Json;
using Serilog;
using Servicio.Contract;
using Servicio.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Transactions;

namespace Servicio.Logic
{
    public class ServicioLogic : IServicioLogic
    {
        private readonly IServicioData oIServicioData;
        private readonly IProveedorData oIProveedorData;
        Mail mail = new Mail();
        public ServicioLogic(IServicioData oIServicioData, IProveedorData oIProveedorData)
        {
            this.oIServicioData = oIServicioData;
            this.oIProveedorData = oIProveedorData;
            mail = new Modelo.Util.Mail();
        }

        public async Task<OperationService> EmailService(Correo request)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request.ListaEmailAdjunto));
            Log.CloseAndFlush();
            var result = new OperationResult();
            var cuerpo = mail.CuerpoCorreo(request);
            var listaCorreo = new List<string>();
            var correoRegistrador = (request.Responsable.Email ?? null);
            var correoResponsable = string.Empty;
            foreach (var emailAdjunto in request.ListaEmailAdjunto ?? new List<EmailAdjunto>())
            {
                if (emailAdjunto.FlagPrincipal == true)
                {
                    correoResponsable = emailAdjunto.Email;
                }
                listaCorreo.Add(emailAdjunto.Email);
            }
            var operation = await mail.EnviarCorreo(correoResponsable: correoResponsable, correoUsuarioRegistro: correoRegistrador, listaAprobador: listaCorreo, asunto: ConfigurationManager.AppSettings["asunto"].ToString(), mensaje: cuerpo);
            return operation;
        }


        public async Task<OperationService> EmailService_GV(Correo_GV request)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request.ListaBeneficiado));
            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request));
            Log.CloseAndFlush();
            var result = new OperationResult();
            var cuerpo = mail.CuerpoCorreo_GV(request);
            var listaCorreo = new List<string>();
            var correoRegistrador = (request.Responsable.Email ?? null);
            var correoResponsable = string.Empty;
            correoResponsable = request.ListaBeneficiado[0].Email;
            listaCorreo.Add(request.Email);

            var operation = await mail.EnviarCorreo_GV(correoResponsable: correoResponsable, correoUsuarioRegistro: correoRegistrador, listaAprobador: listaCorreo, asunto: ConfigurationManager.AppSettings["asunto_GV"].ToString(), mensaje: cuerpo);
            return operation;
        }


        public async Task<OperationResult> SaveService(ServicioRequest request)
        {
            Utilitario.Logger();

            var serviceResponse = new ServicioResponse();
            var result = new OperationResult();

            var proveedor = await oIProveedorData.GetDatoServicio(request.RUCProveedor, (int)MethodProveedorEnum.REGISTRAR_SERVICIO);
            var servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);
            var token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });
            if (proveedor == null)
            {
                /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
            }
            else
            {
                request.URL = proveedor.URL;
                request.Metodo = proveedor.Metodo;
                request.Token = token.access_token;
                var usuarioRegistro = request.UsuarioRegistro;
                var userNameAprobo = request.UserNameAprobo;

                var objResponse = new ServicioResponse();
                bool pasoAprobacion = false;
                var mensajeProveedor = string.Empty;
                try
                {
                    var resultAprobacion = await oIServicioData.SaveService(request);
                    mensajeProveedor = resultAprobacion.Message;
                    objResponse = (ServicioResponse)resultAprobacion.ResponseObject;
                    if (resultAprobacion.Success && objResponse.Confirmacion) pasoAprobacion = true;

                }
                catch (Exception ex)
                {
                    pasoAprobacion = false;
                    result.Message = ex.Message;
                }

                //var resultSaveServiceProveedor = await SaveServiceProveedor(request);

                if (pasoAprobacion)
                {
                    /*REGISTRAMOS EL SERVICIO EN LA BD LOCAL14*/
                    serviceResponse = objResponse;
                    result.Success = true;
                    result.ObjectResult = serviceResponse;

                    var xml = new ServicioTransaccionXML();
                    request.IdServicio = serviceResponse.IdServicio;
                    request.IdEstado = serviceResponse.IdEstado;
                    request.UsuarioRegistro = usuarioRegistro;
                    request.UserNameAprobo = userNameAprobo;
                    var transaction = ObtenerDatosServiceXML(request);
                    xml.ServiceXML = Utilitario.ObjectToXml(transaction);
                   
                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            Utilitario.Logger();
                            var rowAffeted = await oIServicioData.SaveSolicitud(xml);
                            ts.Complete(); result.RowAffected = rowAffeted;
                            Log.Information("Se aprobó la solicitud " + request.ID + " en la db correctamente");
                            Utilitario.WriteInfoLog("Se aprobó la solicitud " + request.ID + " en la db correctamente");
                            Log.CloseAndFlush();
                        }
                        catch (Exception ex)
                        {
                            ts.Dispose();
                            result.Message = ex.Message;
                            result.Success = false;
                            Utilitario.Logger();
                            Utilitario.WriteErrorLog(ex.ToString(), "Error " + request.RUCProveedor);
                            Log.CloseAndFlush();
                        }
                    }
                    
                }
                else
                {
                    /*REGISTRAMOS LOS ERRORES POR PARTE DEL PROVEEDOR*/
                    result.Message = mensajeProveedor ?? "Ocurrio un error en el servicio";
                }

            }
            return result;
           
        }
        public async Task<OperationService> SaveServiceProveedor(ServicioRequest request)
        {
            var operation = new OperationService();
            try
            {
                Utilitario.Logger();
                Utilitario.WriteInfoLog("SaveServiceProveedor()");
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request));

                var result = await oIServicioData.SaveService(request);
                if (result.Success == false || result.ResponseObject == null)
                {
                    operation.Success = false;
                    operation.Message = !result.Success ? result.Message : "No se obtuvo respuesta del servicio de integración.";

                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Nro de Solicitud de aprobación: " + request.ID + " - " + operation.Message);
                    Log.CloseAndFlush();

                    return operation;
                }

                var objResponse = (ServicioResponse)result.ResponseObject;
                if (objResponse.IdServicio > 0)
                {
                    operation = result;

                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Se aprobó la solicitud " + request.ID + " correctamente");
                }
                else
                {
                    operation.Success = false;
                    operation.Message = objResponse.Mensaje;

                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Nro de Solicitud de aprobación: " + request.ID + " - No se pudo aprobar la solicitud.");
                }
                
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.Logger();
                //Log.Error(ex, "Error: " + request.RUCProveedor);
                Utilitario.WriteInfoLog("Error: " + request.RUCProveedor + " - " + ex.ToString());
                Log.CloseAndFlush();
            }
            return operation;
        }

        public async Task<OperationResult> GetEstado(ServicioRequest request)
        {
            var serviceResponse = new ServicioResponse();
            var result = new OperationResult();

            var proveedor = await oIProveedorData.GetDatoServicio(request.RUCProveedor, (int)MethodProveedorEnum.OBTENER_ESTADO);
            var servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);
            var token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });
            if (proveedor == null)
            {
                /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
            }
            else
            {
                request.URL = proveedor.URL;
                request.Metodo = proveedor.Metodo;
                request.Token = token.access_token;

                var resultGetEstado = await GetEstadoService(request);
                if (resultGetEstado.Success)
                {
                    serviceResponse = (ServicioResponse)resultGetEstado.ResponseObject;
                    result.Success = true;
                    result.ObjectResult = serviceResponse;
                }
                else
                {
                    /*REGISTRAMOS LOS ERRORES POR PARTE DEL PROVEEDOR*/
                    result.Message = resultGetEstado.Message;
                }
            }
            return result;
        }
        public async Task<OperationService> GetEstadoService(ServicioRequest request)
        {
            var operation = new OperationService();
            try
            {
                Utilitario.Logger();
                operation.Success = true;
                var result = await oIServicioData.GetEstateService(request);
                operation = result;
                Utilitario.WriteInfoLog("Se consultó el estado del servicio " + request.ID + " correctamente");
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.WriteErrorLog(ex.ToString(), "Error: " + request.RUCProveedor);
                Log.CloseAndFlush();
            }
            return operation;
        }

        public async Task<OperationResult> GetInfoService(ServicioRequest request)
        {
            var serviceResponse = new ServicioResponse();
            var result = new OperationResult();

            var proveedor = await oIProveedorData.GetDatoServicio(request.RUCProveedor, (int)MethodProveedorEnum.OBTENER_INFOMACION_SERVICIO);
            var servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);
            var token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });

            if (proveedor == null)
            {
                /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
            }
            else
            {
                request.URL = proveedor.URL;
                request.Metodo = proveedor.Metodo;
                request.Token = token.access_token;

                var resultGetInfo = await GetInformacionService(request);
                if (resultGetInfo.Success)
                {
                    serviceResponse = (ServicioResponse)resultGetInfo.ResponseObject;
                    result.Success = true;
                    result.ObjectResult = serviceResponse;
                }
                else
                {
                    /*REGISTRAMOS LOS ERRORES POR PARTE DEL PROVEEDOR*/
                    result.Message = resultGetInfo.Message;
                }
            }
            return result;
        }
        public async Task<OperationService> GetInformacionService(ServicioRequest request)
        {
            var operation = new OperationService();
            try
            {
                Utilitario.Logger();
                operation.Success = true;
                var result = await oIServicioData.GetInfoService(request);
                operation = result;
                Utilitario.WriteInfoLog("Se obtuvo la información la solicitud " + request.ID + " correctamente");
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.WriteErrorLog(ex.ToString(), "Error: " + request.RUCProveedor);
                Log.CloseAndFlush();
            }
            return operation;
        }

        public async Task<OperationResult> CancelarService(ServicioRequest request)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog("Cancelar Servicio");
            Log.CloseAndFlush();

            var serviceResponse = new ServicioResponse();
            var result = new OperationResult();

            var proveedor = await oIProveedorData.GetDatoServicio(request.RUCProveedor, (int)MethodProveedorEnum.CANCELAR_SERVICIO);
            var servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);
            var token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });

            if (proveedor == null)
            {
                /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
            }
            else
            {
                request.URL = proveedor.URL;
                request.Metodo = proveedor.Metodo;
                request.Token = token.access_token;

                Utilitario.Logger();
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request));
                Log.CloseAndFlush();

                var resultCancelarServiceProveedor = await CancelServiceProveedor(request);

                if (resultCancelarServiceProveedor.Success)
                {
                    /*REGISTRAMOS EL SERVICIO EN LA BD LOCAL14*/
                    serviceResponse = (ServicioResponse)resultCancelarServiceProveedor.ResponseObject;
                    result.Success = true;
                    result.ObjectResult = serviceResponse;
                }
                else
                {
                    /*REGISTRAMOS LOS ERRORES POR PARTE DEL PROVEEDOR*/
                    result.Message = resultCancelarServiceProveedor.Message;
                    result.Success = false;
                }
            }
            return result;
        }
        public async Task<OperationResult> AnularService(ServicioRequest request)
        {
            Utilitario.Logger();
            ServicioResponse serviceResponse = new ServicioResponse();
            OperationResult result = new OperationResult();

            Proveedor proveedor = await oIProveedorData.GetDatoServicio(request.RUCProveedor, (int)MethodProveedorEnum.CANCELAR_SERVICIO);
            Proveedor servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);
            Token token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });
            if (proveedor == null)
            {
                /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
            }
            else
            {
                request.URL = proveedor.URL;
                request.Metodo = proveedor.Metodo;
                request.Token = token.access_token;

                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request));
                Log.CloseAndFlush();
                OperationService resultServiceProveedor = await AnularServiceProveedor(request);

                if (resultServiceProveedor.Success)
                {
                    /*REGISTRAMOS EL SERVICIO EN LA BD LOCAL14*/
                    serviceResponse = (ServicioResponse)resultServiceProveedor.ResponseObject;
                    result.Success = true;
                    result.ObjectResult = serviceResponse;

                    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            var rowAffeted = await oIServicioData.HistorialInsertAnular(request.ID, (int)SituacionServicioEnum.Cancelado, request.UsuarioRegistro,$"Servicio Proveedor : {request.IdServicio}");
                            ts.Complete();
                            result.RowAffected = rowAffeted;
                            Utilitario.Logger();
                            Utilitario.WriteInfoLog("Se canceló la solicitud " + request.ID + " en la db correctamente");
                            Log.CloseAndFlush();
                        }
                        catch (Exception ex)
                        {
                            ts.Dispose();
                            result.Message = ex.Message;
                            result.Success = false;
                            Utilitario.Logger();
                            Utilitario.WriteErrorLog(ex.ToString(), "Error: " + request.RUCProveedor);
                            Log.CloseAndFlush();
                        }
                    }
                }
                else
                {
                    /*REGISTRAMOS LOS ERRORES POR PARTE DEL PROVEEDOR*/
                    result.Success = false;
                    result.Message = resultServiceProveedor.Message;
                }
            }
            return result;
        }
        
        public async Task<Token> GetToken(Token request)
        {
            try
            {
                Token result = await oIServicioData.GetToken(request);
                Utilitario.Logger();
                Utilitario.WriteInfoLog("Se creó el token :" + request.access_token);
                Log.CloseAndFlush();
                return result;
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.ToString(), "Error");
                Log.CloseAndFlush();
                return new Token() { access_token = ex.Message };
            }
        }
        public async Task<OperationService> CancelServiceProveedor(ServicioRequest request)
        {
            Utilitario.Logger();
            var operation = new OperationService();
            try
            {
                operation.Success = true;
                var result = await oIServicioData.CancelService(request);
                Utilitario.Logger();
                Utilitario.WriteInfoLog("Se canceló el servicio " + request.ID + " correctamente");
                Log.CloseAndFlush();
                return result;
            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.Logger();
                Utilitario.WriteErrorLog(ex.ToString(), "Error");
                Log.CloseAndFlush();
            }
            return operation;
        }
        public async Task<OperationService> AnularServiceProveedor(ServicioRequest request)
        {
            var operation = new OperationService();
            try
            {
                operation.Success = true;
                Utilitario.Logger();
                var result = await oIServicioData.AnularService(request);
                if (result.Success == false || result.ResponseObject == null)
                {
                    operation.Success = false;
                    operation.Message = !result.Success ? result.Message : "No se obtuvo respuesta del servicio de integración.";
                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Nro de Solicitud de anulación: " + request.ID + " - " + operation.Message);
                    Log.CloseAndFlush();

                    return operation;
                }

                var objResponse = (ServicioResponse)result.ResponseObject;
                if (objResponse.Confirmacion)
                {
                    operation = result;
                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Se anuló el servicio " + request.ID + " correctamente");
                }
                else
                {
                    operation.Success = false;
                    operation.Message = objResponse.Mensaje;
                    Utilitario.Logger();
                    Utilitario.WriteInfoLog("Nro de Solicitud de anulación: " + request.ID + " - " + operation.Message);
                }
                
                Log.CloseAndFlush();

                return operation;

            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.Logger();
                Utilitario.WriteErrorLog(ex.ToString(), "Error");
                Log.CloseAndFlush();
            }
            return operation;
        }
        public async Task<OperationService> GetInfoServiceProveedor(ServicioRequest request)
        {
            var operation = new OperationService();
            try
            {
                operation.Success = true;
                var result = await oIServicioData.GetInfoService(request);
                Utilitario.Logger();
                Utilitario.WriteInfoLog("Se obtuvo la información del servicio " + request.ID + " correctamente");
                Log.CloseAndFlush();
                return result;
            }
            catch (Exception ex)
            {
                operation.ExceptionError = ex;
                operation.Message = ex.Message;
                operation.Success = false;
                Utilitario.WriteErrorLog(ex.ToString(), "Error");
                Log.CloseAndFlush();
            }
            return operation;
        }
        public async Task<OperationResult> PushServie(PushRequest request)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Utilitario.Logger();
                    Utilitario.WriteInfoLog($"Push: {JsonConvert.SerializeObject(request)}");
                    var xml = new ServicioTransaccionXML();
                    var transaction = ObtenerDatosServicePushXML(request);
                    xml.ServiceXML = Utilitario.ObjectToXml(transaction);

                    var rowAffeted = await oIServicioData.PushService(xml);
                    ts.Complete();

                    Utilitario.WriteInfoLog("El push del proveedor " + request.RUCProveedor + " se ejecuto correctamente");
                    Log.CloseAndFlush();
                    if (rowAffeted < 0 || rowAffeted == default(int))
                    {
                        return new OperationResult() { Success = false, Message = "Ingresar los datos de manera correcta", RowAffected = rowAffeted };
                    }

                    return new OperationResult() { Success = true, Message = "La transacción fue exitosa" ,RowAffected = rowAffeted };
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    Utilitario.WriteInfoLog("Error - " + ex.ToString());
                    Log.CloseAndFlush();
                    return new OperationResult() { Success = false, Message = ex.Message, Exception = ex };
                }
            }
        }
        private ServicioTransaccion ObtenerDatosServicePushXML(PushRequest request)
        {
            var modelo = new ServicioTransaccion();
            modelo.ArchivoPlanoServicio = new ArchivoPlanoServicio();
            modelo.ArchivoPlanoServicio.ArchivoPlanoAutoMovil = new ArchivoPlanoAutoMovil();
            modelo.ArchivoPlanoServicio.ArchivoPlanoConductor = new ArchivoPlanoConductor();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoGasto = new List<ArchivoPlanoGasto>();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino = new List<ArchivoPlanoDestino>();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoServicioDetalle = new List<ArchivoPlanoServicioDetalle>();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoTracking = new List<ArchivoPlanoTracking>();
            modelo.ArchivoPlanoServicio.IdServicio = request.IdServicio;
            modelo.ArchivoPlanoServicio.IdEstado = request.IdEstado;
            modelo.ArchivoPlanoServicio.IdConductor = request.Conductor.ID;
            modelo.ArchivoPlanoServicio.IdAutomovil = request.AutoMovil.ID;
            modelo.ArchivoPlanoServicio.RUCProveedor = request.RUCProveedor;

            var servicioDetalle = new ArchivoPlanoServicioDetalle();
            servicioDetalle.IdConductor = request.Conductor.ID;
            servicioDetalle.IdAutomovil = request.AutoMovil.ID;
            servicioDetalle.NombreConductor = request.Conductor.NombreCompleto;
            servicioDetalle.DocumentoConductor = request.Conductor.Documento;
            servicioDetalle.MarcaAutomovil = request.AutoMovil.Marca;
            servicioDetalle.PlacaAutomovil = request.AutoMovil.Placa;
            servicioDetalle.ColorAutomovil = request.AutoMovil.Color;
            servicioDetalle.ModeloAutomovil = request.AutoMovil.Modelo;
            servicioDetalle.FotoAutomovil = request.AutoMovil.Foto;
            servicioDetalle.CantidadPasajeros = request.AutoMovil.CantidadPasajeros;
            servicioDetalle.Estrellas = request.Conductor.Estrellas;
            servicioDetalle.FotoConductor = request.Conductor.Foto;
            servicioDetalle.Longitud = request.ServicioDetalle.Longitud;
            servicioDetalle.Latitud = request.ServicioDetalle.Latitud;
            servicioDetalle.IdEstado = request.IdEstado;
            servicioDetalle.Observacion = request.ServicioDetalle.Observacion;
            servicioDetalle.FechaInicio = Convert.ToDateTime(request.ServicioDetalle.FechaInicio);
            servicioDetalle.FechaFin = Convert.ToDateTime(request.ServicioDetalle.FechaFin);

            modelo.ArchivoPlanoServicio.ListaArchivoPlanoServicioDetalle.Add(servicioDetalle);

            

            foreach (var destino in request.ListaDestino ?? new List<Destino>())
            {
                var destinoPlano = new ArchivoPlanoDestino();
                destinoPlano.LatitudDestino = destino.LatitudDestino;
                destinoPlano.LatitudOrigen = destino.LatitudOrigen;
                destinoPlano.LongitudOrigen = destino.LongitudOrigen;
                destinoPlano.LongitudDestino = destino.LongitudDestino;
                destinoPlano.DireccionOrigen = destino.DireccionOrigen;
                destinoPlano.DireccionDestino = destino.DireccionDestino;
                destinoPlano.Orden = destino.Orden;
                destinoPlano.NumeroDireccionOrigen = destino.NumeroDireccionOrigen;
                destinoPlano.NumeroDireccionDestino = destino.NumeroDireccionDestino;
                destinoPlano.DistanciaDestinoKilometro = destino.DistanciaDestinoKilometro;
                destinoPlano.FechaInicio = Convert.ToDateTime(destino.FechaInicio);
                destinoPlano.FechaFin = Convert.ToDateTime(destino.FechaFin);
                destinoPlano.Precio = destino.Precio;
                destinoPlano.ZonaOrigen = destino.ZonaOrigen;
                destinoPlano.ZonaDestino = destino.ZonaDestino;
                destinoPlano.Negociado = destino.Negociado;
                modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino.Add(destinoPlano);
            }
            foreach (var gasto in request.ListaGastoAdicional ?? new List<GastoAdicional>())
            {
                var gastoPlano = new ArchivoPlanoGasto();
                gastoPlano.IdConcepto = gasto.Concepto.ID;
                gastoPlano.Monto = gasto.Monto;
                gastoPlano.Observacion = gasto.Observacion;
                gastoPlano.TiempoMinuto = gasto.TiempoMinuto;
                modelo.ArchivoPlanoServicio.ListaArchivoPlanoGasto.Add(gastoPlano);
            }
            foreach (var tracking in request.ListaTracking ?? new List<Modelo.General.Tracking>())
            {
                var trackingPlano = new ArchivoPlanoTracking();
                trackingPlano.FechaTracking = Convert.ToDateTime(tracking.FechaTracking);
                trackingPlano.Latitud = tracking.Latitud;
                trackingPlano.Longitud = tracking.Longitud;
                modelo.ArchivoPlanoServicio.ListaArchivoPlanoTracking.Add(trackingPlano);
            }

            return modelo;
        }
        private ServicioTransaccion ObtenerDatosServiceXML(ServicioRequest request)
        {
            var modelo = new ServicioTransaccion();
            modelo.ArchivoPlanoServicio = new ArchivoPlanoServicio();
            modelo.ArchivoPlanoServicio.ArchivoPlanoAutoMovil = new ArchivoPlanoAutoMovil();
            modelo.ArchivoPlanoServicio.ArchivoPlanoConductor = new ArchivoPlanoConductor();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoGasto = new List<ArchivoPlanoGasto>();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoCliente = new List<ArchivoPlanoCliente>();
            modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino = new List<ArchivoPlanoDestino>();
            modelo.ArchivoPlanoServicio.ID = request.ID;
            modelo.ArchivoPlanoServicio.IdServicio = request.IdServicio;
            modelo.ArchivoPlanoServicio.IdEstado = request.IdEstado;
            modelo.ArchivoPlanoServicio.FechaServicio = Convert.ToDateTime(request.FechaServicio);
            modelo.ArchivoPlanoServicio.Distancia = request.Distancia;
            modelo.ArchivoPlanoServicio.RUCProveedor = request.RUCProveedor;
            modelo.ArchivoPlanoServicio.Telefono = request.Telefono;
            modelo.ArchivoPlanoServicio.IdTipoPago = request.IdTipoPago;
            modelo.ArchivoPlanoServicio.IdTipoServicio = request.IdTipoServicio;
            modelo.ArchivoPlanoServicio.UsuarioRegistro = request.UsuarioRegistro;
            modelo.ArchivoPlanoServicio.UserNameAprobo = request.UserNameAprobo;
            modelo.ArchivoPlanoServicio.TotalServicio = request.TotalServicio;
            foreach (var destino in request.ListaDestino)
            {
                var destinoPlano = new ArchivoPlanoDestino();
                destinoPlano.LatitudDestino = destino.LatitudDestino;
                destinoPlano.LatitudOrigen = destino.LatitudOrigen;
                destinoPlano.LongitudOrigen = destino.LongitudOrigen;
                destinoPlano.LongitudDestino = destino.LongitudDestino;
                destinoPlano.DireccionOrigen = destino.DireccionOrigen;
                destinoPlano.DireccionDestino = destino.DireccionDestino;
                destinoPlano.NumeroDireccionOrigen = destino.NumeroDireccionOrigen;
                destinoPlano.NumeroDireccionDestino = destino.NumeroDireccionDestino;

                modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino.Add(destinoPlano);
            }

            foreach (var cliente in request.ListaCliente)
            {
                var clientePlano = new ArchivoPlanoCliente();
                clientePlano.ApellidosCliente = cliente.ApellidosCliente;
                clientePlano.FlagPrincipal = cliente.FlagPrincipal;
                clientePlano.NombreCliente = cliente.NombreCliente;
                clientePlano.NumeroDocumentoCliente = cliente.NumeroDocumentoCliente;
                clientePlano.TipoDocumentoCliente = cliente.TipoDocumentoCliente;
                clientePlano.Telefono = cliente.Telefono;

                modelo.ArchivoPlanoServicio.ListaArchivoPlanoCliente.Add(clientePlano);
            }
            return modelo;
        } 
    }
}
