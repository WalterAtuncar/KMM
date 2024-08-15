using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.ServiceDTO.Response;
using GASTO.Service.Util;

namespace GASTO.Service.Services
{
    public class ProveedorGastoWebApiService : IProveedorGastoWebApiService
    {
        string _token = string.Empty;
        bool _flagToken = false;
        string _url = string.Empty;
        //INI EDIAZ 22.04.2020, se agrega otra url para usuario de gasto de sap
        string _urlGasto = string.Empty;
        //FIN EDIAZ 22.04.2020
        ApliClient _service = new ApliClient();
        public ProveedorGastoWebApiService()
        {
            _token = HttpContext.Current.Session["TokenValueGet"] == null ? string.Empty : HttpContext.Current.Session["TokenValueGet"].ToString();
            _flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            _url = ConfigurationManager.AppSettings["servicePrueba"];
            _urlGasto = ConfigurationManager.AppSettings["serviceGasto"];
            _service = new ApliClient();
        }

        public async Task<OperationService> GetOrdenServicio(string ordenServicio)
        {
            var route = "api/integracion/getOrdenServicio";
            var parameters = new List<string>();
            parameters.Add($"?norden={ordenServicio}");
            var result = await _service.CallService<OrdenServicioApi>(_url, route, (int)HttpVerbEnum.HttGet, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> GetCuentasBancarias(string socidedad, string codigo)
        {
            var route = "api/integracion/getCuentasBancarias";
            var parameters = new List<string>();
            parameters.Add($"?sociedad={socidedad}");
            parameters.Add($"codigo={codigo}");
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<CuentaBancariaApi>(_urlGasto, route, (int)HttpVerbEnum.HttGet, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<CuentaBancariaApi>(_url, route, (int)HttpVerbEnum.HttGet, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> PostEnviarAnticipo(List<AnticipoApi> modelo)
        {
            var route = "api/integracion/postEnviarAnticipo";
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<AnticipoApi>(_urlGasto, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<AnticipoApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> PostTipoCambio(TipoCambioApi modelo)
        {
            var route = "api/integracion/postTipoCambio";
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<TipoCambioApi>(_urlGasto, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<TipoCambioApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> GetDatosProveedor(string sociedad,string documento)
        {
            var route = "api/integracion/getDatosProveedor";
            var parameters = new List<string>();
            parameters.Add($"?documento={documento}&sociedad={sociedad}");
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<ProveedorResponse>(_urlGasto, route, (int)HttpVerbEnum.HttGet, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<ProveedorResponse>(_url, route, (int)HttpVerbEnum.HttGet, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> PostEnviarLiquidacion(List<LiquidacionApi> modelo)
        {
            var route = "api/integracion/postEnviarLiquidacion";
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<LiquidacionApi>(_urlGasto, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<LiquidacionApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> PostEnviarCaja(List<CajaApi> modelo)
        {
            var route = "api/integracion/postEnviarCaja";
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<CajaApi>(_urlGasto, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<CajaApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> PostEnviarTarjeta(List<TarjetaApi> modelo)
        {
            var route = "api/integracion/postEnviarTarjeta";
            //EDIAZ 22.04.2020, se cambia por url de web api de gasto
            var result = await _service.CallService<TarjetaApi>(_urlGasto, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            //var result = await _service.CallService<TarjetaApi>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }

        public async Task<OperationService> EmailService(Correo_GV modelo)
        {
            var route = "api/servicio/emailService_GV";
            var result = await _service.CallService<OperationService>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: false, flagToken: _flagToken, token: _token);
            return result;
        }

        //public async Task<OperationService> getEnviarAnticipo()
        //{
        //    var route = "api/integracion/getEnviarAnticipo";
        //    var parameters = new List<string>();
        //    //parameters.Add($"?sociedad={socidedad}");
        //    //parameters.Add($"codigo={codigo}");
        //    var result = await _service.CallService<AnticipoApi>(_url, route, (int)HttpVerbEnum.HttPost, parameters: parameters, flagLista: true, flagToken: _flagToken, token: _token);
        //    return result;
        //}

    }
}
