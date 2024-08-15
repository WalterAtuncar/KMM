using System;
namespace WebApplication.Areas.GASTO.Models
{
    public class DelegacionesViewModel
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioDelegado { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<System.DateTime> FechaHasta { get; set; }
        public Nullable<System.DateTime> FechaAsignacion { get; set; }
        public Nullable<System.DateTime> FechaRevocacion { get; set; }
        public string FechaDesdeStr { get; set; }
        public string FechaHastaStr { get; set; }
        public string FechaAsignacionStr { get; set; }
        public string FechaRevocacionStr { get; set; }
        public bool Principal { get; set; }
        public int Estado { get; set; }
        public string DescripcionEstado { get; set; }
        public string Comentarios { get; set; }
        public string Delegado { get; set; }
        public string Delegador { get; set; }
        public string EsPrincipal { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}