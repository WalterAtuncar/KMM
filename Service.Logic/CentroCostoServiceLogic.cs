using Data.Util;
using Service.Contracts;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Logic
{
    public class CentroCostoServiceLogic : ICentroCostoServiceLogic
    {
        private readonly ICentroCostoService oICentroCostoService;
        public CentroCostoServiceLogic(ICentroCostoService oICentroCostoService)
        {
            this.oICentroCostoService = oICentroCostoService;
        }

        public IEnumerable<Data.CentroCosto> Get(Func<Data.CentroCosto, bool> filter = null)
        {
            return  oICentroCostoService.Get(filter);
        }

        public async Task<Data.Common.CentroCosto> GetById(string codigo)
        {
            return await oICentroCostoService.GetById(codigo);
        }

        public List<Data.Common.CentroCosto> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            return oICentroCostoService.GetLisPaginado(parameter, out totalRows, out cantidadRegistros);
        }

        public async Task<OperationResult> Update(Data.Common.CentroCosto centroCosto)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int rowAffeted = await oICentroCostoService.Update(centroCosto);
                    ts.Complete();
                    return new OperationResult() { Success = true, RowAffeted = rowAffeted };
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
                }
            }
        }

        public async Task<OperationResult> ValidarCentroCosto(string codigoCentroCosto)
        {
            try
            {
                int result = await oICentroCostoService.ValidarCentroCosto(codigoCentroCosto);
                if (result ==  1)
                {
                    return new OperationResult() { Success = true };
                }
                else
                {
                    return new OperationResult() { Success = false };
                }
            }
            catch (Exception ex)
            {

                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
           
        }
    }
}
