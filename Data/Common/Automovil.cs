using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[Automovil")]
    public class Automovil
    {
        [Key]
        public int ID { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public byte[] Foto{ get; set; }
        public int CantidadPasajeros { get; set; }
        public int IdAutomovilServicio { get; set; }
        public int IdProveedorTaxi { get; set; }
    }
}
