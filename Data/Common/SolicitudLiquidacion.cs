using System;

namespace Data.Common
{
    public class SolicitudLiquidacion
    {
        public int nro { get; set; }
        public int ID { get; set; }
        public bool Sel { get; set; }
        public string CentroCostoBeneficiado { get; set; }
        public string SituacionServicio { get; set; }
        public string CodigoSolicitud { get; set; }
        public int IDProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Observacion { get; set; }
        public int IndicadorNegociado { get; set; }
        public string NombreCentroCostoBeneficiado { get; set; }
        public DateTime FechaServicio { get; set; }
        public string FechaServicioStr { get { return this.FechaServicio.ToString(GlobalFormat.FormatoFechaCorta); } }
        public string HoraServicio { get { return this.FechaServicio.ToString(GlobalFormat.FormatoHora); } }
        public DateTime FechaInicio { get; set; }
        public string HoraInicio { get { return this.FechaInicio.ToString(GlobalFormat.FormatoHora); } }
        public string CentroCostoAfecto { get; set; }
        public string NombreCentroCostoAfecto { get; set; }
        public string Documento { get; set; }
        public string Beneficiado { get; set; }
        public string Aprobador { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public string FechaAprobacionStr { get { return this.FechaAprobacion.ToString(GlobalFormat.FormatoFecha); } }
        public string OrdenServicio { get; set; }
        public string Empresa { get; set; }
        public decimal TotalGeneral { get; set; }
        public string TotalGeneralStr { get { return  string.Format(GlobalFormat.Decimal2,this.TotalGeneral); } }
        public decimal PrecioTarifa { get; set; }
        public string PrecioTarifalStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioTarifa); } }
        public decimal PrecioEspera { get; set; }
        public string PrecioEsperaStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioEspera); ; } }
        public decimal PrecioDesvioRuta { get; set; }
        public string PrecioDesvioRutaStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioDesvioRuta); } }
        public decimal PrecioPeaje { get; set; }
        public string PrecioPeajeStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioPeaje); } }
        public decimal PrecioEstacionamiento { get; set; }
        public string PrecioEstacionamientoStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioEstacionamiento); } }
        public decimal PrecioDesplazamiento { get; set; }
        public string PrecioDesplazamientoStr { get { return string.Format(GlobalFormat.Decimal2, this.PrecioDesplazamiento); } }
    }
}
