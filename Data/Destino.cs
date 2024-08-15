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
    using System.Collections.Generic;
    
    public partial class Destino
    {
        public int ID { get; set; }
        public Nullable<int> IdSolicitud { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<decimal> LatitudOrigen { get; set; }
        public Nullable<decimal> LongitudOrigen { get; set; }
        public Nullable<decimal> LatitudDestino { get; set; }
        public Nullable<decimal> LongitudDestino { get; set; }
        public Nullable<int> Orden { get; set; }
        public string DireccionOrigen { get; set; }
        public string NumeroDireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public string NumeroDireccionDestino { get; set; }
        public Nullable<int> DistanciaDestinoKilometro { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public string ZonaOrigen { get; set; }
        public string ZonaDestino { get; set; }
        public Nullable<bool> Negociado { get; set; }
    
        public virtual ProveedorTaxi ProveedorTaxi { get; set; }
    }
}
