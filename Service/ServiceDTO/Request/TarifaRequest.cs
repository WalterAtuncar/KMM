
using Service.General;
using System.Collections.Generic;

namespace Data.ServiceDTO
{
    public class TarifaRequest
    {
        public string RUCProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public List<Service.General.Destino> ListaDestino { get; set; }
    }
}
