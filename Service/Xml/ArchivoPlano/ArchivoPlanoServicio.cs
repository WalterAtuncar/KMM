using System;
using System.Collections.Generic;

namespace Service.Xml.ArchivoPlano
{
    public class ArchivoPlanoServicio
    {
        public string UsuarioRegistro { get; set; }
        public string UsuarioModifico { get; set; }

        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public int IdEstadoServicioProveedor { get; set; }
        public int IdSituacionServicio { get; set; }
        public decimal TotalServicio { get; set; }
        public int IdTipoDestino { get; set; }
        public int ID { get; set; }
        public string TipoServicio { get; set; }
        public string TipoDestino { get; set; }
        public int HoraServicioEnMinutos { get; set; }
        public string Sociedad { get; set; }
        public DateTime FechaServicio { get; set; }
        public string CentroCostoAfectoCodigoSap { get; set; }
        public string OrdenServicio { get; set; }
        public decimal? DistanciaKilometro { get; set; }
        public int IdMotivoCreacionSolicitud { get; set; }
        public int CantidadHoras { get; set; }
        public int IdCategoriaProducto { get; set; }
        public int CantidadProductos { get; set; }
        public string Observaciones { get; set; }
        public int IdSociedad { get; set; }
        public List<ArchivoPlanoDestino> ListaArchivoPlanoDestino { get; set; }
        public List<ArchivoPlanoBeneficiario> ListaArchivoPlanoBeneficiario { get; set; }
        public List<ArchivoPlanoSolicitudProveedorTaxi> ListaArchivoPlanoSolicitudProveedorTaxi { get; set; }
        public string UserNameRegistro { get; set; }
        public string TelefonoPrincipal { get; set; }
    }
}
