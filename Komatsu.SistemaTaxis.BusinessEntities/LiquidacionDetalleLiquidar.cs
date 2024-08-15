using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionDetalleLiquidar
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/06/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; } 
        public string CentroCostoBeneficiado { get; set; }
        public string NombreCentroCostoBeneficiado { get; set; }
        public string CentroCostoAfecto { get; set; }
        public string NombreCentroCostoAfecto { get; set; }
        public string Documento { get; set; }
        public string Beneficiado { get; set; }
        public string Aprobador { get; set; }
        public string CodigoSolicitud { get; set; }
        public string FechaAprobacion { get; set; }
        public string OrdenServicio { get; set; }
        public string FechaServicio { get; set; }
        public string HoraServicio { get; set; }
        public string TotalGeneral { get; set; }
        public string PrecioTarifa { get; set; }
        public string PrecioEspera { get; set; }
        public string PrecioDesvioRuta { get; set; }
        public string PrecioPeaje { get; set; }
        public string PrecioEstacionamiento { get; set; }
        public string PrecioDesplazamiento { get; set; }
    }
}
