using Data.Common;
using Data.Util;
using Service.ServiceDTO.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface ILiquidacionServiceLogic
    {
        Task<OperationResult> Cancelar(Data.Common.Liquidacion liquidacion);
        Task<OperationResult> Liquidar(Data.Common.Liquidacion liquidacion);
        Task<OperationResult> Add(Data.Common.Liquidacion liquidacion);
        Task<List<LiquidacionApi>> GetListByID(int ID);
        Task<List<Data.Common.IndicadorIGV>> GetListIndicadorIGV();
        Task<List<Data.Common.CuentaMayor>> GetListCuentaMayor();
        Task<Data.Common.Liquidacion> GetByID(int id);
        Task<List<Data.Common.ProveedorTaxi>> GetListProveedorTaxi();
        Task<List<SolicitudLiquidacion>> GetListDetalleByLiquidacion(int iD);
        Task<List<LiquidacionExport2>> GetLiquidacionExportByID(int idLiquidacion);
        Task<int> UpdateLiquidacionDetalle(List<SolicitudLiquidacion> listaSolicitud);
        Task<List<SolicitudLiquidacion>> GetListDetalleByIdLiquidacion(int idLiquidacion);
        List<Data.Common.Liquidacion> ListarPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
    }
}
