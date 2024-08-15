namespace WebApplication.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NombreCompleto { get { return $"{Name} {LastName}"; } }
        public string IdSociedad { get; set; }
        public string Compania { get; set; }
        public string EstadoFractal { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdAlternativo { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string SubjectId { get; set; }
        public string Email { get; set; }
        public string sub { get; set; }
        public string Cargo { get; set; }
        public string Unidad { get; set; }
        public string CentroCosto { get; set; }
        public string Celular { get; set; }
        public int IdPerfil { get; set; }
    }
}