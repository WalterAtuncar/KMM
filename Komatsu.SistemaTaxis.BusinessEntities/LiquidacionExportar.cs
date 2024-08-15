using System;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionExportar
    {
        public string CentroCosto { get; set; }
        public string CentroCostoBeneficiado { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCentroCostoBeneficiado { get; set; }
        public string Codigo { get; set; }
        public string CodigoSolicitud { get; set; }
        public Nullable<int> IDProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string HoraServicio { get; set; }
        public Nullable<DateTime> FechaSolicitud { get; set; }
        public Nullable<DateTime> FechaServicio { get; set; }
        public string Empresa { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Beneficiado { get; set; }
        public string CentroCostoAfecto { get; set; }
        public string NombreCentroCostoAfecto { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string OrdenServicio { get; set; }
        public string Observacion { get; set; }
        public Nullable<double> Tarifa { get; set; }
        public Nullable<double> PrecioTarifa { get; set; }
        public Nullable<double> PrecioEspera { get; set; }
        public Nullable<double> PrecioDesvioRuta { get; set; }
        public Nullable<double> PrecioPeaje { get; set; }
        public Nullable<double> PrecioEstacionamiento { get; set; }
        public Nullable<double> PrecioDesplazamiento { get; set; }
        public Nullable<double> TotalGeneral { get; set; }
        public string CorreoProveedor { get; set; }
    }
}
