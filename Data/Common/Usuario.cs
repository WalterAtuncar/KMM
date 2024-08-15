using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Common
{
    //[Table("dbo].[Usuario")]
    public class Usuario
    {
        public int ID { get; set; }
        public string SubjectID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get { return $"{this.Nombres} {this.Apellidos}"; } }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string BU { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Sociedad { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string ListaCentroCosto { get; set; }
        public string CodigoSociedad { get; set; }
        public int IdSociedad { get; set; } 
        public string IdEstado { get; set; }
        public string Estado { get; set; }
    }
}
