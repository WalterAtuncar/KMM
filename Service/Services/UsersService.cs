using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Service.Util;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Data.Util;

namespace Service.Services
{
    public class UsersService : IUsersService
    {

        public IEnumerable<Data.Usuario> Get(Func<Data.Usuario, bool> filter = null)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<Data.Usuario>("Select * from dbo.Usuario");
                return result;
            }
        }

        public List<Data.Common.Usuario> GetBySociedad(string CodigoSociedad)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoSociedad", CodigoSociedad, dbType: DbType.String);

                var result = cn.Query<Data.Common.Usuario>("dbo.usp_Usuario_GetBySociedad", parameters, commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }


        public List<Data.Common.Usuario> GetBySociedadGastos(string CodigoSociedad)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoSociedad", CodigoSociedad, dbType: DbType.String);

                var result = cn.Query<Data.Common.Usuario>("[Gastos].[usp_Usuario_GetBySociedad]", parameters, commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }

        public Data.Common.Usuario GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdUsuario", id, dbType: DbType.Int32);

                var result = cn.Query<Data.Common.Usuario>("dbo.usp_Usuario_By_ID", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result ?? new Data.Common.Usuario();
            }
        }

        public List<Data.Common.Usuario> GetList()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<Data.Common.Usuario>("dbo.usp_Usuario_List", commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }


        public List<Data.Common.Usuario> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
                parameters.Add("@Nombres", parameter.Collection["Nombres"], DbType.String);
                parameters.Add("@Apellidos", parameter.Collection["Apellidos"], DbType.String);
                parameters.Add("@Email", parameter.Collection["Email"], DbType.String);
                parameters.Add("@CodigoCentroCosto", parameter.Collection["CodigoCentroCosto"], DbType.String);
                parameters.Add("@NumeroDocumento", parameter.Collection["NumeroDocumento"], DbType.String);
                parameters.Add("@IdPerfil", parameter.Collection["IdPerfil"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var result = cn.Query<Data.Common.Usuario>("dbo.usp_User_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure).AsList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public int Modificar(Data.Common.Usuario usuario, string username)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdPefil", usuario.IdPerfil, dbType: DbType.Int32);
                parameters.Add("@IdUsuario", usuario.ID, dbType: DbType.Int32);
                parameters.Add("@CodigoCentroCostro", usuario.CodigoCentroCosto, dbType: DbType.String);
                parameters.Add("@ListaCentroCosto", usuario.ListaCentroCosto, dbType: DbType.String);
                parameters.Add("@UserNameModificacion", username, dbType: DbType.String);

                var result = cn.Execute("dbo.usp_User_Update", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public List<Data.Common.Usuario> GetListResponsable()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<Data.Common.Usuario>("dbo.usp_Responsable_Listar", commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public Data.Common.Usuario GetByUserName(string username)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username, dbType: DbType.String);

                var result = cn.Query<Data.Common.Usuario>("dbo.usp_User_Get_By_UserName", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result ?? new Data.Common.Usuario();
            }
        }

        public int GetValidateByUsername(string username)
        {
            using(var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username, dbType: DbType.String);

                var result = cn.Query<int>("dbo.usp_User_ValidateByUsername", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }

        public int GetValidateUrl(int IdPerfil, string URL)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdPerfil", IdPerfil, dbType: DbType.Int32);
                parameters.Add("@URL", URL, dbType: DbType.String);

                var result = cn.Query<int>("dbo.usp_Perfil_Validate_Pagina", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }
    }
}
