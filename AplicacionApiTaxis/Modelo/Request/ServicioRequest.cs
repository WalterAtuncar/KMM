using System;
using System.Collections.Generic;

namespace Modelo.Request
{
    public class ServicioRequest
    {
        public int ID { get; set; }
        public int IdServicio { get; set; }
        public int IdEstado { get; set; }
        public string RUCProveedor { get; set; }
        public string OrdenServicio { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdTipoPago { get; set; }
        public List<Destino> ListaDestino{ get; set; }
        public List<Destino> ListaBeneficiado { get; set; }
        public List<Cliente> ListaCliente { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string FechaServicio { get; set; }
        public string Telefono { get; set; }
        public decimal Distancia { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string MotivoCancelacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public string URL { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameAprobo { get; set; }
        public string Metodo { get; set; }
        public decimal Total { get; set; }
        public decimal TotalServicio { get; set; }
        public string Observacion { get; set; }
        public string Token { get; set; }
        public bool Confirmacion { get; set; }
        public string sociedad { get; set; }
        public string codigo { get; set; }
        //servicio integrado
        public int IdProveedor { get; set; }
        public int IdConductor { get; set; }
        public DateTime FechaCancelacion { get; set; }
        public DateTime FechaAnulacion { get; set; }
       
      

    }
}
