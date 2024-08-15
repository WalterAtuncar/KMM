using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class ReporteAuditoria
    {
        public string CodSolicitud { get; set; }

        public string Sociedad { get; set; }

        public string Tipo { get; set; }

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

        public string CodigoLiquidacion { get; set; }

        public string Comentario { get; set; }

        public DateTime FechaAprobacion { get; set; }

        public DateTime FechaAccion { get; set; }

        public DateTime FechaRetorno { get; set; }

        public DateTime FechaSalida { get; set; }

        public string OrdenServicio { get; set; }

        public string Importe { get; set; }

        public string NumeroCeco { get; set; }

        public string DescripcionCeco { get; set; }
    }
}
