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
    public class DestinoService : IDestinoService
    {
        public async Task<List<Destino>> GetDestinoBySolicitud(int idSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", idSolicitud, dbType: DbType.Int32);

                var result = await cn.QueryAsync<Data.Common.Destino>("dbo.usp_Destino_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
