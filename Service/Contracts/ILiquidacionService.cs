using Data;
using Data.Common;
using Data.Util;
using Service.ServiceDTO.Request;
using Service.Xml.Transaccion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ILiquidacionService
    {
        IEnumerable<usp_Liquidacion_Listar_Result> List(int? Estado, int? ProveedorTaxiID, DateTime? FechaInicio, DateTime? FechaFin);
        Task<Data.Liquidacion> Find(int Id);
        Task<int> Cancelar(Data.Common.Liquidacion liquidacion);
        Task<OperationResult> Insert(LiquidacionTransaccionXML Liquidacion);
        Task<List<LiquidacionApi>> GetListByID(int ID);
        Task<List<Data.Common.IndicadorIGV>> GetListIndicadorIGV();
        Task<List<Data.Common.CuentaMayor>> GetListCuentaMayor();
        Task<Data.Common.Liquidacion> GetByID(int id);
        Task<List<Data.Common.ProveedorTaxi>> GetListProveedorTaxi();
        Task<int> Liquidar(Data.Common.Liquidacion liquidacion);
        Task<List<SolicitudLiquidacion>> GetListDetalleByLiquidacion(int iD);
        Task<List<LiquidacionExport2>> GetLiquidacionExportByID(int idLiquidacion);
        Task<int> UpdateLiquidacionDetalle(SolicitudLiquidacion liquidacionDetalle);
        Task<List<SolicitudLiquidacion>> GetListDetalleByIdLiquidacion(int idLiquidacion);
        List<Data.Common.Liquidacion> ListarPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
    }
}
