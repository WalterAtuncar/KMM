using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[TipoServicio")]
    public class TipoServicio
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }

        public TipoServicio(int ID, string Nombre)
        {
            this.ID = ID;
            this.Nombre = Nombre;
        }
    }
}
