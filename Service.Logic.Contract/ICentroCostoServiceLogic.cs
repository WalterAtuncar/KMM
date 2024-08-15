using Data.Common;
using Data.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface ICentroCostoServiceLogic
    {
        Task<Data.Common.CentroCosto> GetById(string codigo);
        Task<OperationResult> Update(CentroCosto centroCosto);
        Task<OperationResult> ValidarCentroCosto(string codigoCentroCosto);
        List<Data.Common.CentroCosto> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
        IEnumerable<Data.CentroCosto> Get(Func<Data.CentroCosto, bool> filter = null);
    }
}
