using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class SolicitudProveedorTaxi
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public Nullable<int> SolicitudID { get; set; }
        public Nullable<int> ProveedorTaxiID { get; set; }
        public Nullable<double> PrecioServicio { get; set; }
        public string Seleccionado { get; set; }
    }
}
