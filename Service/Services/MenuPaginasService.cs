using System;
using System.Collections.Generic;
using Data.Common;
using Service.Contracts;
using System.Data.SqlClient;
using Service.Util;
using Dapper;
using System.Data;
using System.Linq;

namespace Service.Services
{
    public class MenuPaginasService : IMenuPaginasService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<MenuPaginas> ListaMenus(string UserName)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName);
                var result = cn.Query<Data.Common.MenuPaginas>("dbo.usp_MenuPagina_Listar_By_UserName", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public List<Paginas> ListaPaginas(string UserName)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName);
                var result = cn.Query<Data.Common.Paginas>("dbo.usp_Pagina_Listar_By_UserName", parameters, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }
    }
}
