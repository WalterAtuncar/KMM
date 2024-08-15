using Modelo.General;
using Modelo.Request;
using Modelo.Util;
using System.Threading.Tasks;

namespace Servicio.Logic.Contract
{
    public interface IServicioLogic
    {
        Task<OperationService> EmailService(Correo request);
        Task<OperationService> EmailService_GV(Correo_GV request);
        Task<OperationResult> SaveService(ServicioRequest request);
        Task<OperationResult> GetEstado(ServicioRequest request);
        Task<OperationResult> GetInfoService(ServicioRequest request);
        Task<OperationResult> CancelarService(ServicioRequest request);
        Task<OperationResult> AnularService(ServicioRequest request);
        Task<OperationResult> PushServie(PushRequest request);
    }
}
