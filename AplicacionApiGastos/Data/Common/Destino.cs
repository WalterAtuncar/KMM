using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class Destino
    {
        public int ID { get; set; }
        public int IdSolicitud { get; set; }
        public int IdProveedor { get; set; }
        public decimal LatitudOrigen { get; set; }
        public decimal LongitudOrigen { get; set; }
        public decimal LatitudDestino { get; set; }
        public decimal LongitudDestino { get; set; }
        public int Orden { get; set; }
        public string DireccionOrigen { get; set; }
        public string NumeroDireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public string NumeroDireccionDestino { get; set; }
        public decimal DistanciaDestinoKilometro { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string ZonaOrigen { get; set; }
        public string ZonaDestino { get; set; }
        public bool Negociado { get; set; }
    }
}
