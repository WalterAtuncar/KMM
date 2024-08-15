using Data.Common;
using Service.Contracts;
using Service.Logic.Contract;

namespace Service.Logic
{
    public class BeneficiadoServiceLogic : IBeneficiadoServiceLogic
    {
        private readonly IBeneficiadoService oIBeneficiadoService;

        public BeneficiadoServiceLogic(IBeneficiadoService oIBeneficiadoService)
        {
            this.oIBeneficiadoService = oIBeneficiadoService;
        }

        public OperationResult Add(Beneficiado Beneficiado)
        {
            var result = oIBeneficiadoService.Add(Beneficiado);
            return result;
        }

        public OperationResult Delete(int ID, int SolicitudID)
        {
            var result = oIBeneficiadoService.Delete(ID, SolicitudID);
            return result;
        }
    }
}
