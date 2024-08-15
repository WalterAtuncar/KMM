using System;
using System.Collections.Generic;

namespace Service.Xml.ArchivoPlano
{
    public class ArchivoPlanoLiquidacion
    {
        public DateTime Fecha { get; set; }
        public int ProveedorTaxiID { get; set; }
        public decimal Total { get; set; }
        public int SociedadID { get; set; }
        public string Observaciones { get; set; }
        public string UsuarioRegistro { get; set; }
        public string UserNameRegistro { get; set; }
        public string CodigoSociedad { get; set; }
        public List<ArchivoPlanoLiquidacionDetalle> Servicios { get; set; }
    }
}
