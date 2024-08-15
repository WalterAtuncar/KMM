using GASTO.Domain;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Contracts
{
    public interface IProveedorGastoWebApiService
    {
        Task<OperationService> GetOrdenServicio(string ordenServicio);
        Task<OperationService> GetCuentasBancarias(string socidedad,string codigo);
        Task<OperationService> PostEnviarAnticipo(List<AnticipoApi> modelo);
        Task<OperationService> PostTipoCambio(TipoCambioApi modelo);
        Task<OperationService> GetDatosProveedor(string sociedad,string documento);
        Task<OperationService> PostEnviarLiquidacion(List<LiquidacionApi> modelo);
        Task<OperationService> PostEnviarCaja(List<CajaApi> modelo);
        Task<OperationService> PostEnviarTarjeta(List<TarjetaApi> modelo);
        Task<OperationService> EmailService(Correo_GV modelo);

    }
}
