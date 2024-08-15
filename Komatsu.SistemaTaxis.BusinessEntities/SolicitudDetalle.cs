using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class SolicitudDetalle
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public Nullable<int> IdSolicitud { get; set; }
        public Nullable<int> IdEstado { get; set; }
        public Nullable<int> IdConductor { get; set; }
        public Nullable<int> IdAutomovil { get; set; }
        public Nullable<double> Latitud { get; set; }
        public Nullable<double> Longitud { get; set; }
        public string NombreConductor { get; set; }
        public string DocumentoConductor { get; set; }
        public string ModeloAutomovil { get; set; }
        public string PlacaAutomovil { get; set; }
        public Nullable<int> IdSituacion { get; set; }
        public string UsuarioRegistro { get; set; }
        public Nullable<DateTime> FechaRegistro { get; set; }
    }
}
