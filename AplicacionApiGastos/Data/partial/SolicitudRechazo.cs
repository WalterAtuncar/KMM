using Data.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Common
{
    public partial class SolicitudRechazo
    {
        public enum Query
        {
            None,
            List,
            AprobadorList,
            Recuperar,
        }

        #region Campos aumentados
        public Nullable<Int64> RowNumber { get; set; }
        public string NombreSituacionServicio { get; set; }
        public string NombreEstadoServicioProveedor { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string NombreTipoServicio { get; set; }
        public string BeneficiadoNombreCompleto { get; set; }
        public string BeneficiadoCodigoCentroCosto { get; set; }
        public string BeneficiadoCentroCostoDescripcion { get; set; }
        public string DireccionDestino { get; set; }
        public string DireccionOrigen { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string CodigoSolicitud { get; set; }
        public string CentroCosto { get; set; }
        public Nullable<int> PageSize { get; set; }
        public Nullable<int> PageIndex { get; set; }
        public string FechaServicioStr { get { return Convert.ToDateTime(this.FechaServicio).ToString("dd/MM/yyyy"); } }
        public string FechaRegistroStr { get { return Convert.ToDateTime(this.FechaRegistro).ToString("dd/MM/yyyy"); } }
        public string HoraServicio { get { return Convert.ToDateTime(this.FechaServicio).ToString("HH:mm tt"); } }
        public Nullable<DateTime> FechaInicio { get; set; }
        public string HoraInicio { get { return this.FechaInicio?.ToString("hh:mm tt"); } }
        public string FechaInicioStr { get; set; }
        public string FechaFinStr { get; set; }
        public string SituacionServicioNombre { get; set; }
        public string CentroCostoBeneficiado { get; set; }
        public string NombreCentroCostoBeneficiado { get; set; }
        public string CentroCostoAfecto { get; set; }
        public string NombreCentroCostoAfecto { get; set; }
        public string Documento { get; set; }
        public string Beneficiado { get; set; }
        public string Aprobador { get; set; }
        public string OrdenServicio { get; set; }
        public string PrecioTarifaStr { get; set; }
        public string PrecioEsperaStr { get; set; }
        public string PrecioDesvioRutaStr { get; set; }
        public string PrecioPeajeStr { get; set; }
        public string PrecioEstacionamientoStr { get; set; }
        public string PrecioDesplazamientoStr { get; set; }
        public Nullable<int> IndicadorNegociado { get; set; }
        public string TotalGeneralStr { get; set; }
        public string FechaAprobacionStr
        {
            get
            {
                if (this.FechaAprobacion == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return Convert.ToDateTime(this.FechaAprobacion).ToString("dd/MM/yyyy");
            }
        }
        public string FechaCancelacionStr
        {
            get
            {
                if (this.FechaCancelacion == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return Convert.ToDateTime(this.FechaCancelacion).ToString("dd/MM/yyyy");
            }
        }
        #endregion

        public SolicitudDetalle SolicitudDetalle { get; set; }
        public List<Destino> DestinoList { get; set; }
        public List<Beneficiado> BeneficiadoList { get; set; }
        public List<SolicitudProveedorTaxi> SolicitudProveedorTaxiList { get; set; }

        public SolicitudRechazo() { }

        public SolicitudRechazo(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Recuperar:
                    ID = reader.GetSafeInt32("ID");
                    IdServicioProveedor = reader.GetSafeInt32("IdServicioProveedor");
                    IdTipoServicio = reader.GetSafeInt32("IdTipoServicio");
                    IdTipoPago = reader.GetSafeInt32("IdTipoPago");
                    IdProveedorTaxi = reader.GetSafeInt32("IdProveedorTaxi");
                    IdEstadoServicioProveedor = reader.GetSafeInt32("IdEstadoServicioProveedor");
                    TipoServicio = reader.GetSafeString("TipoServicio");
                    FechaServicio = reader.GetSafeDateTime("FechaServicio");
                    HoraServicioEnMinutos = reader.GetSafeInt32("HoraServicioEnMinutos");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CentroCostoAfectoCodigoSap");
                    CentroCostoAfectoDescripcion = reader.GetSafeString("CentroCostoAfectoDescripcion");
                    NroOrdenServicio = reader.GetSafeString("NroOrdenServicio");
                    CantidadHoras = reader.GetSafeInt32("CantidadHoras");
                    OrigenDireccion = reader.GetSafeString("OrigenDireccion");
                    FechaRegistro = reader.GetSafeDateTime("FechaRegistro");
                    TipoDestino = reader.GetSafeString("TipoDestino");
                    MotivoCancelacion = reader.GetSafeString("MotivoCancelacion");
                    FechaCancelacion = reader.GetSafeDateTime("FechaCancelacion");
                    MotivoCreacionSolicitudID = reader.GetSafeInt32("MotivoCreacionSolicitudID");
                    MotivoRechazo = reader.GetSafeString("MotivoRechazo");
                    Observaciones = reader.GetSafeString("Observaciones");
                    TotalServicio = reader.GetSafeDouble("TotalServicio");
                    TelefonoPrincipal = reader.GetSafeString("TelefonoPrincipal");
                    DistanciaKilometro = reader.GetSafeDouble("DistanciaKilometro");
                    TotalGasto = reader.GetSafeDouble("TotalGasto");
                    TotalGeneral = reader.GetSafeDouble("TotalGeneral");
                    SituacionServicio = reader.GetSafeInt32("SituacionServicio");
                    Sociedad = reader.GetSafeString("Sociedad");
                    Liquidado = reader.GetSafeBoolean("Liquidado");
                    IdConductor = reader.GetSafeInt32("IdConductor");
                    IdAutomovil = reader.GetSafeInt32("IdAutomovil");
                    IdTipoDestino = reader.GetSafeInt32("IdTipoDestino");
                    IdMotivoCreacionSolicitud = reader.GetSafeInt32("IdMotivoCreacionSolicitud");
                    IdSituacionServicio = reader.GetSafeInt32("IdSituacionServicio");
                    IdSociedad = reader.GetSafeInt32("IdSociedad");
                    IdUsuarioAprobador = reader.GetSafeInt32("IdUsuarioAprobador");
                    UsuarioRegistro = reader.GetSafeString("UsuarioRegistro");
                    FechaAprobacion = reader.GetSafeDateTime("FechaAprobacion");
                    UsuarioAprobo = reader.GetSafeString("UsuarioAprobo");
                    Calificacion = reader.GetSafeInt32("Calificacion");
                    UserNameRegistro = reader.GetSafeString("UserNameRegistro");
                    UserNameAprobo = reader.GetSafeString("UserNameAprobo");
                    ComentarioCalificacion = reader.GetSafeString("ComentarioCalificacion");
                    Codigo = reader.GetSafeString("Codigo");
                    FechaModificacion = reader.GetSafeDateTime("FechaModificacion");
                    UsuarioModifico = reader.GetSafeString("UsuarioModifico");
                    break;
                case Query.List:
                    Codigo = reader.GetSafeString("Codigo");
                    RowNumber = reader.GetSafeInt64("RowNumber");
                    ID = reader.GetSafeInt32("ID");
                    IdServicioProveedor = reader.GetSafeInt32("IdServicioProveedor");
                    FechaRegistro = reader.GetSafeDateTime("FechaRegistro");
                    FechaServicio = reader.GetSafeDateTime("FechaServicio");
                    FechaCancelacion = reader.GetSafeDateTime("FechaCancelacion");
                    HoraServicioEnMinutos = reader.GetSafeInt32("HoraServicioEnMinutos");
                    IdSituacionServicio = reader.GetSafeInt32("IdSituacionServicio");
                    NombreSituacionServicio = reader.GetSafeString("NombreSituacionServicio");
                    IdEstadoServicioProveedor = reader.GetSafeInt32("IdEstadoServicioProveedor");
                    NombreEstadoServicioProveedor = reader.GetSafeString("NombreEstadoServicioProveedor");
                    IdProveedor = reader.GetSafeInt32("IdProveedor");
                    RazonSocial = reader.GetSafeString("RazonSocial");
                    NombreTipoServicio = reader.GetSafeString("NombreTipoServicio");
                    BeneficiadoNombreCompleto = reader.GetSafeString("BeneficiadoNombreCompleto");
                    BeneficiadoCodigoCentroCosto = reader.GetSafeString("BeneficiadoCodigoCentroCosto");
                    BeneficiadoCentroCostoDescripcion = reader.GetSafeString("BeneficiadoCentroCostoDescripcion");
                    UserNameRegistro = reader.GetSafeString("UserNameRegistro");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CentroCostoAfectoCodigoSap");
                    CentroCostoAfectoDescripcion = reader.GetSafeString("CentroCostoAfectoDescripcion");
                    NroOrdenServicio = reader.GetSafeString("NroOrdenServicio");
                    DireccionDestino = reader.GetSafeString("DireccionDestino");
                    DireccionOrigen = reader.GetSafeString("DireccionOrigen");
                    Calificacion = reader.GetSafeInt32("Calificacion");
                    break;
                case Query.AprobadorList:
                    Codigo = reader.GetSafeString("Codigo");
                    RowNumber = reader.GetSafeInt64("RowNumber");
                    ID = reader.GetSafeInt32("ID");
                    IdServicioProveedor = reader.GetSafeInt32("IdServicioProveedor");
                    FechaRegistro = reader.GetSafeDateTime("FechaRegistro");
                    FechaServicio = reader.GetSafeDateTime("FechaServicio");
                    FechaCancelacion = reader.GetSafeDateTime("FechaCancelacion");
                    HoraServicioEnMinutos = reader.GetSafeInt32("HoraServicioEnMinutos");
                    IdSituacionServicio = reader.GetSafeInt32("IdSituacionServicio");
                    NombreSituacionServicio = reader.GetSafeString("NombreSituacionServicio");
                    IdEstadoServicioProveedor = reader.GetSafeInt32("IdEstadoServicioProveedor");
                    NombreEstadoServicioProveedor = reader.GetSafeString("NombreEstadoServicioProveedor");
                    IdProveedor = reader.GetSafeInt32("IdProveedor");
                    RazonSocial = reader.GetSafeString("RazonSocial");
                    NombreTipoServicio = reader.GetSafeString("NombreTipoServicio");
                    BeneficiadoNombreCompleto = reader.GetSafeString("BeneficiadoNombreCompleto");
                    BeneficiadoCodigoCentroCosto = reader.GetSafeString("BeneficiadoCodigoCentroCosto");
                    BeneficiadoCentroCostoDescripcion = reader.GetSafeString("BeneficiadoCentroCostoDescripcion");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CentroCostoAfectoCodigoSap");
                    CentroCostoAfectoDescripcion = reader.GetSafeString("CentroCostoAfectoDescripcion");
                    NroOrdenServicio = reader.GetSafeString("NroOrdenServicio");
                    DireccionDestino = reader.GetSafeString("DireccionDestino");
                    DireccionOrigen = reader.GetSafeString("DireccionOrigen");
                    Calificacion = reader.GetSafeInt32("Calificacion");
                    break;
                default:
                    break;
            }
        }
    }
}
