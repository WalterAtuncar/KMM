using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDestinoService
    {
        Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud);
    }
}
