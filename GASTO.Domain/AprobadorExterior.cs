namespace GASTO.Domain
{
    public class AprobadorExterior
    {
        public int ID { get; set; }
        public string SubjectID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int IdPerfil { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string CodigoSociedad { get; set; }
        public string IdEstado { get; set; }
        public string UsuarioAprobador { get; set; }
    }
}
