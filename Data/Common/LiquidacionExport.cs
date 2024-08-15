using System;

namespace Data.Common
{
    public class LiquidacionExport
    {
        public string CentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        
        public string NombreProveedor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string HoraServicio { get { return this.FechaSolicitud.ToString(GlobalFormat.FormatoHora); } }
        public string Empresa { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string CentroCostoAfecto { get; set; }
        public string NombreCentroCostoAfecto { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string OrdenServicio { get; set; }
        public string Observacion { get; set; }
        public decimal Tarifa { get; set; }
        public decimal PrecioEspera { get; set; }
        public decimal PrecioDesvioRuta { get; set; }
        public decimal PrecioPeaje { get; set; }
        public decimal PrecioEstacionamiento { get; set; }
        public decimal TotalGeneral { get; set; }
        public string CorreoProveedor { get; set; }
        public string Codigo { get; set; }
        public int IDProveedor { get; set; }
    }
}
