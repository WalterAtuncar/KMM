using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[Sociedad")]
    public class Sociedad
    {
        [Key]
        public int ID { get; set; }
        public string NombreFI { get; set; }
        public string CodigoSociedadCO { get; set; }
        public string CodigoSociedadFI { get; set; }
        public string NombreCO { get; set; }
        public string RUC { get; set; }
    }
}
