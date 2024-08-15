using System;

namespace GASTO.Domain
{
    public partial class HistorialLog
    {
       

        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string Estado { get; set; }
        public string FechaRegistroStr { get; set; }
        public string HoraStr { get; set; }
        public string Codigo { get; set; }
        public string CodigoGenerado { get; set; }
       

    }
}
