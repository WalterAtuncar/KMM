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
    public class AmbitoViaje : BaseGASTO
    {
        public IEnumerable<BE.AmbitoViaje> List()
        {
            List<BE.AmbitoViaje> lista = new List<BE.AmbitoViaje>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.AmbitoViaje>("SELECT * FROM Gastos.Pais WHERE Estado='A' ORDER BY 1,3 ASC", null, commandType: CommandType.Text).ToList();
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
