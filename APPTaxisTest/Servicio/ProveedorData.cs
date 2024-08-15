using Common.Util;
using Dapper;
using Modelo.General;
using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Servicio.Contract;
using Servicio.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio
{
    public class ProveedorData :  IProveedorData
    { 
        ApliClient service;
        bool flagToken = false;
        //Utilitario utilitario;

        public ProveedorData()
        {
            service = new ApliClient();
            flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            //utilitario = new Utilitario();
        }

        

        public async Task<Proveedor> GetDatoServicio(string RUC, int idMetodoProveedor)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RUCProveedor", RUC);
                parameters.Add("@IdMetodoProveedorTaxi", idMetodoProveedor);

                return await cn.QueryFirstOrDefaultAsync<Proveedor>("dbo.usp_ServicioProveedorTaxi_By_Proveedor", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Proveedor>> GetListProveedor()
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var lista = await cn.QueryAsync<Proveedor>("dbo.usp_ProveedorTaxi_Activo", commandType: CommandType.StoredProcedure);
                return lista.ToList();
            }
        }

       

    }
}
