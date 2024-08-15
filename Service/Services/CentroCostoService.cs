using Dapper;
using Data;
using Data.Util;
using Service.Contracts;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CentroCostoService : ICentroCostoService
    {

        public IEnumerable<Data.CentroCosto> Get(Func<Data.CentroCosto, bool> filter = null)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result =  cn.Query<Data.CentroCosto>("Select * from dbo.CentroCosto");

                return result;
            }
        }

        public async Task<List<Data.CentroCosto>> List()
        {
            using (var context = new Entities())
            {
                return await context.CentroCosto.ToListAsync();
            }
        }

        public async Task<Data.CentroCosto> FindByCodigo(string Codigo)
        {
            using (var context = new Entities())
            {
                return await context.CentroCosto.FirstOrDefaultAsync(x => x.Codigo == Codigo);
            }
        }

        public string FindRUCByCodigo(string codigo)
        {
            using (var context = new Entities())
            {
                var res = context.usp_Sociedad_GetRUCByID(codigo);
                return res.FirstOrDefault();
            }
        }

        public async Task<Data.Common.CentroCosto> GetById(string codigo)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Codigo", codigo, DbType.String);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.CentroCosto>("dbo.usp_CentroCosto_By_Codigo", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.CentroCosto();
            }
        }

        public async Task<int> Update(Data.Common.CentroCosto centroCosto)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Codigo", centroCosto.Codigo, DbType.String);
                parameters.Add("@FlagCosto", centroCosto.FlagCosto, DbType.Boolean);
                parameters.Add("@FlagGasto", centroCosto.FlagGasto, DbType.Boolean);
                parameters.Add("@IdResponsable", centroCosto.IdResponsable, DbType.Int32);

                var result = await cn.ExecuteAsync("dbo.usp_CentroCosto_Update", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> ValidarCentroCosto(string codigoCentroCosto)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CodigoCentroCosto", codigoCentroCosto, DbType.String);

                var result = await cn.ExecuteScalarAsync("dbo.usp_CentroCosto_Validar", parameters, commandType: CommandType.StoredProcedure);

                return (int)result;
            }
        }

        public List<Data.Common.CentroCosto> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parameter.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parameter.Collection["Page"], DbType.Int32);
                parameters.Add("@Codigo", parameter.Collection["Codigo"], DbType.String);
                parameters.Add("@Sociedad", parameter.Collection["Sociedad"], DbType.String);
                parameters.Add("@FechaDesde", parameter.Collection["FechaDesde"], DbType.DateTime);
                parameters.Add("@FechaHasta", parameter.Collection["FechaHasta"], DbType.DateTime);
                parameters.Add("@TotalRows",1, DbType.Int32,ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros",1, DbType.Int32,ParameterDirection.InputOutput);
                parameters.Add("@FlagTipo", parameter.Collection["FlagTipo"], DbType.String);

                var result = cn.Query<Data.Common.CentroCosto>("dbo.usp_CentroCosto_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure).ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }
    }
}
