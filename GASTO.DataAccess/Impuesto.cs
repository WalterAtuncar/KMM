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
    public class Impuesto : BaseGASTO
    {
        public IEnumerable<BE.Impuesto> List()
        {
            List<BE.Impuesto> lista = new List<BE.Impuesto>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.Impuesto>("select * from Gastos.Impuesto", null, commandType: CommandType.Text).ToList();
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
