using System;
using System.Collections.Generic;
using System.Data;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Liquidacion
    {
        public enum Query
        {
            None,
            List
        }

        #region Campos aumentados
        public Nullable<Int64> RowNumber { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public string RazonSocial { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string EstadoNombre { get; set; }
        public string CuentaMayor { get; set; }
        public List<LiquidacionDetalle> LiquidacionDetalleLis { get; set; }
        #endregion

        public Liquidacion() { }

        public Liquidacion(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.None:
                    break;
                case Query.List:
                    ID = reader.GetSafeInt32("ID");
                    RowNumber = reader.GetSafeInt64("RowNumber");
                    Codigo = reader.GetSafeString("Codigo");
                    Fecha = reader.GetSafeDateTime("Fecha");
                    ProveedorTaxiID = reader.GetSafeInt32("ProveedorTaxiID");
                    RazonSocial = reader.GetSafeString("RazonSocial");
                    Total = reader.GetSafeString("Total");
                    Estado = reader.GetSafeInt32("Estado");
                    EstadoNombre = reader.GetSafeString("EstadoNombre");
                    AsientoContable = reader.GetSafeString("AsientoContable");
                    Ejercicio = reader.GetSafeString("Ejercicio");
                    DiaRegistro = reader.GetSafeString("DiaRegistro");
                    HoraRegistro = reader.GetSafeString("HoraRegistro");
                    HoraServicio = reader.GetSafeString("HoraServicio");
                    FechaFactura = reader.GetSafeDateTime("FechaFactura");
                    CuentaMayor = reader.GetSafeString("CuentaMayor");
                    RutaCompletaFile = reader.GetSafeString("RutaCompletaFile");
                    NombreFile = reader.GetSafeString("NombreFile");
                    break;
                default:
                    break;
            }
        }
    }
}
