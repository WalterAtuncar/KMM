using Service.General;
using System.Collections.Generic;

namespace Service.ServiceDTO
{
    public class TarifaResponse
    {
        public decimal Total { get; set; }
        public decimal DistanciaKilometro { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public string RUCProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public bool FlagMenor { get; set; }
        public string Mensaje { get; set; }
    }
}
