using Dapper;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Util;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;
using GASTO.Domain.Util;


namespace GASTO.Service.Services
{
    public class MaestrosService : IMaestrosService
    {
        public List<Maestros> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
                parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
                parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
                parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

                var lista = cn.Query<Maestros>("Gastos.usp_Maestros_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
                var result = lista.ToList();
                totalRows = parameters.Get<int>("@TotalRows");
                cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
                return result;
            }
        }

       
    }
}
