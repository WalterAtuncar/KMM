using Service.Contracts;
using Service.ServiceDTO.Response;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ProveedorGastoWebApiService : IProveedorGastoWebApiService
    {
        string _token = string.Empty;
        bool _flagToken = false;
        string _url = string.Empty;
        ApliClient _service = new ApliClient();

        public ProveedorGastoWebApiService()
        {
            //_token = HttpContext.Current.Session["TokenValueGet"] == null ? string.Empty : HttpContext.Current.Session["TokenValueGet"].ToString();
            _token = "";
            _flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
            _url = ConfigurationManager.AppSettings["servicePrueba"];
            _service = new ApliClient();
        }

        public async Task<OperationService> postPagoAnticipo(List<Anticipo> modelo)
        {
            var route = "api/integracion/postPagoAnticipo";
            var result = await _service.CallService<Anticipo>(_url, route, (int)HttpVerbEnum.HttPost, objectSerialize: modelo, flagLista: true, flagToken: _flagToken, token: _token);
            return result;
        }
    }
}
