using Modelo.General;
using System.Collections.Generic;

namespace Modelo.Request
{
    public class PushRequest
    {
        public int IdServicio{ get; set; }
        public int IdEstado { get; set; }
        public string RUCProveedor { get; set; }
        public Conductor Conductor { get; set; }
        public AutoMovil AutoMovil { get; set; }
        public ServicioDetalle ServicioDetalle { get; set; }
        public List<GastoAdicional> ListaGastoAdicional { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public List<Tracking> ListaTracking { get; set; }
    }
}
