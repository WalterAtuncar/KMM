using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;

namespace GASTO.Service.Logic.Contract
{
    public interface IMaestrosServiceLogic
    {
        List<Maestros> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }
}
