using System;

namespace WebApplication.Areas.GASTO.Models
{
    public class SolicitudCambioEstado
    {
        public long IdSolicitud { get; set; }
        public string Codigo { get; set; }
        public int IdTipo { get; set; }
        public int IdSituacionServicio { get; set; }
    }
}