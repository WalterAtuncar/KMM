using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class PenalidadTiempoEspera
    {
        public string CentroCostoAfectoDescripcion { get; set; }
        public int IdResponsable { get; set; }
        public decimal PrecioEspera { get; set; }
        public decimal PrecioDesvioRuta { get; set; }
        public decimal PrecioPeaje { get; set; }
        public decimal PrecioEstacionamiento { get; set; }
        public decimal PrecioDesplazamiento { get; set; }
    }
}
