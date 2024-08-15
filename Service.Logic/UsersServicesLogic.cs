using Data.Common;
using Data.Util;
using Service.Contracts;
using Service.Logic.Contract;
using System.Collections.Generic;

namespace Service.Logic
{
    public class UsersServicesLogic : IUsersServiceLogic
    {
        private readonly IUsersService oIUsersService;

        public UsersServicesLogic(IUsersService oIUsersService)
        {
            this.oIUsersService = oIUsersService;
        }

        public List<Usuario> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            return oIUsersService.GetLisPaginado(parameter, out totalRows, out cantidadRegistros);
        }

        public List<Data.Common.Usuario> GetListResponsable()
        {
            var result = oIUsersService.GetListResponsable();
            return result;
        }
        public Data.Common.Usuario GetByUserName(string username)
        {
            return oIUsersService.GetByUserName(username);
        }

        public int GetValidateByUsername(string username)
        {
            return oIUsersService.GetValidateByUsername(username);
        }
    }
}
