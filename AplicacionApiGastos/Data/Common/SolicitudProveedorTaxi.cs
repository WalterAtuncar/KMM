using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[SolicitudProveedorTaxi")]
    public class SolicitudProveedorTaxi
    {
        [Key]
        public int SolicitudID { get; set; }
        public int ProveedorTaxiID { get; set; }
        public decimal PrecioServicio { get; set; }
        public int Seleccionado { get; set; }
        public Data.Common.ProveedorTaxi ProveedorTaxi { get; set; }
    }
}
