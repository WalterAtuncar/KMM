using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class ComparacionMensual
    {
        public string Mes { get; set; }
        public Boolean FlagCosto { get; set; }
        public Boolean FlagGasto { get; set; }
        public decimal TotaGeneral { get; set; }
    }
}
