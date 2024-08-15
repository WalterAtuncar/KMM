using System.Collections.Generic;

namespace Modelo.General
{
    public class Tarifa
    {
        public string Proveedor { get; set; }
        public int IdProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public decimal Precio { get; set; }
        public List<Referencia> ListaReferencia { get; set; }
        public decimal Distancia { get; set; }
        public bool FlagMenor { get; set; }
    }
}
