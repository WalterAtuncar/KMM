using System;
using System.Collections.Generic;

namespace Data.Common
{
    public class Liquidacion
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public string NombreFile { get; set; }
        public string RutaCompletaFile { get; set; }
        public string NombreProveedor { get; set; }
        public List<Adjunto> ListaAdjunto { get; set; }
        public string CorreoProveedor { get; set; }
        public string CorreoReferencia { get; set; }
        public int ProveedorTaxiID { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public string EstadoNombre { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameCancelo { get; set; }
        public string UsuarioCancelo { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaPaginadoStr
        {
            get
            {
                if (this.Fecha == DateTime.MinValue)
                {
                    return string.Empty;
                }
                else
                {
                    return this.Fecha.ToString(GlobalFormat.FormatoFechaCorta);
                }
            }
        }
        public string FechaStr{ get; set;}
        public DateTime FechaLiquidacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModifica { get; set; }
        public int SociedadID { get; set; }
        public string Observacion { get; set; }
        public string AsientoContable { get; set; }
        public string Ejercicio { get; set; }
        public string DiaRegistro { get; set; }
        public string UsuarioRegistroSAP { get; set; }
        public string HoraRegistro { get; set; }
        public string Mensaje { get; set; }
        public string FacturaProvision { get; set; }
        public string UsuarioModifica { get; set; }
        public string UserNameRegistro { get; set; }
        public int IdSolicitud { get; set; }
        public string CodigoDocumento { get; set; }
        public string UsuarioLiquido { get; set; }
        public string UserNameLiquido { get; set; }
        public string CodigoSociedad { get; set; }
        public string CodigoSapProveedor { get; set; }
        public string CodigoIndicador { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime FechaContable { get; set; }
        public string FechaFacturaStr { get; set; }
        public string FechaFacturaPaginadoStr {
            get
            {
                if (this.FechaFactura == DateTime.MinValue)
                {
                    return string.Empty;
                }
                else
                {
                    return this.FechaFactura.ToString(GlobalFormat.FormatoFechaCorta);
                }
            }
        }
        public string FechaContableStr { get; set; }
        public string NumeroFactura { get; set; }
        public string Moneda { get; set; }
        public double ImporteTotal { get; set; }
        public string ImporteTotalStr { get { return string.Format(GlobalFormat.Decimal2, this.ImporteTotal); } }
        public string CuentaMayor { get; set; }
        public string Impuesto { get; set; }
        public string LugarComercial { get; set; }
        public string IndicadorIGV { get; set; }
        public string IndicadorCME { get; set; }
        public string Texto { get; set; }
        public List<SolicitudLiquidacion> ListaSolicitud { get; set; }

    }
}
