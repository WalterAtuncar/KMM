using System;
using System.Data;

namespace GASTO.Domain
{
    public partial class DetRendicionSolicitud
    {
        public enum Query
        {
            None,
            Paginado,
            Recuperar,
            RecuperarDetalle
        }

        public DetRendicionSolicitud()
        {
        }

        public DetRendicionSolicitud(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Paginado:
                    ID = reader.GetSafeString("IdDetRendicionSolicitud");
                    IdRendicionSolicitud=(long) reader.GetSafeInt64("IdRendicionSolicitud");
                    IsProveedorExt = reader.GetSafeString("IsProveedorExt");
                    Proveedor = reader.GetSafeString("Proveedor");
                    RazonSocial = reader.GetSafeString("Proveedor");
                    NombreCompleto = reader.GetSafeString("NombreCompleto") == null ? "" : reader.GetSafeString("NombreCompleto");
                    FechaStr = reader.GetSafeDateTime("Fecha") == null ? "" : reader.GetSafeDateTime("Fecha")?.ToString("dd/MM/yyyy");
                    Moneda = reader.GetSafeString("Moneda");
                    ImporteStr = reader.GetSafeString("Importe");
                    TotalSolesStr = reader.GetSafeString("TotalSoles");
                    BaseInafectaStr = reader.GetSafeString("BaseInafecta");
                    RUC = reader.GetSafeString("RUC");
                    TipoDocumento = reader.GetSafeString("TipoDocumento");
                    Impuesto = reader.GetSafeString("Impuesto");
                    TipoGastoViaje = reader.GetSafeString("TipoGastoViaje");
                    CECO = reader.GetSafeString("CECO") == null ? "" : reader.GetSafeString("CECO");
                    OrdenServicio = reader.GetSafeString("OrdenServicio") == null ? "" : reader.GetSafeString("OrdenServicio");
                    NroComprobante = reader.GetSafeString("NroComprobante");
                    CodigoLiquidacion = reader.GetSafeString("CodigoLiquidacionDetalle");
                    CORREL = reader.GetSafeString("CORREL");
                    NombreFile = reader.GetSafeString("NombreFile") == null ? "" : reader.GetSafeString("NombreFile");
                    RutaCompletaFile = reader.GetSafeString("RutaCompletaFile") == null ? "" : reader.GetSafeString("RutaCompletaFile");
                    break;

                case Query.RecuperarDetalle:
        
                    IdDetRendicionSolicitud = (long)reader.GetSafeInt64("IdDetRendicionSolicitud");
                    TipoStr = reader.GetSafeString("TipoStr");
                    Tipo = (bool)reader.GetSafeBoolean("Tipo");
                    RUC = reader.GetSafeString("RUC");
                    IdTipoDocumento = (long)reader.GetSafeInt64("IdTipoDocumento");
                    IdTipoGastoViaje = (long)reader.GetSafeInt64("IdTipoGastoViaje");
                    FechaStr = reader.GetSafeDateTime("Fecha")?.ToString("dd/MM/yyyy");
                    OrdenServicio = reader.GetSafeString("OrdenServicio") == null ? "" : reader.GetSafeString("OrdenServicio");
                    RUT = reader.GetSafeString("RUT");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CentroCostoAfectoCodigoSap") == null ? "" : reader.GetSafeString("CentroCostoAfectoCodigoSap");
                    NroComprobante = reader.GetSafeString("NroComprobante");
                    Tratamiento = reader.GetSafeString("Tratamiento");
                    Nombre1 = reader.GetSafeString("Proveedor");
                    Calle = reader.GetSafeString("Calle") == null ? "" : reader.GetSafeString("Calle");
                    Poblacion = reader.GetSafeString("Poblacion") == null ? "" : reader.GetSafeString("Poblacion");
                    Nombre2 = reader.GetSafeString("Nombre2") == null ? "" : reader.GetSafeString("Nombre2");
                    Nombre3 = reader.GetSafeString("Nombre3") == null ? "" : reader.GetSafeString("Nombre3");
                    Nombre4 = reader.GetSafeString("Nombre4") == null ? "" : reader.GetSafeString("Nombre4");
                    Pais = reader.GetSafeString("Pais");
                    NombreFile = reader.GetSafeString("NombreFile");
                    RutaCompletaFile = reader.GetSafeString("RutaCompletaFile");
                    GlosaDetalle = reader.GetSafeString("GlosaDetalle") == null ? "" : reader.GetSafeString("GlosaDetalle");
                    IdMoneda = (int)reader.GetSafeInt32("IdMoneda");
                    CodAbreviatura = reader.GetSafeString("CodAbreviatura");
                    Importe = (double)reader.GetSafeDecimal("Importe");
                    BaseInafecta = (double)reader.GetSafeDecimal("BaseInafecta");
                    ImporteStr = string.Format("{0:0,0.00}", reader.GetSafeDecimal("Importe"));
                    BaseInafectaStr = string.Format("{0:0,0.00}", reader.GetSafeDecimal("BaseInafecta"));
                    IdImpuesto = (long)reader.GetSafeInt64("IdImpuesto");
                    Proveedor= reader.GetSafeString("Proveedor");
                    RazonSocial = reader.GetSafeString("Proveedor");
                    Externo= (bool)reader.GetSafeBoolean("Externo");
                    Clase = reader.GetSafeString("Clase");
                    CodigoGastoViaje = reader.GetSafeString("CodigoGastoViaje");
                    CuentaContable = reader.GetSafeString("CuentaContable");
                    Impuesto = reader.GetSafeString("Impuesto");
                    CORREL = reader.GetSafeString("CORREL");
                    TipoCambio = (decimal)reader.GetSafeDecimal("TipoCambio");
                    CodigoSAPProveedor = reader.GetSafeString("CodigoSAPProveedor") == null ? "" : reader.GetSafeString("CodigoSAPProveedor");
                    NombreGR = reader.GetSafeString("NombreGR") == null ? "" : reader.GetSafeString("NombreGR");
                    RUCGR = reader.GetSafeString("RUCGR") == null ? "" : reader.GetSafeString("RUCGR");
                    ObjetivoGR = reader.GetSafeString("ObjetivoGR") == null ? "" : reader.GetSafeString("ObjetivoGR");
                    DetalleGR = reader.GetSafeString("DetalleGR") == null ? "" : reader.GetSafeString("DetalleGR");
                    TipoDocumento = reader.GetSafeString("TipoDocumento") == null ? "" : reader.GetSafeString("TipoDocumento");
                    break;
                default:
                    break;
            }
        }
    }
}
