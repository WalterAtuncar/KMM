using Data.Common;
using Data.ServiceDTO;
using Service.Contracts;
using Service.General;
using Service.Logic.Contract;
using Service.ServiceDTO;
using Service.ServiceDTO.Request;
using Service.ServiceDTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Logic
{
    public class ProveedorWebApiServiceLogic : IProveedorWebApiServiceLogic
    {
        private readonly IProveedorWebApiService oIProveedorWebApiService;
        private readonly ISolicitudService oISolicitudService;
        public ProveedorWebApiServiceLogic(IProveedorWebApiService oIProveedorWebApiService, ISolicitudService oISolicitudService)
        {
            this.oIProveedorWebApiService = oIProveedorWebApiService;
            this.oISolicitudService = oISolicitudService;
        }

        public async Task<OperationResult> AnularService(ServicioRequest request)
        {
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.AnularService(request);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                    var rowAffeted = await UpdateEstadoServicioProveedor(request.IdServicio, 8);
                    result.RowAffeted = rowAffeted;
                }
                else
                {
                    result.Success = false;
                    result.ObjectResult = resultServicio.ResponseObject;
                }
               
            }
            catch (Exception ex)
            {
                TraceHelper.Write(ex.ToString());
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> CancelService(ServicioRequest request)
        {
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.CancelService(request);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                    var rowAffeted = await UpdateEstadoServicioProveedor(request.IdServicio, 8);
                    result.RowAffeted = rowAffeted;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<int> UpdateEstadoServicioProveedor(int? idServicio, int idEstadoServicio)
        {
            return await oIProveedorWebApiService.UpdateStateService(idServicio, idEstadoServicio);
        }

        public async Task<List<TarifaResponse>> GetListTarifa(TarifaRequest request)
        {
            var result = new List<TarifaResponse>();
            try
            {
                var resultServicio = await oIProveedorWebApiService.GetListTarifa(request);
                if (resultServicio.Success)
                {
                    result = (List<TarifaResponse>)resultServicio.ResponseObject;
                }               
            }
            catch (Exception ex)
            {
                TraceHelper.Write(ex.ToString());
                throw ex;
              /*LOG DE ERRORES*/
            }

            return result;
        }

        public async Task<OperationResult> SaveService(ServicioRequest request)
        {
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.SaveService(request);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                }
                else
                {
                    result.Success = false;
                    result.Message = resultServicio.Message;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<OperationResult> GetService(ServicioRequest request)
        {
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.GetService(request);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                }
                else
                {
                    result.Success = false;
                    result.Message = resultServicio.Message;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<OperationResult> GetState(ServicioRequest request)
        {
            var result = new OperationResult();
            try
            {
                string ruc = await oISolicitudService.GetRUCByServicio(request.IdServicio);
                request.RUCProveedor = ruc;
                var resultServicio = await oIProveedorWebApiService.GetState(request);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                }
                else
                {
                    result.Success = false;
                    result.Message = resultServicio.Message;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<OperationResult> PostLiquidación(List<LiquidacionApi> modelo)
        {
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.PostLiquidación(modelo);
                if (resultServicio.Success)
                {
                    result.Success = true;
                    result.ObjectResult = resultServicio.ResponseObject;
                }
                else
                {
                    result.Success = false;
                    result.Message = resultServicio.Message;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }


        public async Task<OperationResult> GetOrdenServicioLiquidacionBatch(List<OrdenServicioBatch> modelo, string sociedad)
        {

            

            foreach (var ordenServiciobatch in modelo) {
                ordenServiciobatch.ordenservicio = ValidarOrdenServicio(ordenServiciobatch.ordenservicio);
                //var ordenDeServicio = ValidarOrdenServicio(ordenServiciobatch.ordenservicio);

            }

            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.GetOrdenServicioBatch(modelo);

                if (resultServicio.ResponseObject == null || ((List<OrdenServicioBatch>)resultServicio.ResponseObject).Count == 0)
                {
                    result.Success = false;
                    result.Message = "No válida";

                }
                else
                {

                    var ordenesServicio = (List<OrdenServicioBatch>)resultServicio.ResponseObject;

                    

                    foreach (var ordenServicio in ordenesServicio)
                    {
                        var resultEstado = ValidarEstadoOrdenServicioLiquidacionBatch(ordenServicio, sociedad);
                        result.ObjectResult = ((List<OrdenServicioBatch>)resultServicio.ResponseObject);
                        
                        if (resultEstado.Success)
                        {
                            ordenServicio.mensaje = resultEstado.Mensaje;
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = resultEstado.Mensaje;
                            ordenServicio.mensaje = resultEstado.Mensaje;
                        }
                    }

                    // Verificar si algún objeto en result.ObjectResult tiene mensaje diferente de nulo
                    bool algunMensajeNoNulo = ordenesServicio.Any(os => os.mensaje != null);
                    if (algunMensajeNoNulo)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        result.Success = true;
                    }


                }

            }
            catch (Exception ex)
            {
                TraceHelper.Write(ex.ToString());
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> GetOrdenServicioLiquidacion(string ordenServicio, string sociedad)
        {
            var ordenDeServicio = ValidarOrdenServicio(ordenServicio);
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.GetOrdenServicio(ordenDeServicio);
                if (resultServicio.ResponseObject == null || ((List<OrdenServicioApi>)resultServicio.ResponseObject).Count == 0)
                {
                    result.Success = false;
                    result.Message = "No válida";
                }
                else
                {
                    var resultEstado = ValidarEstadoOrdenServicioLiquidacion(((List<OrdenServicioApi>)resultServicio.ResponseObject).FirstOrDefault(), sociedad);
                    if (resultEstado.Success)
                    {
                        result.Success = true;
                        result.ObjectResult = ((List<OrdenServicioApi>)resultServicio.ResponseObject).FirstOrDefault();
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = resultEstado.Mensaje;
                    }
                }

            }
            catch (Exception ex)
            {
                TraceHelper.Write(ex.ToString());
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<OperationResult> GetOrdenServicio(string ordenServicio, string sociedad)
        {
            var ordenDeServicio = ValidarOrdenServicio(ordenServicio);
            var result = new OperationResult();
            try
            {
                var resultServicio = await oIProveedorWebApiService.GetOrdenServicio(ordenDeServicio);
                if (resultServicio.ResponseObject == null || ((List<OrdenServicioApi>)resultServicio.ResponseObject).Count == 0)
                {
                    result.Success = false;
                    result.Message = "No válida";
                }
                else
                {
                    var resultEstado = ValidarEstadoOrdenServicio(((List<OrdenServicioApi>)resultServicio.ResponseObject).FirstOrDefault(), sociedad);
                    if (resultEstado.Success)
                    {
                        result.Success = true;
                        result.ObjectResult = ((List<OrdenServicioApi>)resultServicio.ResponseObject).FirstOrDefault();
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = resultEstado.Mensaje;
                    }
                }
               
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ExceptionError = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        private string ValidarOrdenServicio(string ordenServicio)
        {
            if (ordenServicio.Length < 12)
            {
                while (ordenServicio.Length < 12)
                {
                    ordenServicio = $"0{ordenServicio}";
                }
            }
            return ordenServicio;
        }

        private OrdenServicioValid ValidarEstadoOrdenServicio(OrdenServicioApi modelo, string sociedad)
        {
            var result = new OrdenServicioValid();
            if (modelo.STATUS == "D")
            {
                result.Success = false;
                result.Mensaje = "Cierre Definitivo";
            }
            else if (modelo.STATUS == "C")
            {
                result.Success = false;
                result.Mensaje = "Cierre Técnico";
            }
            else if (modelo.STATUS == "A")
            {
                result.Success = false;
                result.Mensaje = "Abierto";
            }
            else if (modelo.BUKRS != sociedad)
            {
                result.Success = false;
                result.Mensaje = "La OS no pertenece a la sociedad";
            }
            else
            {
                result.Success = true;
            }
            return result;
        }

        private OrdenServicioValid ValidarEstadoOrdenServicioLiquidacion(OrdenServicioApi modelo, string sociedad) {
            var result = new OrdenServicioValid();
            if (modelo.STATUS == "D")
            {
                result.Success = false;
                result.Mensaje = "Cierre Definitivo";
            }
            else if (modelo.STATUS == "C")
            {
                result.Success = false;
                result.Mensaje = "Cierre Técnico";
            }
            else if (modelo.STATUS == "A")
            {
                result.Success = false;
                result.Mensaje = "Abierto";
            }
            else
            {
                result.Success = true;
            }
            return result;
        }

        private OrdenServicioValid ValidarEstadoOrdenServicioLiquidacionBatch(OrdenServicioBatch modelo, string sociedad)
        {
            var result = new OrdenServicioValid();
            if (modelo.STATUS == "D")
            {
                result.Success = false;
                result.Mensaje = "En estado de Cierre Definitivo para la OS " + modelo.AUFNR;
            }
            else if (modelo.STATUS == "C")
            {
                result.Success = false;
                result.Mensaje = "En estado de Cierre Técnico para la OS " + modelo.AUFNR;
            }
            else if (modelo.STATUS == "A")
            {
                result.Success = false;
                result.Mensaje = "En estado Abierto para la OS " + modelo.AUFNR;
            }
            else
            {
                result.Success = true;                
            }
            return result;
        }


        public async Task<List<Tracking>> GetTracking(int idServicio, int idEstadoServicio)
        {
            var listaTracking = new List<Tracking>();
            string ruc = await oISolicitudService.GetRUCByServicio(idServicio);
            //if (idEstadoServicio != 7)
            //{
            //    var modelo = GetService(new ServicioRequest() { IdServicio = idServicio , RUCProveedor = ruc });
            //    if (modelo.Success)
            //    {
            //        listaTracking = ((ServicioResponse)modelo.ObjectResult).ListaTracking;
            //    }
            //}
            //else
            //{
            //    listaTracking = oISolicitudService.GetTracking(idServicio);
            //}

            var modelo = await GetService(new ServicioRequest() { IdServicio = idServicio, RUCProveedor = ruc });
            if (modelo.Success)
            {
                listaTracking = ((ServicioResponse)modelo.ObjectResult).ListaTracking;
            }

            return listaTracking;
        }
    }
}
