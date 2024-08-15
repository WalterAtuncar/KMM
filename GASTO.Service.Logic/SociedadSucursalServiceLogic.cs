using System.Collections.Generic;
using System.Threading.Tasks;
using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Logic.Contract;
using GASTO.Service.Services;

namespace GASTO.Service.Logic
{
    public class SociedadSucursalServiceLogic : ISociedadSucursalServiceLogic
    {
        public readonly ISociedadSucursalService oISociedadSucursalService;
        public SociedadSucursalServiceLogic()
        {
            this.oISociedadSucursalService = new SociedadSucursalService();
        }

        public Task<int> Add(SociedadSucursal sociedadSucursal)
        {
            return oISociedadSucursalService.Add(sociedadSucursal);
        }

        public Task<int> Edit(SociedadSucursal sociedadSucursal)
        {
            return oISociedadSucursalService.Edit(sociedadSucursal);
        }

        public Task<SociedadSucursal> GetById(int id)
        {
            return oISociedadSucursalService.GetById(id);
        }

        public List<Sucursal> ListarByIdSociedad(string idsociedad)
        {
            return oISociedadSucursalService.ListarByIdSociedad(idsociedad);
        }

        public List<SociedadSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            return oISociedadSucursalService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
        }

        public Task<bool> Remove(SociedadSucursal sociedadSucursal)
        {
            return oISociedadSucursalService.Remove(sociedadSucursal);
        }
    }
}
