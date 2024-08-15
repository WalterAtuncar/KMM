using System;

namespace Modelo.XML.ArchivoPlano
{
    public class ArchivoPlanoServicioDetalle
    {
        public string ColorAutomovil;
        public byte[] FotoAutomovil;
        public int CantidadPasajeros;
        public double Estrellas;
        public byte[] FotoConductor;
        public string ModeloAutomovil;

        public int IdConductor { get; set; }
        public string NombreConductor { get; set; }
        public string DocumentoConductor { get; set; }
        public int IdAutomovil { get; set; }
        public string MarcaAutomovil { get; set; }
        public string PlacaAutomovil { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int IdEstado { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
