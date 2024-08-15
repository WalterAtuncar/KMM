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
    public class EmailService : IEmailService
    {
        public async Task<List<Usuario>> GetListAprobadorByCentroCosto(string codigo)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoCentroCosto", codigo, dbType: DbType.String);

                var result = await cn.QueryAsync<Data.Common.Usuario>("dbo.usp_AprobadorByCentroCosto_List", parameters, commandType: CommandType.StoredProcedure);
               
                return result.ToList();
            }
        }

        public async Task<List<EmailAdjunto>> GetListEmailBySolicitud(int newID, int? IdServicioProveedor, int IdTipoCorreo)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSolicitud", newID, dbType: DbType.Int32);
                parameters.Add("@IdServicioProveedor", IdServicioProveedor, dbType: DbType.Int32);
                parameters.Add("@IdTipoCorreo", IdTipoCorreo, dbType: DbType.Int32);

                var result = await cn.QueryAsync<EmailAdjunto>("dbo.usp_GetListEmailBySolicitud", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<Usuario> GetListResponsableByCentroCosto(string codigo)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoCentroCosto", codigo, dbType: DbType.String);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.Usuario>("dbo.usp_ResponsableByCentroCosto_List", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<List<Usuario>> GetListSoporteAdministrativo()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = await cn.QueryAsync<Data.Common.Usuario>("dbo.usp_SoporteAdministrativo_List", commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<Data.Common.Usuario> GetUsuarioRegistroBySolicitud(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int16);

                return await cn.QueryFirstOrDefaultAsync<Data.Common.Usuario>("dbo.usp_UsuarioRegistroBySolicitud_List", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
