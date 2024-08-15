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
    public class Sucursal : BaseGASTO
    {

        
        public IEnumerable<BE.Sucursal> List()
        {
            List<BE.Sucursal> lista = new List<BE.Sucursal>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {
                   
                    lista = cn.Query<BE.Sucursal>("select * from Gastos.Sucursal", null, commandType: CommandType.Text).ToList();
                 }
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            return lista;
        }

        


    }
}
