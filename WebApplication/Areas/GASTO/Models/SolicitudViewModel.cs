using Data;
using System;
using System.Collections.Generic;
using BE_GASTO = GASTO.Domain;

namespace WebApplication.Areas.GASTO.Models
{
    public class SolicitudViewModel
    {
        public string Dni { get; set; }
        public string NomApe { get; set; }
        public string FechaInicioStr { get; set; }
        public string FechaFinStr { get; set; }
        //public string Moneda { get; set; }
        public bool IsExterior { get; set; }
        public List<CentroCosto> ListaCentroCosto { get; internal set; }
       
        public List<BE_GASTO.Adjunto> ListaAdjunto { get; set; }
        //public string CentroCostoAfectoCodigoSap { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string NroOrdenServicio { get; set; }
       
        public int Ambito { get; set; }

        public List<Data.Common.Sociedad> ListaSociedad { get; internal set; }
        //public List<DocumentoLiquidacion> ListaDocumentoLiquidacion { get; set; }
        //public List<IndicadorLiquidacion> ListaIndicadorLiquidacion { get; set; }
        //public List<IndicadorIGV> ListaIndicadorIGV { get; set; }
        //public List<CuentaMayor> ListaCuentaMayor { get; set; }
        //public List<ProveedorTaxi> ListaProveedor { get; set; }
        //public List<Data.Common.Destino> ListaDestino { get; set; }


        public long IdSolicitud { get; set; }
        public string Codigo2 { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public Nullable<long> IdCentroCosto { get; set; }
        public Nullable<long> IdAprobador { get; set; }
        public Nullable<long> IdSucursal { get; set; }
        public Nullable<int> IdDestino { get; set; }
        public Nullable<int> IdTipo { get; set; }
        public string Motivo { get; set; }
        public Nullable<System.DateTime> FechaSalida { get; set; }
        public Nullable<System.DateTime> FechaRetorno { get; set; }
        public string AsientoContable { get; set; }
        public string Competencia { get; set; }
        public string CtaBancaria { get; set; }
        public Nullable<long> IdMoneda { get; set; }
        public Nullable<decimal> ImporteSolicitud { get; set; }
        public string Comentario { get; set; }
        public string Observaciones { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string BeneficiadoNombreCompleto { get; set; }
        public Nullable<long> IdUsuarioAprobador { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string FechaRegistroStr { get; set; }
        public Nullable<int> EstadoLogico { get; set; }
        public Nullable<long> IdUsuarioM { get; set; }
        public Nullable<System.DateTime> FechaAccion { get; set; }

        public string IdSociedad { get; set; }
        public bool Activo { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string FechaActualizacion { get; set; }
        public int IdSituacionServicio { get; set; }
        public int IdDestinoBag { get; set; }
        public int IdTipoReembolso { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
        public string Codigo { get; set; }
        public string NombreTipoServicio { get; set; }
        public string ImporteSolicitudStr { get; set; }
        public string DNI { get; set; }
        public string EstadoSolicitud { get; set; }
        public string NombreSituacionServicio { get; set; }
        public string Aprobador { get; set; }
        public string CECO { get; set; }
        public string UsuarioActualizo { get; set; }
        public string Moneda { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string NombreAdjunto { get; set; }
        public string  AnioEjercicio { get; set; }
        public int IdPerfil { get; set; }

    }
}