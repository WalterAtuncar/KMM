using Data;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class UsuarioViewModel
    {
        public Data.Common.Usuario Usuario { get; set; }
        public List<CentroCosto> ListaCentroCosto { get; set; }
        public List<Perfil> ListaPerfil { get; set; }
        public List<Data.Common.Usuario> ListaResponsable { get; set; }
        public List<string> ListaCentroCostoByUsuario { get; set; }
    }
}