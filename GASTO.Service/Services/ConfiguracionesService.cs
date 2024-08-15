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
    public class ConfiguracionesService : IConfiguracionesService
    {
        public async Task<int> Add(Domain.Configuraciones configuraciones)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Clave", configuraciones.Clave, dbType: DbType.String);
                parameters.Add("@Valor1", configuraciones.Valor1, dbType: DbType.Decimal);
                parameters.Add("@Valor2", configuraciones.Valor2, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", configuraciones.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", configuraciones.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_Configuraciones_Insert", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> Edit(Domain.Configuraciones configuraciones)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", configuraciones.Id, dbType: DbType.Int32);
                parameters.Add("@Clave", configuraciones.Clave, dbType: DbType.String);
                parameters.Add("@Valor1", configuraciones.Valor1, dbType: DbType.Decimal);
                parameters.Add("@Valor2", configuraciones.Valor2, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", configuraciones.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", configuraciones.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_Configuraciones_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Domain.Configuraciones> GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);
                var result = await cn.QueryFirstOrDefaultAsync<Domain.Configuraciones>("Gastos.usp_Configuraciones_GetByID", parameters, commandType: CommandType.StoredProcedure);
                return result ?? new Domain.Configuraciones();
            }
        }

        public List<Domain.Configuraciones> GetByKey(string key)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@key", key, dbType: DbType.String);
                var results = cn.Query<Domain.Configuraciones>("Gastos.usp_Configuraciones_GetByKey", parameters, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public List<Domain.Configuraciones> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);
                var lista = cn.Query<Domain.Configuraciones>("Gastos.usp_Configuraciones_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public async Task<bool> Remove(Domain.Configuraciones configuraciones)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", configuraciones.Id, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", configuraciones.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", configuraciones.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_Configuraciones_Delete", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
