using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Contracts
{
    public interface IUsuarioSucursalService
    {
        Task<int> Add(UsuarioSucursal usuarioSucursal);
        Task<int> Edit(UsuarioSucursal usuarioSucursal);
        Task<bool> Remove(UsuarioSucursal usuarioSucursal);
        Task<UsuarioSucursal> GetById(int id);
        List<UsuarioSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }
}
