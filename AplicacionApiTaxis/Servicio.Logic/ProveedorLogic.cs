using Modelo.General;
using Servicio.Contract;
using Servicio.Logic.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Logic
{
    public class ProveedorLogic : IProveedorLogic
    {
        private readonly IProveedorData oIProveedorData;
        public ProveedorLogic(IProveedorData oIProveedorData)
        {
            this.oIProveedorData = oIProveedorData;
        }

        public async Task<Proveedor> GetDatoServicio(string RUC, int idMetodoProveedor)
        {
            var result = await oIProveedorData.GetDatoServicio(RUC, idMetodoProveedor);
            return result;
        }

        public async Task<List<Proveedor>> GetListProveedor()
        {
            var result = await oIProveedorData.GetListProveedor();
            return result;
        }
    }
}
