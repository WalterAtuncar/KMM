using Data;
using Service.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClienteService
    {
        usp_Cliente_Select_Result Find(int id);
    }
}
