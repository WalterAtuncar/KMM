using Modelo.Request;
using Modelo.Response;
using Modelo.Util;
using Servicio.Contract;
using Servicio.Util;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Servicio
{
    public class TarifaData : BaseConexion , ITarifaData
    {
        ApliClient service;
        bool flagToken = false;
        public TarifaData()
        {
            service = new ApliClient();
            flagToken = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["flagToken"]));
        }

        public async Task<OperationService> GetTarifa(TarifaRequest request)
        {
            var result = await service.CallService<TarifaResponse>(request.URL, request.Metodo, (int)HttpVerbEnum.HttPost, objectSerialize: request,flagToken : flagToken,token : request.Token );
            return result;
        }
       
    }
}
