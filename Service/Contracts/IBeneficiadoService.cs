using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBeneficiadoService
    {
        OperationResult Delete(int ID, int SolicitudID);
        OperationResult Add(Data.Common.Beneficiado Beneficiado);
    }
}
