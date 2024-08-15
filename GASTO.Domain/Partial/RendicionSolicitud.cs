using System;
using System.Data;

namespace GASTO.Domain
{
    public partial class RendicionSolicitud
    {
        public enum Query
        {
            None,
            Paginado,
            Recuperar,
            RecuperarDetalle
        }

        public RendicionSolicitud()
        {
        }

        public RendicionSolicitud(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                //case Query.Paginado:
                //    ID = reader.GetSafeString("IdDetRendicionSolicitud");
                //    IsProveedorExt = reader.GetSafeString("IsProveedorExt");
                //    Proveedor = reader.GetSafeString("Proveedor");
                //    NombreCompleto = reader.GetSafeString("NombreCompleto");
                //    FechaStr = reader.GetSafeString("Fecha");
                //    Moneda = reader.GetSafeString("Moneda");
                //    ImporteStr = reader.GetSafeString("Importe");
                //    TotalSolesStr = reader.GetSafeString("TotalSoles");
                //    BaseInafectaStr = reader.GetSafeString("BaseInafecta");
                //    RUC = reader.GetSafeString("RUC");
                //    TipoDocumento = reader.GetSafeString("TipoDocumento");
                //    Impuesto = reader.GetSafeString("Impuesto");
                //    TipoGastoViaje = reader.GetSafeString("TipoGastoViaje");
                //    CECO = reader.GetSafeString("CECO");
                //    OrdenServicio = reader.GetSafeString("OrdenServicio");
                //    NroComprobante = reader.GetSafeString("NroComprobante");
                //    break;

                case Query.RecuperarDetalle:
        
                    EstadoRendicion = reader.GetSafeString("Estado");
                    IdTipo = (int)reader.GetSafeInt32("IdTipo");
                    IdSituacionServicio = (int)reader.GetSafeInt32("IdSituacionServicio");
                    IdEstadoRendicion = reader.GetSafeInt32("IdEstadoRendicion");
                    IdRendicion = (long)reader.GetSafeInt64("IdRendicion");
                    Comentario = reader.GetSafeString("Comentario");
                    break;
                default:
                    break;
            }
        }
    }
}
