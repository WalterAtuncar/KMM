using System.Collections.Generic;

namespace WebApplication.Models
{
    public class CentroCostoViewModel
    {
        public Data.Common.CentroCosto CentroCosto { get; set; }
        public List<Data.Common.Usuario> ListaUsuario { get; set; }
    }
}