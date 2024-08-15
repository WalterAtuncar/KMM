using Dapper;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASTO.Service.Services
{
    public class UsuarioCajaService : IUsuarioCajaService
    {
        public List<Usuario> Listar()
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionGasto))
            {
                //var result = cn.Query<Usuario>("select ID,Nombres,Apellidos, Apellidos+' '+Nombres ApellidosNombres,CodigoSociedad,NumeroDocumento from Usuario where IdEstado='AC' order by Apellidos,Nombres");
                var result = cn.Query<Usuario>("select ID,Nombres,Apellidos, Apellidos+' '+Nombres ApellidosNombres,CodigoSociedad,NumeroDocumento from Usuario where IdEstado <> 'CE' order by Apellidos,Nombres");
                return result.ToList();
            }
        }
    }

   
}
