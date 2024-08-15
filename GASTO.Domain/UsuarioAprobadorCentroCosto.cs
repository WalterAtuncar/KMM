namespace GASTO.Domain
{
    public class UsuarioAprobadorCentroCosto
    {
        public int ID { get; set; }
        public int IDUsuarioAprobador { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Administrador { get; set; }
        public bool Activo { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string CodigoLocal { get; set; }
    }
}
