using System;
using System.Collections.Generic;

namespace GASTO.Domain
{
    public partial class DetRendicionSolicitud
    {
      

        public Int64 IdDetRendicionSolicitud { get; set; }
        public string ID { get; set; }
        public Int64 IdRendicionSolicitud { get; set; }
        public Int64 IdTipoDocumento { get; set; }
        public string NroComprobante { get; set; }
        public Int64 IdProveedor { get; set; }
        public List<Adjunto> ListaAdjunto { get; set; }
        public string IsProveedorExt { get; set; }
        public string Proveedor { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Nombre3 { get; set; }
        public string Nombre4 { get; set; }
        
        public string Clase { get; set; }
        public string CodigoGastoViaje { get; set; }
        public string CuentaContable { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoDocumento { get; set; }
        public string RUC { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string FechaStr { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public double Importe { get; set; }
        public decimal TipoCambio { get; set; }
        public string ImporteStr { get; set; }
        public double BaseInafecta { get; set; }
        public string BaseInafectaStr { get; set; }
        public string Impuesto { get; set; }
        public string TipoGastoViaje { get; set; }
        public string CECO { get; set; }
        public string OrdenServicio { get; set; }
        public Int64 IdTipoCambio { get; set; }
        public int IdTipo { get; set; }
        public double TotalSoles { get; set; }
        public string TotalSolesStr { get; set; }
        public Int64 IdTipoGastoViaje { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public Int64 IdOrdenServicio { get; set; }
        public int PersonaFisica { get; set; }
        public Int64 IdImpuesto { get; set; }
        public Nullable<Int64> IdUsuarioM { get; set; }
        public Nullable<System.DateTime> FechaAccion { get; set; }
        public string FechaAccionStr { get; set; }
        public int PersonaJuridica { get; set; }
        public int ProveedorExterno { get; set; }
        public string RUT { get; set; }
        public string Tratamiento { get; set; }
        public string RazonSocial { get; set; }
        public bool Externo { get; set; }
        public string Calle { get; set; }
        public string Poblacion { get; set; }
        public string Pais { get; set; }
        public string GlosaDetalle { get; set; }
        public int ProveedorExtranjero { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public bool Tipo { get; set; }
        public string TipoStr { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string CodAbreviatura { get; set; }
        public string NroIzquierda { get; set; }
        public string NroCentro { get; set; }
        public string NroDerecha { get; set; }
        public string NombreFile { get; set; }
        public string RutaCompletaFile { get; set; }
        public string CodigoLiquidacion { get; set; }
        public string CORREL { get; set; }
        public string CodigoSAPProveedor { get; set; }
        public string NombreGR { get; set; }
        public string RUCGR { get; set; }
        public string ObjetivoGR { get; set; }
        public string DetalleGR { get; set; }

        public bool EnvioAnticipo { get; set; }
        public bool Error_Servicio { get; set; }
        public string Error_ServicioMessage { get; set; }
    }
}
