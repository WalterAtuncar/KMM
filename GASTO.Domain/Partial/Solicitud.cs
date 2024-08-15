
using System;
using System.Collections.Generic;
using System.Data;

namespace GASTO.Domain
{
    public partial class Solicitud
    {
        public enum Query
        {
            None,
            Paginado,
            Recuperar,
            RecuperarDetalle,
            DatosEmail,
            AprobadorExterior,
        }

        public Solicitud() { }

        public Solicitud(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Paginado:
                    Codigo2 = reader.GetSafeString("Codigo2");
                    Codigo = reader.GetSafeString("Codigo");
                    DNI = reader.GetSafeString("DNI");
                    FechaRegistroStr = reader.GetSafeDateTime("FechaRegistro")?.ToString("dd/MM/yyyy");
                    FechaAprobacion = reader.GetSafeDateTime("FechaAprobacion") == null ? "" : reader.GetSafeDateTime("FechaAprobacion")?.ToString("dd/MM/yyyy");
                    FechaCancelacion = reader.GetSafeDateTime("FechaCancelacion") == null ? "" : reader.GetSafeDateTime("FechaCancelacion")?.ToString("dd/MM/yyyy");
                    EstadoSolicitud = reader.GetSafeString("NombreSituacionServicio");
                    NombreTipoServicio = reader.GetSafeString("NombreTipoServicio");
                    Aprobador = reader.GetSafeString("Aprobador");
                    CECO = reader.GetSafeString("CentroCostoAfectoDescripcion");
                    UsuarioActualizo = reader.GetSafeString("UsuarioActualizo");
                    Moneda = reader.GetSafeString("Moneda");
                    AsientoContable = reader.GetSafeString("AsientoContable");
                    Motivo = reader.GetSafeString("Motivo");
                    FechaDesde = reader.GetSafeDateTime("FechaSalida")?.ToString("dd/MM/yyyy");
                    FechaHasta = reader.GetSafeDateTime("FechaRetorno")?.ToString("dd/MM/yyyy");
                    FechaActualizacion = reader.GetSafeDateTime("FechaActualiza") == null ? "" : reader.GetSafeDateTime("FechaActualiza")?.ToString("dd/MM/yyyy");

                    ImporteSolicitudStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("ImporteSolicitud"));
                    GastoRendido = string.Format("{0:0,0.00}", reader.GetSafeDouble("GastoRendido"));
                    NombreCO = reader.GetSafeString("NombreCO");
                    Beneficiario = reader.GetSafeString("Beneficiado");
                    Estado = reader.GetSafeString("Estado");
                    IdDestinoBag = reader.GetSafeInt32("IdDestinoBag") == null ? 0 : (int)reader.GetSafeInt32("IdDestinoBag");
                    IdSituacionServicio = (int)reader.GetSafeInt32("IdSituacionServicio0");
                    IdSituacionServicio2 = (int)reader.GetSafeInt32("IdSituacionServicio2");
                    IdTipo = reader.GetSafeInt32("IdTipoServicio");
                    CodigoLiquidacion = reader.GetSafeString("CodigoLiquidacion");

                    break;
                case Query.Recuperar:
                    IdUsuario = reader.GetSafeInt64("IdUsuario");
                    Codigo = reader.GetSafeString("Codigo");
                    IdSolicitud = (Int64)reader.GetSafeInt64("IdSolicitud");
                    IdSucursal = reader.GetSafeInt64("IdSucursal");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CentroCostoAfectoCodigoSap");
                    IdAprobador = reader.GetSafeInt64("IdAprobador");
                    IdTipo = reader.GetSafeInt32("IdTipo");
                    FechaInicioStr = reader.GetSafeDateTime("FechaSalida")?.ToString("dd/MM/yyyy");
                    FechaFinStr = reader.GetSafeDateTime("FechaRetorno")?.ToString("dd/MM/yyyy");
                    FechaRegistroStr = reader.GetSafeDateTime("FechaRegistro")?.ToString("dd/MM/yyyy");
                    Comentario = reader.GetSafeString("Comentario");
                    IdDestino = reader.GetSafeInt32("IdDestino");
                    IdDestinoPais = reader.GetSafeInt32("IdDestino");
                    Motivo = reader.GetSafeString("Motivo");
                    //IdUsuarioAprobador = reader.GetSafeInt64("IdUsuarioAprobador");
                    ImporteSolicitud = reader.GetSafeDecimal("ImporteSolicitud");
                    IdTipoReembolso = (int)reader.GetSafeInt32("IdTipoReembolso");
                    IdMoneda = reader.GetSafeInt32("IdMoneda") == null ? 0 : (long)reader.GetSafeInt32("IdMoneda");
                    IdSociedad = reader.GetSafeString("IdSociedad");
                    NombreFile = reader.GetSafeString("NombreFile");
                    RutaCompletaFile = reader.GetSafeString("RutaCompletaFile");
                    CtaBancaria = reader.GetSafeString("CtaBancaria");
                    IdSituacionServicio = (int)reader.GetSafeInt32("IdSituacionServicio");
                    break;
                case Query.RecuperarDetalle:
                    NomApe = reader.GetSafeString("NomApe");
                    IdSolicitud = (Int64)reader.GetSafeInt64("IdSolicitud");
                    Sucursal = reader.GetSafeString("Sucursal");
                    Moneda = reader.GetSafeString("Moneda");
                    Codigo = reader.GetSafeString("Codigo");
                    CECO = reader.GetSafeString("CECO") == null ? "" : reader.GetSafeString("CECO");

                    Aprobador = reader.GetSafeString("Aprobador");
                    IdTipo = reader.GetSafeInt32("IdTipo");
                    Tipo = reader.GetSafeString("Tipo");
                    IdMoneda = (long)reader.GetSafeInt32("IdMoneda");

                    FechaInicioStr = reader.GetSafeDateTime("FechaSalida")?.ToString("dd/MM/yyyy");
                    FechaFinStr = reader.GetSafeDateTime("FechaRetorno")?.ToString("dd/MM/yyyy");
                    FechaRegistroStr = reader.GetSafeDateTime("FechaRegistro")?.ToString("dd/MM/yyyy");
                    Comentario = reader.GetSafeString("Comentario");
                    IdDestino = reader.GetSafeInt32("IdDestino");
                    IdDestinoPais = reader.GetSafeInt32("IdDestino");
                    Destino = reader.GetSafeString("Destino");
                    Motivo = reader.GetSafeString("Motivo");

                    UsuarioAprobador = reader.GetSafeString("UsuarioAprobador");
                    NombreFile = reader.GetSafeString("NombreFile");
                    CtaBancaria = reader.GetSafeString("CtaBancaria");
                    ImporteSolicitud = reader.GetSafeDecimal("ImporteSolicitud");
                    ImporteSolicitudStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("ImporteSolicitud"));
                    IdTipoReembolso = (int)reader.GetSafeInt32("IdTipoReembolso");
                    NombreCO = reader.GetSafeString("NombreCO");
                    IdSociedad = reader.GetSafeString("IdSociedad");
                    TipoReembolso = reader.GetSafeString("TipoReembolso");
                    NumeroDocumento = reader.GetSafeString("NumeroDocumento");
                    TotalRendidoStr = string.Format("{0:0,0.00}", reader.GetSafeDouble("TotalRendido"));
                    FechaContabilidadStr = reader.GetSafeDateTime("FechaContabilidad")?.ToString("dd/MM/yyyy");
                    break;
                case Query.DatosEmail:
                    this.ColaboradorC = reader.GetSafeString(nameof(ColaboradorC));
                    this.TipoC = reader.GetSafeString(nameof(TipoC));
                    this.CorreoA = reader.GetSafeString(nameof(CorreoA));
                    this.Codigo = reader.GetSafeString(nameof(Codigo));
                    break;
                case Query.AprobadorExterior:
                    IdUsuario = reader.GetSafeInt64("IDUsuarioAprobador");
                    CentroCostoAfectoCodigoSap = reader.GetSafeString("CodigoCentroCosto");
                    CorreoA = reader.GetSafeString("Email");
                    break;
                default:
                    break;
            }
        }

        public string NombreGR { get; set; }

        public string RUCGR { get; set; }

        public string ObjetivoGR { get; set; }

        public string DetalleGR { get; set; }

        public string ColaboradorC { get; set; }

        public string TipoC { get; set; }

        public string CorreoA { get; set; }



    }
}
