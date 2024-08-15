using Data;
using Data.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts.Proveedor
{
    public interface IProveedorService
    {
        Task<int> Add(Data.Common.ProveedorTaxi proveedor);
        Task<bool> ValidarProveedor(string ruc, int id);
        Task<int> Modificar(Data.Common.ProveedorTaxi proveedor);
        Task<OperationResult> AddCredencial(Data.CredencialesProveedorTaxi credencial);
        Task<OperationResult> ModificarCredencial(Data.CredencialesProveedorTaxi credencial);
        OperationResult AddMethodService(List<usp_ServicioProveedorTaxi_List_Result> listaMethod, int IdProveedor);
        Task<CredencialesProveedorTaxi> GetCredencialesById(int id);
        Task<Data.Common.ProveedorTaxi> GetProveedorById(int id);
        IEnumerable<ProveedorTaxi> Listar();
        Task<string> Key();
        List<Data.Common.ProveedorTaxi> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }
}
