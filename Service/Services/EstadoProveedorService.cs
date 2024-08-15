using Dapper;
using Data;
using Service.Contracts;
using Service.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Service.Services
{
    public class EstadoProveedorService: IEstadoProveedorService
    {
        public List<EstadoServicio> GetList()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var result = cn.Query<EstadoServicio>("SELECT * FROM dbo.EstadoServicio").ToList();

                return result;
            }
        }
    }
}
