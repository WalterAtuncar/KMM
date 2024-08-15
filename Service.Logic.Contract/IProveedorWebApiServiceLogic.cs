using Data.ServiceDTO;
using Service.ServiceDTO;
using Service.ServiceDTO.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IProveedorWebApiServiceLogic
    {
        Task<List<TarifaResponse>> GetListTarifa(TarifaRequest modelo);
        Task<OperationResult> SaveService(ServicioRequest modelo);
        Task<OperationResult> AnularService(ServicioRequest modelo);
        Task<OperationResult> CancelService(ServicioRequest modelo);
        Task<OperationResult> PostLiquidación(List<LiquidacionApi> modelo);
        Task<OperationResult> GetOrdenServicio(string ordenServicio, string sociedad);
        Task<OperationResult> GetService(ServicioRequest modelo);
        Task<OperationResult> GetState(ServicioRequest modelo);
        Task<OperationResult> GetOrdenServicioLiquidacion(string ordenServicio, string sociedad);
        Task<OperationResult> GetOrdenServicioLiquidacionBatch(List<OrdenServicioBatch> modelo, string sociedad);
        Task<List<Service.General.Tracking>> GetTracking(int idServicio,int idEstadoServicio);
    }
}
