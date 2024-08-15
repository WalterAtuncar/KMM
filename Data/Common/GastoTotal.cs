using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class GastoTotal
    {
        public string CentroCostoAfectoCodigo { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
        public int IdResponsable { get; set; }
        public Boolean FlagCosto { get; set; }
        public Boolean FlagGasto { get; set; }
        public decimal TotalGeneral { get; set; }
    }
}
