using Data.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IUsersServiceLogic
    {
        List<Data.Common.Usuario> GetListResponsable();
        Data.Common.Usuario GetByUserName(string username);
        int GetValidateByUsername(string username);
        List<Data.Common.Usuario> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
    }
}
