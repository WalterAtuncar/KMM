using Modelo.Request;
using Modelo.Util;
using System.Threading.Tasks;

namespace Servicio.Contract
{
    public interface ITarifaData
    {
        Task<OperationService> GetTarifa(TarifaRequest request);
    }
}
