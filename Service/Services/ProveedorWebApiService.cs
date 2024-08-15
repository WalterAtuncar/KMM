using Dapper;
using Data.ServiceDTO;
using Service.Contracts;
using Service.ServiceDTO;
using Service.ServiceDTO.Request;
using Service.ServiceDTO.Response;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace Service.Services
{
    public class ProveedorWebApiService : IProveedorWebApiService
    {
        string _token = string.Empty;
        bool _flagToken = false;
        string _url = string.Empty;
        ApliClient _service = new ApliClient();
        public ProveedorWebApiService()
        {
            _token = HttpContext.Current.Session["TokenValueGet"] == null ? string.Empty : HttpContext.Current.Session["TokenValueGet"].ToString();
            _flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            _url = ConfigurationManager.AppSettings["servicePrueba"];
            _service = new ApliClient();
        }

        public async Task<OperationService> EmailService(Data.Common.Correo modelo)
        {
            var route = "api/servicio/emailService";
            var result = await _service.CallService<OperationService>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: false, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> GetListTarifa(TarifaRequest modelo)
       {
            var route = "api/tarifa/getListTarifa";
            var result = await _service.CallService<TarifaResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo,flagLista:true,flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> PostLiquidación(List<LiquidacionApi> modelo)
        {
            var route = "api/integracion/postLiquidacion";
            var result = await _service.CallService<LiquidacionApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo,flagLista:true, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> GetOrdenServicio(string ordenServicio)
        {
            var route = "api/integracion/getOrdenServicio";
            var parameters = new List<string>();
            parameters.Add($"?norden={ordenServicio}");

            var result = await _service.CallService<OrdenServicioApi>(_url, route, (int)HttpVerbEnum.HttGet, parameters: parameters,flagLista:true,flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> GetOrdenServicioBatch(List<OrdenServicioBatch> modelo)
        {
            var route = "api/integracion/getOrdenServicioBatch";
            var result = await _service.CallService<OrdenServicioBatch>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> SaveService(ServicioRequest modelo)
        {
            var route = "api/servicio/saveService";
            var result = await _service.CallService<ServicioResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> AnularService(ServicioRequest modelo)
        {
            var route = "api/servicio/anularService";
            var result = await _service.CallService<OperationResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> CancelService(ServicioRequest modelo)
        {
            var route = "api/servicio/cancelService";
            var result = await _service.CallService<OperationResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> GetService(ServicioRequest modelo)
        {
            var route = "api/servicio/getInfoService";
            var result = await _service.CallService<ServicioResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> GetState(ServicioRequest modelo)
        {
            var route = "api/servicio/getStateService";
            var result = await _service.CallService<ServicioResponse>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagToken: _flagToken, token: _token);
            return result;
        }
        public async Task<OperationService> GetListServices(string tipoDocumentoCliente, string numeroDocumentoCliente, string ruc)
        {
            string tipoDocumento = "02";
            string numeroDocumento = "10727290686";
            string RUC = "10727290686";

            var route = "api/servicio/getListService";

            var parameters = new List<string>();

            parameters.Add($"tipoDocumentoCliente={tipoDocumento}");
            parameters.Add($"numeroDocumentoCliente={numeroDocumento}");
            parameters.Add($"ruc={RUC}");

            var result = await _service.CallService<List<ServicioResponse>>(_url, route, (int)HttpVerbEnum.HttGet, parameters);
            return result;
        }

        public async Task<int> UpdateStateService(int? idServicio, int idEstadoServicio)
        {
            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdServicio", idServicio, dbType: DbType.Int32);
                parameters.Add("@IdEstadoServicio", idEstadoServicio, dbType: DbType.Int32);

                var result = await cn.ExecuteAsync("dbo.usp_Upd_Servicio_State", parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
