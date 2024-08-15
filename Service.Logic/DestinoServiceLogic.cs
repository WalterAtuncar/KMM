using Data.Common;
using Service.Contracts;
using Service.Logic.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic
{
    public class DestinoServiceLogic : IDestinoServiceLogic
    {
        private readonly IDestinoService oIDestinoService;
        public DestinoServiceLogic(IDestinoService oIDestinoService)
        {
            this.oIDestinoService = oIDestinoService;
        }

        public async Task<List<Destino>> GetDestinoBySolicitud(int idSolicitud)
        {
            return await oIDestinoService.GetDestinoBySolicitud(idSolicitud);
        }
    }
}
