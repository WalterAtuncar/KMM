using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Service.Contracts;
using Service.Util;

namespace Service.Services
{
    public class SociedadService : ISociedadService
    {
        public IEnumerable<Data.Common.Sociedad> Get()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<Data.Common.Sociedad>("Select * from dbo.Sociedad");
                return result;
            }
        }
    }
}
