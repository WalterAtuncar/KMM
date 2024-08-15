using System.Data;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionExportar
    {
        public enum Query
        {
            None,
            ExportByID,
        }

        #region Campos aumentados
        #endregion

        public LiquidacionExportar() { }

        public LiquidacionExportar(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.ExportByID:
                    CentroCosto = reader.GetSafeString("CentroCosto");
                    CentroCostoBeneficiado = reader.GetSafeString("CentroCostoBeneficiado");
                    NombreCentroCosto = reader.GetSafeString("NombreCentroCosto");
                    NombreCentroCostoBeneficiado = reader.GetSafeString("NombreCentroCostoBeneficiado");                  
                    Codigo = reader.GetSafeString("Codigo");
                    CodigoSolicitud = reader.GetSafeString("Codigo");
                    IDProveedor = reader.GetSafeInt32("IDProveedor");
                    NombreProveedor = reader.GetSafeString("NombreProveedor");
                    FechaSolicitud = reader.GetSafeDateTime("FechaSolicitud");
                    FechaServicio = reader.GetSafeDateTime("FechaServicio");
                    Empresa = reader.GetSafeString("Empresa");
                    Documento = reader.GetSafeString("Documento");
                    Nombre = reader.GetSafeString("Nombre");
                    CentroCostoAfecto = reader.GetSafeString("CentroCostoAfecto");
                    NombreCentroCostoAfecto = reader.GetSafeString("NombreCentroCostoAfecto");
                    Origen = reader.GetSafeString("Origen");
                    Destino = reader.GetSafeString("Destino");
                    OrdenServicio = reader.GetSafeString("OrdenServicio");
                    Observacion = reader.GetSafeString("Observacion");
                    Tarifa = reader.GetSafeDouble("Tarifa");
                    PrecioTarifa = reader.GetSafeDouble("Tarifa");
                    PrecioEspera = reader.GetSafeDouble("PrecioEspera");
                    PrecioDesvioRuta = reader.GetSafeDouble("PrecioDesvioRuta");
                    PrecioPeaje = reader.GetSafeDouble("PrecioPeaje");
                    PrecioEstacionamiento = reader.GetSafeDouble("PrecioEstacionamiento");
                    PrecioDesplazamiento = reader.GetSafeDouble("PrecioDesplazamiento");
                    TotalGeneral = reader.GetSafeDouble("TotalGeneral");
                    CorreoProveedor = reader.GetSafeString("CorreoProveedor");
                    //HoraServicio = reader.GetSafeString("HoraServicio");
                    HoraServicio = reader.GetSafeString("FechaServicio");
                    Beneficiado = reader.GetSafeString("Beneficiado");
                    break;
                default:
                    break;
            }
        }
    }
}
