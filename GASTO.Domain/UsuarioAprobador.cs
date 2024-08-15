using System.Collections.Generic;

namespace GASTO.Domain
{
    public class UsuarioAprobador
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string CodigoSociedadFI { get; set; }
        public string Sociedad { get; set; }
        public string Administrador { get; set; }
        public bool Activo { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string CentrosCostos { get; set; }
        //public List<UsuarioAprobadorCentroCosto> listaUsuarioAprobadorCentroCosto { get; set; }
        public List<string> listaUsuarioAprobadorCentroCostoByUsuario { get; set; }
    }
}
