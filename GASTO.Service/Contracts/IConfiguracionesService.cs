using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Contracts
{
    public interface IConfiguracionesService
    {
        Task<int> Add(Configuraciones configuraciones);
        Task<int> Edit(Configuraciones configuraciones);
        Task<bool> Remove(Configuraciones configuraciones);
        Task<Configuraciones> GetById(int id);
        List<Configuraciones> GetByKey(string key);
        List<Configuraciones> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }
}
