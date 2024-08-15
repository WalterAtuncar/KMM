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
    public class UsuarioAprobadorServiceLogic : IUsuarioAprobadorServiceLogic
    {
        public readonly IUsuarioAprobadorService oIUsuarioAprobadorService;
        public UsuarioAprobadorServiceLogic()
        {
            this.oIUsuarioAprobadorService = new UsuarioAprobadorService();
        }

        public Task<int> Add(UsuarioAprobador usuarioAprobador)
        {
            return oIUsuarioAprobadorService.Add(usuarioAprobador);
        }

        public Task<int> Edit(UsuarioAprobador usuarioAprobador)
        {
            return oIUsuarioAprobadorService.Edit(usuarioAprobador);
        }

        public Task<UsuarioAprobador> GetById(int id)
        {
            return oIUsuarioAprobadorService.GetById(id);
        }

        public List<UsuarioAprobador> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            var result = oIUsuarioAprobadorService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
            return result;
        }

        public Task<bool> Remove(UsuarioAprobador usuarioAprobador)
        {
            return oIUsuarioAprobadorService.Remove(usuarioAprobador);
        }
        public List<UsuarioAprobadorCentroCosto> GetListarUsuarioAprobadorCentroCostoById(int id)
        {
            var result = oIUsuarioAprobadorService.GetListarUsuarioAprobadorCentroCostoById(id);
            return result;
        }
    }
}
