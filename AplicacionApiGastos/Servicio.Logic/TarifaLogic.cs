using Common.Util;
using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Newtonsoft.Json;
using Serilog;
using Servicio.Contract;
using Servicio.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio.Logic
{
    public class TarifaLogic : ITarifaLogic
    {
        private readonly ITarifaData oITarifaData;
        private readonly IProveedorData oIProveedorData;
        private readonly IServicioData oIServicioData;
        public TarifaLogic(ITarifaData oITarifaData , IProveedorData oIProveedorData, IServicioData oIServicioData)
        {
            this.oITarifaData = oITarifaData;
            this.oIProveedorData = oIProveedorData;
            this.oIServicioData = oIServicioData;
        }

        public async Task<List<TarifaResponse>> GetListTarifa(TarifaRequest request)
        {
            Utilitario.Logger();

            Utilitario.WriteInfoLog("Obteniendo Tarifa GetListTarifa()");
            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(request));

            var listaOperationService = new List<OperationService>();
            var listaProveedores = await oIProveedorData.GetListProveedor();
            var listaTarifaResponse = new List<TarifaResponse>();

            foreach (var proveedor in listaProveedores)
            {
                try
                {
                    var servicio = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TARIFARIO);
                    var servicioToken = await oIProveedorData.GetDatoServicio(proveedor.RUC, (int)MethodProveedorEnum.OBTENER_TOKEN);

                    if (servicio == null)
                    {
                        /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE ESTE SERVICIO*/
                        break;
                    }
                    else
                    {
                        if (servicioToken.URL == null || servicioToken.UserName == null || servicio.Password == null || servicio.Metodo == null || servicio.GrantType == null)
                        {
                            /*SE REGISTRARÍA EL PROVEEDOR QUE NO TIENE TOKEN*/
                            break;
                        }
                        else
                        {
                            var token = await oIServicioData.GetToken(new Modelo.General.Token() { URL = servicioToken.URL, Metodo = servicioToken.Metodo, UserName = servicioToken.UserName, Password = servicioToken.Password, grant_type = servicioToken.GrantType });
                            Utilitario.Logger();
                            request.URL = servicio.URL;
                            request.RUCProveedor = servicio.RUC;
                            request.Metodo = servicio.Metodo;
                            request.Token = token.access_token;
                            var tarifaresponse = await GetTarifa(request);
                            //Log.Information("Consulta tarifa del proveedor " + request.RUCProveedor);
                            Utilitario.WriteInfoLog("Consulta tarifa del proveedor " + request.RUCProveedor);
                            if (tarifaresponse.Success)
                            {
                                var tarifa = (TarifaResponse)tarifaresponse.ResponseObject;
                                tarifa.IdProveedor = servicio.IdProveedor;
                                tarifa.Proveedor = servicio.RazonSocial;
                                tarifa.RUCProveedor = servicio.RUC;
                                listaOperationService.Add(new OperationService() { Success = true, ResponseObject = tarifa });
                                Utilitario.WriteInfoLog("Se agregó la tarifa del proveedor " + request.RUCProveedor);
                            }
                            else
                            {
                                Utilitario.WriteInfoLog($"Error del proveedor {request.RUCProveedor} {tarifaresponse.Message}");
                                listaOperationService.Add(tarifaresponse);
                                Utilitario.WriteInfoLog("ERROR - " + tarifaresponse.ExceptionError);
                            }
                            Log.CloseAndFlush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Log.Information("ERROR AL OBTENER TARIFA.");
                    Utilitario.WriteInfoLog("ERROR AL OBTENER TARIFA.");
                    Utilitario.WriteInfoLog("ERROR - " + ex.ToString());
                    //Log.CloseAndFlush();
                    listaOperationService.Add(new OperationService() { Success = false, ExceptionError = ex, Message = ex.Message });
                }
            }

            var listaTarifaMenorResponse = await GetTarifaMenor(listaOperationService);

            return listaTarifaMenorResponse;
        }
        public async Task<OperationService> GetTarifa(TarifaRequest request)
        {
            var result = new OperationService();
            try
            {
                result = await oITarifaData.GetTarifa(request);
                return result;
            }
            catch (Exception ex)
            {
                /*SE REGISTRA EL LOG DE ERRORES PARA LOS PROVEEDORES*/
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<List<TarifaResponse>> GetTarifaMenor(List<OperationService> listaOperationService)
        {
            var precioMenor = decimal.MaxValue;
            int proveedorID = default(int);
            var result = new List<TarifaResponse>();
            foreach (var listaTarifaResponse in listaOperationService)
            {
                if (listaTarifaResponse.Success)
                {
                    var tarifa = (TarifaResponse)listaTarifaResponse.ResponseObject;
                    if(precioMenor > tarifa.Total)
                    {
                        precioMenor = tarifa.Total;
                        proveedorID = tarifa.IdProveedor;
                    }
                    await Task.Run(() => result.Add(tarifa));
                }
            }
            result.Where(m => m.IdProveedor == proveedorID).FirstOrDefault().FlagMenor = true;
            return result;
        }

    }
}
