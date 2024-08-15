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
    public class Estados : BaseGASTO
    {
        public IEnumerable<BE.Estados> List()
        {
            List<BE.Estados> lista = new List<BE.Estados>();
            try
            {
                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
                {

                    //lista = cn.Query<BE.Estados>("select E.* from GASTO.Estado E where IdEstado in ( select distinct T.IdSituacionServicio from (select distinct S.IdSituacionServicio from gasto.Solicitud S where s.EstadoLogico = 1 union all select distinct S.IdSituacionServicio2 from gasto.Solicitud S where s.EstadoLogico = 1 ) T)", null, commandType: CommandType.Text).ToList();
                    lista = cn.Query<BE.Estados>("select E.*,EM.Estado from Gastos.Estado E inner join Gastos.EstadoMaestro EM ON EM.IdEstadoMaestro = E.IdEstadoMaestro", null, commandType: CommandType.Text).ToList();
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
