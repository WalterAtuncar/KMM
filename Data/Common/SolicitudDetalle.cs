using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[SolicitudDetalle")]
    public class SolicitudDetalle
    {
        public int IdSolicitud { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string NombreConductor { get; set; }
        public string DocumentoConductor { get; set; }
        public string ModeloAutomovil { get; set; }
        public string PlacaAutomovil { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroStr
        {
            get
            {
                if (this.FechaRegistro == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return this.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss");
            } }
    }
}
