﻿namespace WebApplication.Areas.GASTO.Models
{
    public class SociedadSucursalViewModel
    {
        public int ID { get; set; }
        public int IdSucursal { get; set; }
        public string CodigoSociedadFI { get; set; }
        public string Sociedad { get; set; }
        public string Sucursal { get; set; }
        public bool Activo { get; set; }
    }
}