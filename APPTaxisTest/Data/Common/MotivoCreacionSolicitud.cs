using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[MotivoCreacionSolicitud")]
    public class MotivoCreacionSolicitud
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
}
