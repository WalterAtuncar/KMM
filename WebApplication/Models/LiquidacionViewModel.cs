using Data.Common;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class LiquidacionViewModel
    {
        public Data.Common.Liquidacion Liquidacion { get; set; }
        public Data.Common.ProveedorTaxi ProveedorTaxi { get; set; }
        public string InputList { get; set; }
        public SolicitudLiquidacion Solicitud { get; set; }
        public List<Data.Common.Sociedad> ListaSociedad { get; internal set; }
        public List<DocumentoLiquidacion> ListaDocumentoLiquidacion { get; set; }
        public List<IndicadorLiquidacion> ListaIndicadorLiquidacion { get; set; }
        public List<IndicadorIGV> ListaIndicadorIGV { get; set; }
        public List<CuentaMayor> ListaCuentaMayor { get; set; }
        public List<ProveedorTaxi> ListaProveedor { get; set; }
        public List<Data.Common.Destino> ListaDestino { get; set; }
    }
}