using Dapper;
using System;
using System.Collections.Generic;

namespace Data.Common
{
    [Dapper.Table("dbo].[Solicitud")]
    public class Solicitud
    {
        [Dapper.Contrib.Extensions.Key]
        public int ID { get; set; }
        public string Codigo { get; set; }
        public int IdServicioProveedor { get; set; }
        public int CantidadHoras { get; set; }
        public int IdTipoServicio { get; set; }
        public string OrigenDireccion { get; set; }
        public int IdTipoPago { get; set; }
        public int IdProveedorTaxi { get; set; }
        public string Telefono { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public decimal DistanciaKilometro { get; set; }
        public int IdEstadoServicioProveedor { get; set; }
        public string NombreEstadoServicioProveedor { get; set; }
        public string UsuarioAprobo { get; set; }
        public DateTime FechaAprobacion{ get; set; }
        public string FechaAprobacionStr { get {
                if (this.FechaAprobacion == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return this.FechaAprobacion.ToString(GlobalFormat.FormatoFechaCorta);
            }
         }
        public DateTime FechaServicio { get; set; }
        public string FechaServicioStr { get { return this.FechaServicio.ToString("dd/MM/yyyy"); } }
        public double HoraServicioEnMinutos { get; set; }
        public double DiferenciaEnMinutos { get; set; }
        public double TiempoEspera { get; set; }
        public string HoraServicio { get { return this.FechaServicio.ToString("HH:mm tt"); } }

        public string NombreSituacionServicio { get; set; }
        public string RazonSocial { get; set; }
        public string NombreTipoServicio { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string NroOrdenServicio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroStr { get { return this.FechaRegistro.ToString("dd/MM/yyyy"); } }
        public DateTime FechaCancelacion { get; set; }
        public string FechaCancelacionStr { get
            {
                if (this.FechaCancelacion == DateTime.MinValue)
                {
                    return string.Empty;
                }
                return this.FechaCancelacion.ToString("dd/MM/yyyy"); } }
        public string MotivoCancelacion { get; set; }
       
        public string Observaciones { get; set; }
        public string MotivoRechazo { get; set; }
        public int IdSociedad { get; set; }
        public int Calificacion { get; set; }
        public int IdMotivoCreacionSolicitud { get; set; }
        public string NombreMotivoCreacionSolicitud { get; set; }
        public int IdSituacionServicio { get; set; }
        public decimal TotalServicio { get; set; }
        public MotivoCreacionSolicitud MotivoCreacionSolicitud { get; set; }
        public TipoPago TipoPago { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public Usuario Usuario { get; set; }
        public TipoDestino TipoDestino { get; set; }
        public Sociedad Sociedad { get; set; }
        public EstadoProveedor EstadoProveedor { get; set; }
        public SituacionServicio SituacionServicio { get; set; }
        public List<Data.Common.SolicitudProveedorTaxi> ListaSolicitudProveedorTaxi { get; set; }
        public CentroCosto CentroCosto { get; set; }
        public List<Beneficiado> ListaBeneficiado { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public Conductor Conductor { get; set; }
        public Automovil Automovil { get; set; }
        public ProveedorTaxi ProveedorTaxi { get; set; }
        public string UserNameAprobo { get; set; }
         [Column("UserNameRegistro")]
        public string UserNameRegistro { get; set; }
        public string BeneficiadoNombreCompleto { get; set; }
        public string BeneficiadoCodigoCentroCosto { get; set; }
        public string BeneficiadoCentroCostoDescripcion { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
    }
}
