using Data;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Data.Common;

namespace Service.Services
{
    public class BeneficiadoService : IBeneficiadoService
    {
        public OperationResult Add(Data.Common.Beneficiado Beneficiado)
        {
            try
            {
                using (var context = new Entities())
                {
                    using (var scope = new TransactionScope())
                    {
                        context.usp_Beneficiado_Add(Beneficiado.SolicitudID, Beneficiado.IdTipoDocumento, Beneficiado.ID, Beneficiado.Apellidos, Beneficiado.Nombre, Beneficiado.Telefono, Beneficiado.FlagPrincipal, Beneficiado.NumeroDocumento, Beneficiado.CodigoCentroCosto);
                        scope.Complete();
                        return new OperationResult() { Success = true };
                    }
                }
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
        }

        public OperationResult Delete(int ID, int SolicitudID)
        {
            try
            {
                using (var context = new Entities())
                {
                    using (var scope = new TransactionScope())
                    {
                        context.usp_Beneficiado_Delete(ID, SolicitudID);
                        scope.Complete();
                        return new OperationResult() { Success = true };
                    }
                }
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
        }
    }
}
