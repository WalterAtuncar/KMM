using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionDetalleLiquidar
    {
        public enum Query
        {
            None,
            Liquidacion,
        }

        #region Campos aumentados
        #endregion

        public LiquidacionDetalleLiquidar() { }

        public LiquidacionDetalleLiquidar(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Liquidacion:
                    ID = reader.GetSafeInt32("ID");
                    CentroCostoBeneficiado = reader.GetSafeString("CentroCostoBeneficiado");
                    NombreCentroCostoBeneficiado = reader.GetSafeString("NombreCentroCostoBeneficiado");
                    CentroCostoAfecto = reader.GetSafeString("CentroCostoAfecto");
                    NombreCentroCostoAfecto = reader.GetSafeString("NombreCentroCostoAfecto");
                    Documento = reader.GetSafeString("Documento");
                    Beneficiado = reader.GetSafeString("Beneficiado");
                    Aprobador = reader.GetSafeString("Aprobador");
                    CodigoSolicitud = reader.GetSafeString("CodigoSolicitud");
                    FechaAprobacion = reader.GetSafeDateTime("FechaAprobacion")?.ToString("dd/MM/yyyy");
                    OrdenServicio = reader.GetSafeString("OrdenServicio");
                    FechaServicio = reader.GetSafeDateTime("FechaServicio")?.ToString("dd/MM/yyyy");
                    HoraServicio = reader.GetSafeDateTime("FechaServicio")?.ToString("HH:mm tt");
                    TotalGeneral = string.Format("{0:0,0.00}", reader.GetSafeDouble("TotalGeneral"));
                    PrecioTarifa = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioTarifa"));
                    PrecioEspera = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioEspera"));
                    PrecioDesvioRuta = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioDesvioRuta"));
                    PrecioPeaje = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioPeaje"));
                    PrecioEstacionamiento = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioEstacionamiento"));
                    PrecioDesplazamiento = string.Format("{0:0,0.00}", reader.GetSafeDouble("PrecioDesplazamiento"));
                    break;
                default:
                    break;
            }
        }
    }
}
