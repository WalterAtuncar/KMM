using Data;
using System.Collections.Generic;

namespace WebApplication.Models

{
    public class PaginaViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public string Icono { get; set; }
        public List<usp_Pagina_List_Not_In_Perfil_Result> ListaPagina { get; set; }
        public List<usp_Pagina_List_By_Perfil_Result> ListaPaginaPerfil { get; set; }

        public List<Data.Common.MenuPaginas> ListarMenu { get; set; }
        public List<Data.Common.Paginas> ListarPaginas { get; set; }
    }
}