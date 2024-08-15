using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using System.Data.SqlClient;
using Service.Util;
using Dapper;

namespace Service.Services
{
    public class MotivoCreacionSolicitudService : IMotivoCreacionSolicitudService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MotivoCreacionSolicitud> Get(Func<MotivoCreacionSolicitud, bool> filter = null)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<MotivoCreacionSolicitud>("Select * from dbo.MotivoCreacionSolicitud");
                return result;
            }
        }

        public List<Data.MotivoCreacionSolicitud> List()
        {
            using (var context = new Entities())
            {
                return context.MotivoCreacionSolicitud.ToList();
            }
        }
    }
}
