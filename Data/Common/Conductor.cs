using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[Conductor")]
    public class Conductor
    {
        [Key]
        public int ID { get; set; }
        public int IdConductorServicio { get; set; }
        public int IdProveedorTaxi { get; set; }
        public decimal Estrellass { get; set; }
        public string NombreCompleto { get; set; }
        public string Documento { get; set; }
        public byte[] Foto { get; set; }
        public string FotoStr { get; set; }
    }
}
