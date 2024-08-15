namespace Modelo.Request
{
    public class ServicioDetalle
    {
        public int IdEstado { get; set; }
        public string Observacion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
