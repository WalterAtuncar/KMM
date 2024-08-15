using Data.Common;
using Service.Contracts;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Service.Logic
{
    public class UsuarioServiceLogic : IUsuarioServiceLogic
    {
        private readonly IUsersService oIUsersService;
        public UsuarioServiceLogic(IUsersService oIUsersService)
        {
            this.oIUsersService = oIUsersService;
        }

        public Usuario GetById(int id)
        {
            return oIUsersService.GetById(id);
        }

        public List<Usuario> GetList()
        {
            return oIUsersService.GetList();
        }

        public OperationResult Modificar(Usuario usuario, string username)
        {
            using (var ts = new TransactionScope())
            {
                try
                {
                    int rowAffected = oIUsersService.Modificar(usuario, username);
                    ts.Complete();
                    return new OperationResult() { RowAffeted = rowAffected, Success = true };
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return new OperationResult() { Errors = new List<string>() { ex.Message }, Success = false }; 
                }
            }
        }
    }
}
