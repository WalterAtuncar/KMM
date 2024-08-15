using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Logic.Contract
{
    public interface IConfiguracionesServiceLogic
    {
        Task<int> Add(Configuraciones configuraciones);
        Task<int> Edit(Configuraciones configuraciones);
        Task<bool> Remove(Configuraciones configuraciones);
        Task<Configuraciones> GetById(int id);
        //Task<Configuraciones> GetByKey(string key);
        List<Configuraciones> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }
}
