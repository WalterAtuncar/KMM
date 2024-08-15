using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IDestinoServiceLogic
    {
        Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud);
    }
}
