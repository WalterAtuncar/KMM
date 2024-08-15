using Modelo.General;
using System.Collections.Generic;

namespace Modelo.Request
{
    public class TarifaRequest
    {
        public string RUCProveedor { get; set; }
        public string URL { get; set; }
        public string Metodo { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public string Mensaje { get; set; }
        public string Token { get; set; }

    }
}
