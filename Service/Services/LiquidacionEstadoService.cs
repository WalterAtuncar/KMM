using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Data;
using Service.Contracts;
using Service.Util;

namespace Service.Services
{
    public class LiquidacionEstadoService : ILiquidacionEstadoService
    {
        public IEnumerable<LiquidacionEstado> Get()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<LiquidacionEstado>("Select * from dbo.LiquidacionEstado");
                return result;
            }
        }
    }
}
