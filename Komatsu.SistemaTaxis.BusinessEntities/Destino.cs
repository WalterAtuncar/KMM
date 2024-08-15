using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Destino
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public Nullable<int> IdSolicitud { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<double> LatitudOrigen { get; set; }
        public Nullable<double> LongitudOrigen { get; set; }
        public Nullable<double> LatitudDestino { get; set; }
        public Nullable<double> LongitudDestino { get; set; }
        public Nullable<int> Orden { get; set; }
        public string DireccionOrigen { get; set; }
        public Nullable<int> NumeroDireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public Nullable<int> NumeroDireccionDestino { get; set; }
        public Nullable<double> DistanciaDestinoKilometro { get; set; }
        public Nullable<double> Precio { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
        public string ZonaOrigen { get; set; }
        public string ZonaDestino { get; set; }
        public Nullable<bool> Negociado { get; set; }
    }
}
