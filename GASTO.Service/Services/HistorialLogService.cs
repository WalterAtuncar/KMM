using Dapper;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GASTO.Service.Services
{
    public class HistorialLogService : IHistorialLogService
    {
        public HistorialLogService()
        {

        }

        public async Task<List<HistorialLog>> GetHistorialByIdSolicitud(int IdSolicitud)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", IdSolicitud, dbType: DbType.Int32);
                var result = await cn.QueryAsync<HistorialLog>("Gastos.usp_HistorialAprobacion_By_Solicitud", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
