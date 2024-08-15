using System.Collections.Generic;
using System.Threading.Tasks;
using GASTO.Domain;

namespace GASTO.Service.Contracts
{
    public interface IHistorialLogService
    {
        Task<List<HistorialLog>> GetHistorialByIdSolicitud(int IdSolicitud);
        
    }
}
