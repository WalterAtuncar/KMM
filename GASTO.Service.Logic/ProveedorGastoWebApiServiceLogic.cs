using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Logic.Contract;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.Services;

namespace GASTO.Service.Logic
{
    public class ProveedorGastoWebApiServiceLogic : IProveedorGastoWebApiServiceLogic
    {
        private readonly IProveedorGastoWebApiService oIProveedorWebApiService;
        public ProveedorGastoWebApiServiceLogic()
        {
            this.oIProveedorWebApiService = new ProveedorGastoWebApiService();
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
                        result.Message = resultEstado.Mensaje;
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
            if (modelo.BUKRS != sociedad)
            {
                result.Success = false;
                result.Mensaje = "La OS no pertenece a la sociedad";
            }
            else if (modelo.STATUS == "D")
            {
                result.Success = false;
                result.Mensaje = "Cierre Definitivo";
            }
            else if (modelo.STATUS == "C")
            {
                result.Success = true;
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
                result.Mensaje = "Liberada";
            }
            return result;
        }

    } 
}
