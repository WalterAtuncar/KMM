using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    [Table("dbo].[Beneficiado")]
    public class Beneficiado
    {
        [Key]
        public int ID { get; set; }
        public string UsersID { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Apellidos { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public bool FlagPrincipal { get; set; }
        public string NombreCompleto { get { return $"{this.Apellidos}, {this.Nombre}"; } }
        public int SolicitudID { get; set; }

        public string RUC { get; set; }
    }
}
