using System;

namespace Modelo.XML.ArchivoPlano
{
    public class ArchivoPlanoDestino
    {
        public double LatitudOrigen { get; set; }
        public double LongitudOrigen { get; set; }
        public double LatitudDestino { get; set; }
        public double LongitudDestino { get; set; }
        public string ZonaOrigen { get; set; }
        public string ZonaDestino { get; set; }
        public int Orden { get; set; }
        public int IdEstado { get; set; }
        public string DireccionOrigen { get; set; }
        public int NumeroDireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public int NumeroDireccionDestino { get; set; }
        public decimal DistanciaDestinoKilometro { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Negociado { get; set; }
    }
}
