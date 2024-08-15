using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Common
{
    public class Paginas
    {
        public int Id_Usuario { get; set; }
        public string Nombre_Completo { get; set; }
        public string Menu { get; set; }
        public string Pagina { get; set; }
        public string Direccion { get; set; }
        public int IdMenu { get; set; }
        public string URL { get; set; }
    }
}