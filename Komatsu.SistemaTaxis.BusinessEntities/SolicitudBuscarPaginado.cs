using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class SolicitudBuscarPaginado
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (08/05/2018)
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        public Nullable<int> ID { get; set; }
        public string CodigoSolicitud { get; set; }
        public string FechaServicioStr { get; set; }
        public string HoraServicio { get; set; }
        public string HoraInicio { get; set; }
        public string SituacionServicioNombre { get; set; }
        public string CentroCostoBeneficiado { get; set; }
        public string CentroCostoAfecto { get; set; }
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
    }
}
