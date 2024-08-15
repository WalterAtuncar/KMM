using Data;
using GASTO.Service.ServiceDTO.Request;
using System;
using System.Collections.Generic;
using BE_GASTO = GASTO.Domain;

namespace WebApplication.Areas.GASTO.Models
{
    public class SolicitudAprobarSapViewModel
    {
        public int IdSolicitud { get; set; }
        public int IdSituacionServicio { get; set; }
        public string Codigo { get; set; }
        public int IdTipo { get; set; }
        public bool EnviarSap { get; set; }
        public bool EnviadoSap { get; set; }
        public bool ConDetalle { get; set; }
        public string CuentaContable { get; set; }
        public string CodigoLiquidacion { get; set; }
        public string CodigoReembolso { get; set; }
        public bool ExisteOrdenSolicitudInvalida { get; set; }      
        public List<BE_GASTO.DetRendicionSolicitud> listaDetRendicionSolicitud { get; set; }
        public List<AnticipoApi> listaEnviarSapAnticipo { get; set; }
        public List<LiquidacionApi> listaEnviarSapLiquidacion { get; set; }
        public List<CajaApi> listaEnviarSapCaja { get; set; }
        public List<TarjetaApi> listaEnviarSapTarjeta { get; set; }
        public string TipoSolicitud { get; set; }
        public bool Error_Servicio { get; set; }
        public string Error_ServicioMessage { get; set; }
    }
}