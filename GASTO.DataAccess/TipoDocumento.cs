using Dapper;
using GASTO.DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BE = GASTO.Domain;

namespace GASTO.DataAccess
{
    public class TipoDocumento : BaseGASTO
    {
        public IEnumerable<BE.TipoDocumento> List(int idtipo)
        {
            List<BE.TipoDocumento> lista = new List<BE.TipoDocumento>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@IdTipo", idtipo, DbType.Int32);
                    lista = cn.Query<BE.TipoDocumento>("Gastos.usp_ListarSolicitudTipoDocumento", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return lista;
        }
    }
}
