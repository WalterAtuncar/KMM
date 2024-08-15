using Data;
using System.Collections.Generic;
using Data.Common;

namespace WebApplication.Models
{
    public class ProveedorTaxiModel
    {
        public Data.Common.ProveedorTaxi Proveedor { get; set; }
        public List<EstadoServicio> ListaEstadoProveedor { get; set; }
        public List<usp_ServicioProveedorTaxi_List_Result> ListaMetodos { get; set; }
        public Data.CredencialesProveedorTaxi Credenciales { get; set; }
        public List<Data.Common.IndicadorIGV> ListaIndicadorIGVFactura { get; set; }
        public List<Data.Common.IndicadorIGV> ListaIndicadorIGVProvision { get; set; }
    }
}