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
    public class UsuarioAprobadorService : IUsuarioAprobadorService
    {
        public async Task<int> Add(UsuarioAprobador UsuarioAprobador)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", UsuarioAprobador.IdUsuario, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", UsuarioAprobador.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", UsuarioAprobador.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", UsuarioAprobador.UserNameRegistro, dbType: DbType.String);
                parameters.Add("@ListaCentroCosto", UsuarioAprobador.listaUsuarioAprobadorCentroCostoByUsuario[0], dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioAprobador_Insert", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> Edit(UsuarioAprobador UsuarioAprobador)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", UsuarioAprobador.ID, dbType: DbType.Int32);
                parameters.Add("@IdUsuario", UsuarioAprobador.IdUsuario, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", UsuarioAprobador.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", UsuarioAprobador.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", UsuarioAprobador.UserNameRegistro, dbType: DbType.String);
                parameters.Add("@ListaCentroCosto", UsuarioAprobador.listaUsuarioAprobadorCentroCostoByUsuario[0], dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioAprobador_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<UsuarioAprobador> GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<UsuarioAprobador>("Gastos.usp_UsuarioAprobador_GetByID", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new UsuarioAprobador();
            }
        }

        public List<UsuarioAprobador> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var lista = cn.Query<UsuarioAprobador>("Gastos.usp_UsuarioAprobador_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public async Task<bool> Remove(UsuarioAprobador UsuarioAprobador)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", UsuarioAprobador.ID, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", UsuarioAprobador.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", UsuarioAprobador.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_UsuarioAprobador_Delete", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public List<UsuarioAprobadorCentroCosto> GetListarUsuarioAprobadorCentroCostoById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, DbType.Int32);
                var lista = cn.Query<UsuarioAprobadorCentroCosto>("Gastos.usp_UsuarioAprobadorCentroCosto_ListarByID", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                return result;
            }
        }
    }
}
