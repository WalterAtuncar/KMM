//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    
    public partial class usp_Solicitud_GetByLiquidacionID_Result
    {
        public Nullable<long> RowNumber { get; set; }
        public int ID { get; set; }
        public Nullable<int> IdTipoPago { get; set; }
        public string TipoPagoNombre { get; set; }
        public Nullable<int> IdProveedorTaxi { get; set; }
        public string RazonSocial { get; set; }
        public string FechaServicio { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
        public string NroOrdenServicio { get; set; }
        public Nullable<decimal> TotalGeneral { get; set; }
    }
}
