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
    public class TipoGastosViaje : BaseGASTO
    {
        public IEnumerable<BE.TipoGastosViaje> List()
        {
            List<BE.TipoGastosViaje> lista = new List<BE.TipoGastosViaje>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.TipoGastosViaje>("select * from Gastos.TipoGastosViaje", null, commandType: CommandType.Text).ToList();
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
