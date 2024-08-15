using System.Collections.Generic;
using Data;

namespace WebApplication.Models
{
    public class SolicitudViewModel
    {
        public Data.Common.Solicitud Solicitud { get; set; }
        public string ListaUser { get; set; }
        public List<Data.Common.GastoAdicional> ListaGastoAdicional { get; set; }
        public List<Data.Common.SolicitudDetalle> ListaSolicitudDetalle { get; set; }
        public List<Data.Common.Solicitud> ListaSolicitud { get; set; }
        public List<Service.General.Destino> ListaDestino { get; set; }
        public List<Data.Common.Destino> ListaRecorrido { get; set; }
        public List<Data.Common.Usuario> ListaUsuarios { get; set; }
        public List<Data.Common.SolicitudProveedorTaxi> ListaSolicitudProveedorTaxi { get; set; }
        public List<Data.Common.Sociedad> ListaSociedad { get; internal set; }
        public List<CentroCosto> ListaCentroCosto { get; internal set; }
        public List<Data.TipoServicio> ListaTipoServicio { get; set; }
        public List<Data.TipoDestino> ListaTipoDestino { get; set; }
        public List<Data.MotivoCreacionSolicitud> ListaMotivoCreacionSolicitud { get; set; }
    }
}