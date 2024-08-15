using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class GuardarLiquidacionModel
    {
        public DateTime Fecha { get; set; }
        public int ProveedorTaxiID { get; set; }
        public int SociedadID { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public int[] Servicios { get; set; }
    }
}