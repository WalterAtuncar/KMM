using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IBeneficiadoServiceLogic
    {
        OperationResult Delete(int ID, int SolicitudID);
        OperationResult Add(Data.Common.Beneficiado Beneficiado);
    }
}
