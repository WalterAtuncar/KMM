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
    public class Sociedad : BaseGASTO
    {
        public IEnumerable<BE.Sociedad> List()
        {
            List<BE.Sociedad> lista = new List<BE.Sociedad>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.Sociedad>("select * from Sociedad", null, commandType: CommandType.Text).ToList();
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
