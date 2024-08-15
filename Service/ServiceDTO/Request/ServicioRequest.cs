using Service.General;
using System;
using System.Collections.Generic;

namespace Service.ServiceDTO
{
    public class ServicioRequest
    {
        public decimal TotalServicio { get; set; }
        public string UsuarioRegistro { get; set; }
        public int ID { get; set; }
        public string RUCProveedor { get; set; }
        public string OrdenServicio { get; set; }
        public int IdProveedor { get; set; }
        public int? IdServicio { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public int IdConductor { get; set; }
        public int IdEstado { get; set; }
        public List<Destino> ListaDestino { get; set; }
        public List<Cliente> ListaCliente { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string FechaServicio { get; set; }
        public string Telefono { get; set; }
        public decimal Distancia { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string MotivoCancelacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public DateTime FechaCancelacion { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public bool Confirmacion { get; set; }
        public decimal Total { get; set; }
        public string Observacion { get; set; }
        public string Token { get; set; }
        public string UserNameAprobo { get; set; }
    }
}
