using Dapper;
using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GASTO.Service.Services
{
    public class SociedadSucursalService : ISociedadSucursalService
    {
       
        public async Task<int> Add(Domain.SociedadSucursal sociedadSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdSucursal", sociedadSucursal.IdSucursal, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", sociedadSucursal.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", sociedadSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", sociedadSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_SociedadSucursal_Insert", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> Edit(Domain.SociedadSucursal sociedadSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", sociedadSucursal.ID, dbType: DbType.Int32);
                parameters.Add("@IdSucursal", sociedadSucursal.IdSucursal, dbType: DbType.Int32);
                parameters.Add("@CodigoSociedadFI", sociedadSucursal.CodigoSociedadFI, dbType: DbType.String);
                parameters.Add("@UsuarioRegistro", sociedadSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", sociedadSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_SociedadSucursal_Update", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Domain.SociedadSucursal> GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Domain.SociedadSucursal>("Gastos.usp_SociedadSucursal_GetByID", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Domain.SociedadSucursal();
            }
        }

        public List<Domain.Sucursal> ListarByIdSociedad(string idsociedad)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoSociedadFI", idsociedad, DbType.String);
                var lista = cn.Query<Domain.Sucursal>("Gastos.usp_SucursalByIdSociedad_Listar", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                return result;
            }
        }

        public List<Domain.SociedadSucursal> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var lista = cn.Query<Domain.SociedadSucursal>("Gastos.usp_SociedadSucursal_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public async Task<bool> Remove(SociedadSucursal sociedadSucursal)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", sociedadSucursal.ID, dbType: DbType.Int32);
                parameters.Add("@UsuarioRegistro", sociedadSucursal.UsuarioRegistro, dbType: DbType.String);
                parameters.Add("@UserNameRegistro", sociedadSucursal.UserNameRegistro, dbType: DbType.String);
                var result = await cn.ExecuteAsync("Gastos.usp_SociedadSucursal_Delete", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
