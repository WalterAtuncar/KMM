using Data.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICentroCostoService 
    {
        IEnumerable<Data.CentroCosto> Get(Func<Data.CentroCosto, bool> filter = null);
        Task<List<Data.CentroCosto>> List();
        Task<Data.CentroCosto> FindByCodigo(string Codigo);
        Task<Data.Common.CentroCosto> GetById(string codigo);
        Task<int> Update(Data.Common.CentroCosto centroCosto);
        Task<int> ValidarCentroCosto(string codigoCentroCosto);
        List<Data.Common.CentroCosto> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
    }
}
