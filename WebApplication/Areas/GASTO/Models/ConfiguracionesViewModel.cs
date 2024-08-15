using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Areas.GASTO.Models
{
    public class ConfiguracionesViewModel
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public decimal Valor1 { get; set; }
        public int Valor2 { get; set; }
        public bool Activo { get; set; }
    }
}