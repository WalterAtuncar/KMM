using Modelo.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Logic.Contract
{
    public interface IProveedorLogic
    {
        Task<Proveedor> GetDatoServicio(string RUC, int idMetodoProveedor);
        Task<List<Proveedor>> GetListProveedor();
    }
}
