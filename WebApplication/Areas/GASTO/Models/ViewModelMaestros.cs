using GASTO.Domain;
using System.Collections.Generic;


namespace WebApplication.Areas.GASTO.Models
{
    public class ViewModelMaestros
    {
       
        public List<SociedadSucursal> ListaSociedadSucursal { get; internal set; }
        public List<UsuarioSucursal> ListaUsuariosSucursal { get; internal set; }
        public List<Configuraciones> ListaConfiguraciones { get; internal set; }
        public List<UsuarioAprobador> ListaUsuariosAprobador { get; internal set; }
    }
}