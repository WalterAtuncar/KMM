using Data.ServiceDTO;
using Service.ServiceDTO;
using Service.ServiceDTO.Request;
using Service.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IProveedorWebApiService
    {
        Task<OperationService> EmailService(Data.Common.Correo modelo);
        Task<OperationService> GetListTarifa(TarifaRequest modelo);
        Task<OperationService> SaveService(ServicioRequest modelo);
        Task<OperationService> AnularService(ServicioRequest modelo);
        Task<OperationService> CancelService(ServicioRequest modelo);
        Task<OperationService> PostLiquidación(List<LiquidacionApi> modelo);
        Task<OperationService> GetOrdenServicio(string ordenServicio);
        Task<OperationService> GetOrdenServicioBatch(List<OrdenServicioBatch> modelo);
        Task<OperationService> GetService(ServicioRequest modelo);
        Task<OperationService> GetState(ServicioRequest modelo);
        Task<int> UpdateStateService(int? idServicio, int idEstadoServicio);
    }
}
