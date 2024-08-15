using Dapper;
using Data;
using Service.Contracts.Perfil;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace Service.Services
{
    public class PerfilService: IPerfilService
    {
        public OperationResult Add(Data.Common.Perfil perfil)
        {
            using (var ts = new TransactionScope())
            {
                try
                {

                    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Nombre", perfil.Nombre, DbType.String);
                        parameters.Add("@Descripcion", perfil.Descripcion, DbType.String);
                        parameters.Add("@IdEstado", perfil.IdEstado, DbType.Int32);

                        cn.Execute("[dbo].[usp_Perfil_Insert]", parameters, commandType: CommandType.StoredProcedure);

                        ts.Complete();
                        return new OperationResult() { Success = true };
                    }


                }
                catch (Exception ex)
                {
                    ts.Dispose();

                    return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
                }
            }
        }

        public OperationResult IsValid(Data.Common.Perfil perfil)
        {
            using(var context = new Entities())
            {
                try
                {
                    var result = context.Perfil.Where(m => m.Nombre.ToUpper() == perfil.Nombre.ToUpper()).ToList();
                    if (result.Count > 0)
                    {
                        return new OperationResult() { Success = true, ObjectResult = true };
                    }
                    else
                    {
                        return new OperationResult() { Success = true, ObjectResult = false };
                    }
                }
                catch (Exception ex)
                {
                    return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
                }
            }
        }

        public OperationResult Modificar(Data.Common.Perfil perfil)
        {
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ID", perfil.ID, DbType.Int32);
                    parameters.Add("@Nombre", perfil.Nombre, DbType.String);
                    parameters.Add("@Descripcion", perfil.Descripcion, DbType.String);
                    parameters.Add("@IdEstado", perfil.IdEstado, DbType.Int32);

                    var result = cn.Execute("[dbo].[usp_Perfil_Update]", parameters, commandType: CommandType.StoredProcedure);
                    return new OperationResult() { Success = true };
                }
            }
            catch (Exception ex)
            {

                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
           
        }

        public Data.Common.Perfil GetById(int id)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<Data.Common.Perfil>("SELECT * FROM dbo.Perfil").Where(m => m.ID == id).ToList().FirstOrDefault();

                return result;
            }
        }
        public List<Data.Common.Perfil> ListarPaginado(int PageSize, int PageIndex, out int TotalRows, out int CantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", PageSize, DbType.Int32);
                parameters.Add("@PageIndex", PageIndex, DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var result = cn.Query<Data.Common.Perfil>("dbo.usp_Perfil_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure).ToList();
                TotalRows = parameters.Get<int>("@TotalRows");
                CantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

        public List<Perfil> GetList()
        {
            using (var context = new Entities())
            {
                return context.Set<Perfil>().ToList();
            }
        }
    }
}
