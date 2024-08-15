using System;
using System.Collections.Generic;

namespace Modelo.XML.ArchivoPlano
{
    public class ArchivoPlanoServicio
    {
       
        public string Token { get; set; }
        public int ID { get; set; }
        public int IdAutomovil;
        public int IdConductor;
        public int IdServicio { get; set; }
        public int IdEstado { get; set; }
        public string RUCProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public string CodigoCentroCosto { get; set; }
        public DateTime FechaServicio { get; set; }
        public string Telefono { get; set; }
        public decimal Distancia { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string MotivoCancelacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public List<ArchivoPlanoServicioDetalle> ListaArchivoPlanoServicioDetalle { get; set; }
        public List<ArchivoPlanoTracking> ListaArchivoPlanoTracking { get; set; }
        public List<ArchivoPlanoDestino> ListaArchivoPlanoDestino { get; set; }
        public List<ArchivoPlanoCliente> ListaArchivoPlanoCliente { get; set; }
        public ArchivoPlanoConductor ArchivoPlanoConductor { get; set; }
        public ArchivoPlanoAutoMovil ArchivoPlanoAutoMovil { get; set; }
        public List<ArchivoPlanoGasto> ListaArchivoPlanoGasto { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameAprobo { get; set; }
        public decimal TotalServicio { get; set; }
    }
}
