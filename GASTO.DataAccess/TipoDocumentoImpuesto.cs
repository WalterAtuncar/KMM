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
    public class TipoDocumentoImpuesto : BaseGASTO
    {
        public IEnumerable<BE.TipoDocumentoImpuesto> List()
        {
            List<BE.TipoDocumentoImpuesto> lista = new List<BE.TipoDocumentoImpuesto>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.TipoDocumentoImpuesto>("select * from Gastos.TipoDocumentoImpuesto", null, commandType: CommandType.Text).ToList();
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
