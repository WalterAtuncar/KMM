using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ReporteAuditoriaViewModel
    {
        public string CodSolicitud { get; set; }
        public string Sociedad { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocUsuarioRegistro { get; set; }
        public string UsuarioAprobador { get; set; }
        public string DocUsuarioAprobador { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string AsientoContable { get; set; }
        public DateTime FechaRegistroHE { get; set; }
        public string UsuarioRegistroHE { get; set; }
        public string EstadoHE { get; set; }
    }
}