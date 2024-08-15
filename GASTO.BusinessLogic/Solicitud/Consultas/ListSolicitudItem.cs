using System;

namespace GASTO.BusinessLogic
{
    public class ListSolicitudItem
    {
        public long IdSolicitud { get; set; }
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
        public Nullable<long> IdUsuarioAprobador { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<int> EstadoLogico { get; set; }
        public Nullable<long> IdUsuarioM { get; set; }
        public Nullable<System.DateTime> FechaAccion { get; set; }
        public string IdSociedad { get; set; }
        public bool Activo { get; set; }

    }
}
