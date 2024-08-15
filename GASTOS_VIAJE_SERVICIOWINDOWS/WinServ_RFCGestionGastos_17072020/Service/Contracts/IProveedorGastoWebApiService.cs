using Service.ServiceDTO.Response;
using Service.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IProveedorGastoWebApiService
    {
        Task<OperationService> postPagoAnticipo(List<Anticipo> modelo);
    }
}
