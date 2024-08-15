using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class AhorroProveedor
    {
        public string  CentrosCostoAfecto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioMenor { get; set; }
        public decimal PrecioSeleccionado { get; set; }
        public decimal PrecioMayor { get; set; }
    }
}
