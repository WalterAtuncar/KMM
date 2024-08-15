using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IReporteService
    {
        List<Data.Common.GastoTotal> GetGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin);
        List<Data.Common.DetalleGastoTotal> GetDetalleGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin);
        List<Data.Common.PenalidadTiempoEspera> GetPenalidadTiempoEspera(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin);
        List<Data.Common.ComparacionMensual> GetComparacionMensual(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin);
        List<Data.Common.SolicitudLiquidacion> GetListReporteDetallado(string listaCentroCosto, string listaUsuario);
        List<Data.Common.AhorroProveedor> GetListAhorroProveedor(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin);
        List<Data.Common.ReporteAuditoria> GetListReporteAuditoria(string idSolicitud, string fechaInicio, string fechaFin , string sociedad);
        List<Data.Common.ReporteRepresentacion> GetListReporteRepresentacion();
        List<Data.Common.ReporteRepresentacion> GetListReporteDetallado(int anio);
    }
}
