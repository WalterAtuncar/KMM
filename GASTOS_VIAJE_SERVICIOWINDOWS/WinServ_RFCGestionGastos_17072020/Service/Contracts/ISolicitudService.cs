using Service.Entidad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISolicitudService
    {
        Task<List<Solicitud>> ListDatosPagosAnticipos();
        Task<int> UpdatePagoAnticipo(Solicitud solicitud);
    }
}
