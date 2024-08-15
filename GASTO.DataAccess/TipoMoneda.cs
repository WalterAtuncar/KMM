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
    public class TipoMoneda : BaseGASTO
    {
        public IEnumerable<BE.TipoMoneda> List()
        {
            List<BE.TipoMoneda> lista = new List<BE.TipoMoneda>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.TipoMoneda>("select * from Gastos.Moneda", null, commandType: CommandType.Text).ToList();
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
