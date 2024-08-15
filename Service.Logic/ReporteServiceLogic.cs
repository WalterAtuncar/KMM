using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Common;
using Service.Contracts;

namespace Service.Logic
{
    public class ReporteServiceLogic : IReporteServiceLogic
    {
        private readonly IReporteService oIReporteService;

        public ReporteServiceLogic(IReporteService oIReporteService)
        {
            this.oIReporteService = oIReporteService;
        }

        public List<ComparacionMensual> GetComparacionMensual(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteService.GetComparacionMensual(idResponsable, idSociedad, fechaInicio, fechaFin);
            return result;
        }

        public List<DetalleGastoTotal> GetDetalleGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteService.GetDetalleGastoTotal(idResponsable, idSociedad, fechaInicio, fechaFin);
            return result;
        }

        public List<GastoTotal> GetGastoTotal(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteService.GetGastoTotal(idResponsable, idSociedad, fechaInicio, fechaFin);
            return result;
        }

        public List<SolicitudLiquidacion> GetListReporteDetallado(string listaCentroCosto, string listaUsuario)
        {
            var result = oIReporteService.GetListReporteDetallado(listaCentroCosto, listaUsuario);
            return result;
        }

        public List<PenalidadTiempoEspera> GetPenalidadTiempoEspera(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteService.GetPenalidadTiempoEspera(idResponsable, idSociedad, fechaInicio, fechaFin);
            return result;
        }

        public List<Data.Common.AhorroProveedor> GetListAhorroProveedor(int? idResponsable, int? idSociedad, string fechaInicio, string fechaFin)
        {
            var result = oIReporteService.GetListAhorroProveedor(idResponsable, idSociedad, fechaInicio, fechaFin);
            return result;
        }

        public List<Data.Common.ReporteAuditoria> GetListReporteAuditoria(string IdSolicitud,string fechaInicio, string fechaFin , string sociedad)
        {
            var result = oIReporteService.GetListReporteAuditoria(IdSolicitud, fechaInicio, fechaFin , sociedad);
            return result;
        }


        public List<Data.Common.ReporteRepresentacion> GetListReporteRepresentacion()
        {
            var result = oIReporteService.GetListReporteRepresentacion();
            return result;
        }


        public List<Data.Common.ReporteRepresentacion> GetListReporteDetallado(int anio)
        {
            var result = oIReporteService.GetListReporteDetallado(anio);
            return result;
        }

    }
}
