using Modelo.General;
using Modelo.Request;
using System.Collections.Generic;

namespace Modelo.Response
{
    public class ServicioResponse
    {
        public int IdServicio { get; set; }
        public int? IdConductor { get; set; }
        public int IdEstado { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public decimal Minutos { get; set; }
        public decimal DistanciaKilometro { get; set; }
        public List<Destino> ListaDestino{ get; set; }
        public List<Cliente> ListaCliente { get; set; }
        public List<Tracking> ListaTracking { get; set; }
        public Conductor Conductor { get; set; }
        public AutoMovil AutoMovil { get; set; }
        public string FechaServicio { get; set; }
        public string MotivoCancelacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public string Mensaje { get; set; }
        public bool Confirmacion { get; set; }
    }
}
