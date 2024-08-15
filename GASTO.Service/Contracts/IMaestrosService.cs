using GASTO.Domain;
using GASTO.Domain.Util;
using System.Collections.Generic;


namespace GASTO.Service.Contracts
{
    public interface IMaestrosService
    {
        List<Maestros> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros);
    }

}
