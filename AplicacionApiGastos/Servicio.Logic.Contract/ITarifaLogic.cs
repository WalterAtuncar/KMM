using Modelo.Request;
using Modelo.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Logic.Contract
{
    public interface ITarifaLogic
    {
        Task<List<TarifaResponse>> GetListTarifa(TarifaRequest request);
    }
}
