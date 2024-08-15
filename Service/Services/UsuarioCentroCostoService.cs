using Dapper;
using Data.Common;
using Service.Contracts;
using Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioCentroCostoService : IUsuarioCentroCostoService
    {
        public async Task<List<UsuarioCentroCosto>> GetUsuarioCentroCostoById(int IdUsuario)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", IdUsuario, dbType: DbType.Int32);
                var result = await cn.QueryAsync<Data.Common.UsuarioCentroCosto>("dbo.usp_UsuarioCentroCosto_By_ID", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
