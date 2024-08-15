using System;

namespace GASTO.Domain
{
    public partial class RendicionSolicitud
    {
        public long IdRendicion { get; set; }
        public long IdSolicitud { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<long> IdEstadoRendicion { get; set; }
        public string EstadoRendicion { get; set; }
        public string Comentario { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public Nullable<long> IdUsuarioM { get; set; }
        public Nullable<System.DateTime> FechaCambioEst { get; set; }
        public Nullable<long> IdUsuarioCambioEst { get; set; }
        public string ComentarioRechazo { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public int Envia { get; set; }
        public int IdTipo { get; set; }
        public int IdSituacionServicio { get; set; }
        public string CodigoLiquidacion { get; set; }

    }
}
