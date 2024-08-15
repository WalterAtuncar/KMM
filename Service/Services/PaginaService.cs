using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Service.Services
{
    public class PaginaService
    {
        public OperationResult AddPaginaPerfil(int idPagina , int idPerfil)
        {
            using (var context = new Entities())
            {
                try
                {
                    var result = context.usp_PerfilPagina_Insert(idPagina, idPerfil);
                    return new OperationResult() { Success = true };
                }
                catch (Exception ex)
                {
                    return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
                }
            }
        }

        public OperationResult DeletePafinaPerfil(int idPagina, int idPerfil)
        {
            using (var context = new Entities())
            {
                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var result = context.usp_PerfilPagina_Delete(idPagina, idPerfil);
                        ts.Complete();
                        return new OperationResult() { Success = true };
                    }
                    catch (Exception ex)
                    {
                        ts.Dispose();
                        return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
                    }
                }
            }
        }

        public List<usp_Pagina_List_Result> GetList()
        {
            using (var context = new Entities())
            {
                return context.usp_Pagina_List().ToList();
            }
        }
        public List<usp_Pagina_List_Not_In_Perfil_Result> GetListNotPerfil(int id)
        {
            using (var context = new Entities())
            {
                return context.usp_Pagina_List_Not_In_Perfil(id).ToList();
            }
        }

        public List<usp_Pagina_List_By_Perfil_Result> GetListByPerfil(int id)
        { 
            using (var context = new Entities())
            {
                return context.usp_Pagina_List_By_Perfil(id).ToList();
            }
        }

        public List<usp_MenuPagina_Listar_By_UserName_Result> ListarMenu(string UserName)
        {
            using (var context = new Entities())
            {
                return context.usp_MenuPagina_Listar_By_UserName(UserName).ToList();
            }
        }

        public List<usp_Pagina_Listar_By_UserName_Result> ListarPaginas(string UserName)
        {
            using (var context = new Entities())
            {
                return context.usp_Pagina_Listar_By_UserName(UserName).ToList();
            }
        }
    }
}
