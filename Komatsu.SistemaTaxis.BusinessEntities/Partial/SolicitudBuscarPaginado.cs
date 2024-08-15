using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class SolicitudBuscarPaginado
    {
        public enum Query
        {
            None,
            Paginado,
        }

        #region Campos aumentados
        #endregion

        public SolicitudBuscarPaginado() { }

        public SolicitudBuscarPaginado(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Paginado:
                    ID = reader.GetSafeInt32("ID");
                    FechaServicioStr = reader.GetSafeDateTime("FechaServicio")?.ToString("dd/MM/yyyy");
                    CodigoSolicitud = reader.GetSafeString("CodigoSolicitud");
                    SituacionServicioNombre = reader.GetSafeString("SituacionServicio");
                    HoraServicio = reader.GetSafeDateTime("FechaServicio")?.ToString("HH:mm tt");
                    CentroCostoBeneficiado = reader.GetSafeString("CentroCostoBeneficiado");
                    Beneficiado = reader.GetSafeString("Beneficiado");
                    Documento = reader.GetSafeString("Documento");
                    OrdenServicio = reader.GetSafeString("OrdenServicio");
                    CentroCostoAfecto = reader.GetSafeString("CentroCostoAfecto");
                    Aprobador = reader.GetSafeString("Aprobador");
                    TotalGeneralStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("TotalGeneral"));
                    PrecioTarifaStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioTarifa"));
                    PrecioEsperaStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioEspera"));
                    PrecioDesvioRutaStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioDesvioRuta"));
                    PrecioPeajeStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioPeaje"));
                    PrecioEstacionamientoStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioEstacionamiento"));
                    PrecioDesplazamientoStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioDesplazamiento"));
                    HoraInicio = reader.GetSafeDateTime("FechaInicio")?.ToString("HH:mm tt");
                    IndicadorNegociado = reader.GetSafeInt32("IndicadorNegociado");
                    break;
                default:
                    break;
            }
        }
    }
}
