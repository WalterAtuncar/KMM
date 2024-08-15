using Dapper;
using GASTO.Service.Contracts;
using Service.Util;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GASTO.Service.Services
{
    public class UsersGastoService : IUsersGastoService
    {

        public GASTO.Domain.Usuario GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", id, dbType: DbType.Int32);

                var result = cn.Query<GASTO.Domain.Usuario>("Gastos.usp_Usuario_By_ID", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result ?? new GASTO.Domain.Usuario();
            }
        }

        //NEY TANGOA 14/10/2020

        public GASTO.Domain.Usuario GetUsuarioByUserName(string UserName)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", UserName, dbType: DbType.String);

                var result = cn.Query<GASTO.Domain.Usuario>("[Gastos].[usp_Usuario_By_UserName]", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result ?? new GASTO.Domain.Usuario();
            }
        }

        //FIN CAMBIOS
    }
}
