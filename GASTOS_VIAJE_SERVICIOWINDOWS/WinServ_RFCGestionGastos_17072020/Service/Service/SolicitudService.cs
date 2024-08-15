using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Service.Util;

using System.Linq;
using Service.Entidad;
using Dapper;

namespace Service.Service
{
    public static class SolicitudService 
    {
        public static List<Solicitud> ListDatosPagosAnticipos()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                //var parameters = new DynamicParameters();
                //parameters.Add("@IdSolicitud", IdSolicitud, dbType: DbType.Int32);
                var result = cn.Query<Solicitud>("Gastos.usp_ListDatosPagosAnticipos", null, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public static int UpdatePagoAnticipo(Solicitud solicitud)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                //parameters.Add("@IdSolicitud", solicitud.IdSolicitud, dbType: DbType.Int32);
                parameters.Add("@Codigo", solicitud.Codigo, dbType: DbType.String);
                parameters.Add("@CodigoPago", solicitud.CodigoPago, dbType: DbType.String);
                //NEY TANGOA 16/07/2020
                parameters.Add("@FechaPago", solicitud.FechaPago, dbType: DbType.DateTime);
                //FIN CAMBIOS
                var result = cn.Execute("Gastos.usp_PagoAnticipo_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
