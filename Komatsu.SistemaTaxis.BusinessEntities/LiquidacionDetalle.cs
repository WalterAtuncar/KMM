using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionDetalle
    {
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> LiquidacionDetalleID { get; set; }
        public Nullable<int> LiquidacionID { get; set; }
        public Nullable<int> SolicitudID { get; set; }
        public string OrdenServicio { get; set; }
        public string CentroCostoAfecto { get; set; }
        public double PrecioTarifa { get; set; }
        public double PrecioEspera { get; set; }
        public double PrecioDesvioRuta { get; set; }
        public double PrecioPeaje { get; set; }
        public double PrecioEstacionamiento { get; set; }
        public double TotalGeneral { get; set; }
        public double PrecioDesplazamiento { get; set; }
    }
}
