namespace GASTO.Domain
{
    public class UsuarioSucursal
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string CodigoSociedadFI { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public string Sociedad { get; set; }
        public string Administrador { get; set; }
        public bool Activo { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string CodigoLocal { get; set; }
    }
}
