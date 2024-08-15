using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[ProveedorTaxi")]
    public class ProveedorTaxi
    {
        [Key]
        public int ID { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Estado { get; set; }
        public decimal TiempoReserva { get; set; }
        public decimal TiempoEspera { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CodigoSAP { get; set; }
        public int IdEstado { get; set; }
        public string Email { get; set; }
        public int IdIndicadorIGVFactura { get; set; }
        public int IdIndicadorIGVProvision { get; set; }
        public string CodigoIndicadorFactura { get; set; }
        public string CodigoIndicadorProvision { get; set; }
        
    }
}
