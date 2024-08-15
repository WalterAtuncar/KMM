using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionGrabar
    {
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<DateTime> Fecha { get; set; }
        public Nullable<int> ProveedorTaxiID { get; set; }
        public string Total { get; set; }
        public Nullable<int> SociedadID { get; set; }
        public string Observacion { get; set; }
        public string Moneda { get; set; }
        public string Lugar_Comer { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public Nullable<int> Estado { get; set; }
        public List<LiquidacionDetalle> LiquidacionDetalleLis { get; set; }
    }
}
