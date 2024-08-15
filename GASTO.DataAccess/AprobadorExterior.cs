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
    public class AprobadorExterior : BaseGASTO
    {
        public IEnumerable<BE.AprobadorExterior> List()
        {
            List<BE.AprobadorExterior> lista = new List<BE.AprobadorExterior>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    lista = cn.Query<BE.AprobadorExterior>("select ID, isnull(NumeroDocumento,'') +' - '+Isnull(Apellidos,'')+' '+Isnull(Nombres,'') UsuarioAprobador from Gastos.AprobadorExterior where IdEstado = 'AC'", null, commandType: CommandType.Text).ToList();
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
