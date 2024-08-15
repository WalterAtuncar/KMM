using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASTO.Domain
{
    public class Configuraciones
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public decimal Valor1 { get; set; }
        public int Valor2 { get; set; }
        public bool Activo { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
    }
}
