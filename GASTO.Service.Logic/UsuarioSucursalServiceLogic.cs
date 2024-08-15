using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Logic.Contract;
using GASTO.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Logic
{
    public class UsuarioSucursalServiceLogic : IUsuarioSucursalServiceLogic
    {
        public readonly IUsuarioSucursalService oIUsuarioSucursalService;
        public UsuarioSucursalServiceLogic()
        {
            this.oIUsuarioSucursalService = new UsuarioSucursalService();
        }

        public Task<int> Add(UsuarioSucursal usuarioSucursal)
        {
            return oIUsuarioSucursalService.Add(usuarioSucursal);
        }

        public Task<int> Edit(UsuarioSucursal usuarioSucursal)
        {
            return oIUsuarioSucursalService.Edit(usuarioSucursal);
        }

        public Task<UsuarioSucursal> GetById(int id)
        {
            return oIUsuarioSucursalService.GetById(id);
        }

        public List<UsuarioSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            var result = oIUsuarioSucursalService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
            return result;
        }

        public Task<bool> Remove(UsuarioSucursal usuarioSucursal)
        {
            return oIUsuarioSucursalService.Remove(usuarioSucursal);
        }
    }
}
