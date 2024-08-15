using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Contracts
{
    public interface ISociedadSucursalService
    {
        Task<int> Add(SociedadSucursal sociedadSucursal);
        Task<int> Edit(SociedadSucursal sociedadSucursal);
        Task<bool> Remove(SociedadSucursal sociedadSucursal);
        Task<SociedadSucursal> GetById(int id);
        List<SociedadSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
        List<Sucursal> ListarByIdSociedad(string idsociedad);
    }
}
