using Modelo.General;
using Modelo.Request;
using Modelo.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Contract
{
    public interface IProveedorData
    {
        Task<Proveedor> GetDatoServicio(string RUC, int idMetodoProveedor);
        Task<List<Proveedor>> GetListProveedor();
      
    }
}
