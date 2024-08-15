using GASTO.Domain;
using System.Collections.Generic;

namespace WebApplication.Areas.GASTO.Models
{
    public class UsuarioAprobadorViewModel
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string CodigoSociedadFI { get; set; }
        public string Sociedad { get; set; }
        public string Administrador { get; set; }
        public bool Activo { get; set; }
        public List<Data.CentroCosto> listaCentroCosto { get; set; }
        public List<string> listaCentroCostoByUsuario { get; set; }
    }
}