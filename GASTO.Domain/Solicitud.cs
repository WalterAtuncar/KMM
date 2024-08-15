using System;
using System.Collections.Generic;

namespace GASTO.Domain
{
    public partial class Solicitud
    {
        public long IdSolicitud { get; set; }
        public string Codigo2 { get; set; }
        public string FechaContabilidadStr { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public Nullable<long> IdUsuarioLogin { get; set; }
        public Nullable<long> IdCentroCosto { get; set; }
        public Nullable<long> IdAprobador { get; set; }
        public Nullable<long> IdSucursal { get; set; }
        public Nullable<int> IdDestino { get; set; }
        public Nullable<int> IdDestinoPais { get; set; }
        public Nullable<int> IdTipo { get; set; }
        public List<Adjunto> ListaAdjunto { get; set; }
        public string Motivo { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
        public Nullable<System.DateTime> FechaRetorno { get; set; }
        public string AsientoContable { get; set; }
        public string Competencia { get; set; }
        public string CtaBancaria { get; set; }
        public string CodigoLiquidacion { get; set; }
        public string TotalRendidoStr { get; set; }
        public Nullable<long> IdMoneda { get; set; }
        public Nullable<decimal> ImporteSolicitud { get; set; }
        public string Comentario { get; set; }
        public string Observaciones { get; set; }
        public string CodigoLocal { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string BeneficiadoNombreCompleto { get; set; }
        public Nullable<long> IdUsuarioAprobador { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string FechaRegistroStr { get; set; }
        public Nullable<int> EstadoLogico { get; set; }
        public Nullable<long> IdUsuarioM { get; set; }
        public Nullable<int> IdUsuarioR { get; set; }
        public Nullable<System.DateTime> FechaAccion { get; set; }
        public string NumeroDocumento { get; set; }

        public string IdSociedad { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public string FechaDesde { get; set; }
        public string CuentaContable { get; set; }
        public string FechaAprobacion { get; set; }
        public string FechaCancelacion { get; set; }
        public string FechaHasta { get; set; }
        public string FechaActualizacion { get; set; }
        public int IdSituacionServicio { get; set; }
        public int IdTipoReembolso { get; set; }
        public int IdDestinoBag { get; set; }
        public int IdSituacionServicio2 { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
        public string Codigo { get; set; }
        public string NombreTipoServicio { get; set; }
        public string ImporteSolicitudStr { get; set; }
        public string GastoRendido { get; set; }
        public string DNI { get; set; }
        public string EstadoSolicitud { get; set; }
        public string NombreSituacionServicio { get; set; }
        public string Aprobador { get; set; }
        public string CECO { get; set; }
        public string UsuarioActualizo { get; set; }
        public string Moneda { get; set; }
        public string CodigoReembolso { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }

        public string FechaInicioStr { get; set; }
        public string FechaFinStr { get; set; }
        public string NombreCO { get; set; }
        public string Beneficiario { get; set; }
        public string AnioEjercicio { get; set; }
        public string Estado { get; set; }
        public string UserName { get; set; }

        public string Destino { get; set; }
        public string UsuarioAprobador { get; set; }
        public string TipoReembolso { get; set; }
        public string Tipo { get; set; }
        public string Sucursal { get; set; }
        public string NomApe { get; set; }
        public string NombreFile { get; set; }
        public string RutaCompletaFile { get; set; }
        public decimal TipoCambio { get; set; }
        public string TipoSolicitud { get; set; }
        public string IdOrdenServicio { get; set; }

    }
}
