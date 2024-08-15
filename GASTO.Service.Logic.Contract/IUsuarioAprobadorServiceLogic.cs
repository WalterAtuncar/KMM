using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Logic.Contract
{
    public interface IUsuarioAprobadorServiceLogic
    {
        Task<int> Add(UsuarioAprobador UsuarioAprobador);
        Task<int> Edit(UsuarioAprobador UsuarioAprobador);
        Task<bool> Remove(UsuarioAprobador UsuarioAprobador);
        Task<UsuarioAprobador> GetById(int id);
        List<UsuarioAprobador> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
        List<UsuarioAprobadorCentroCosto> GetListarUsuarioAprobadorCentroCostoById(int id);
    }
}
