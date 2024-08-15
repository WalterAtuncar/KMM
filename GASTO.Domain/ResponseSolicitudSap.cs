using System.Collections.Generic;

namespace GASTO.Domain
{
    public class ResponseSolicitudSap
    {
        public string Codigo { get; set; }
        public string TipoSolicitud { get; set; }
        public string MensajeSolicitud { get; set; }
        public string CabeceraOk { get; set; }
        public List<ResponseSolicitudRendicionSap> listaResponseSolicitudRendicionSap { get; set; }
    }
}
