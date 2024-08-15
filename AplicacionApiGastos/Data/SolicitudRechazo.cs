using System;

namespace Data.Common
{
    public partial class SolicitudRechazo
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (08/05/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public Nullable<int> IdServicioProveedor { get; set; }
        public Nullable<int> IdTipoServicio { get; set; }
        public Nullable<int> IdTipoPago { get; set; }
        public Nullable<int> IdProveedorTaxi { get; set; }
        public Nullable<int> IdEstadoServicioProveedor { get; set; }
        public string TipoServicio { get; set; }
        public Nullable<DateTime> FechaServicio { get; set; }
        public Nullable<int> HoraServicioEnMinutos { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string CentroCostoAfectoDescripcion { get; set; }
        public string NroOrdenServicio { get; set; }
        public Nullable<int> CantidadHoras { get; set; }
        public string OrigenDireccion { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
        public string TipoDestino { get; set; }
        public string MotivoCancelacion { get; set; }
        public Nullable<DateTime> FechaCancelacion { get; set; }
        public Nullable<int> MotivoCreacionSolicitudID { get; set; }
        public string MotivoRechazo { get; set; }
        public string Observaciones { get; set; }
        public Nullable<double> TotalServicio { get; set; }
        public string TelefonoPrincipal { get; set; }
        public Nullable<double> DistanciaKilometro { get; set; }
        public Nullable<double> TotalGasto { get; set; }
        public Nullable<double> TotalGeneral { get; set; }
        public Nullable<int> SituacionServicio { get; set; }
        public string Sociedad { get; set; }
        public Nullable<bool> Liquidado { get; set; }
        public Nullable<int> IdConductor { get; set; }
        public Nullable<int> IdAutomovil { get; set; }
        public Nullable<int> IdTipoDestino { get; set; }
        public Nullable<int> IdMotivoCreacionSolicitud { get; set; }
        public Nullable<int> IdSituacionServicio { get; set; }
        public Nullable<int> IdSociedad { get; set; }
        public Nullable<int> IdUsuarioAprobador { get; set; }
        public string UsuarioRegistro { get; set; }
        public Nullable<DateTime> FechaAprobacion { get; set; }
        public string UsuarioAprobo { get; set; }
        public Nullable<int> Calificacion { get; set; }
        public string UserNameRegistro { get; set; }
        public string UserNameAprobo { get; set; }
        public string ComentarioCalificacion { get; set; }
        public string Codigo { get; set; }
        public Nullable<DateTime> FechaModificacion { get; set; }
        public string UsuarioModifico { get; set; }
    }
}
