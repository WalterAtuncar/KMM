using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Util;

namespace GASTO.Service.Services
{
    public class UsuarioSucursalService : IUsuarioSucursalService
    {
        public async Task<int> Add(UsuarioSucursal usuarioSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSucursal", usuarioSucursal.IdSucursal, dbType: DbType.Int32);
                parameters.Add("@IdUsuario", usuarioSucursal.IdUsuario, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", usuarioSucursal.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", usuarioSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", usuarioSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioSucursal_Insert", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> Edit(UsuarioSucursal usuarioSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", usuarioSucursal.ID, dbType: DbType.Int32);
                parameters.Add("@IdSucursal", usuarioSucursal.IdSucursal, dbType: DbType.Int32);
                parameters.Add("@IdUsuario", usuarioSucursal.IdUsuario, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", usuarioSucursal.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", usuarioSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", usuarioSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioSucursal_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<UsuarioSucursal> GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<UsuarioSucursal>("Gastos.usp_UsuarioSucursal_GetByID", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new UsuarioSucursal();
            }
        }

        public List<UsuarioSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var lista = cn.Query<UsuarioSucursal>("Gastos.usp_UsuarioSucursal_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public async Task<bool> Remove(UsuarioSucursal usuarioSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", usuarioSucursal.ID, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", usuarioSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", usuarioSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioSucursal_Delete", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
