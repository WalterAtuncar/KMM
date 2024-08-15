namespace Modelo.Request
{
    public class Coordenada
    {
        public double OrigenLatitud { get; set; }
        public double OrigenLongitud { get; set; }
        public double DestinoLatitud { get; set; }
        public double DestinoLongitud { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
