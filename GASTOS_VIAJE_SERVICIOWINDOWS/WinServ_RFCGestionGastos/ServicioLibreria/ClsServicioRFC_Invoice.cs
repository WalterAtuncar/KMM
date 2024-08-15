using Service.Contracts;
using Service.Entidad;
using Service.Service;
using Service.ServiceDTO.Response;
using Service.Util;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ServicioLibreria
{
    public static class ClsServicioRFC_Invoice
    {
        
        public static async void ProcesoValidaPago()
        {
            TraceLog.Write("Conectando a Servicio Windows Valida Pago Anticipo", "WinServ_RFCGestionViajes");
            try
            {
                int valorRetorno = 0;
                DateTime fecharegistro = DateTime.Now;
                var listaresponse = new List<Anticipo>();
                var listarequest = new List<Anticipo>();
                List<Solicitud> oListaAncitiposVerificarPago = SolicitudService.ListDatosPagosAnticipos();
                if (oListaAncitiposVerificarPago != null && oListaAncitiposVerificarPago.Count > 0)
                {
                    foreach (Solicitud row in oListaAncitiposVerificarPago)
                    {
                        Anticipo item = new Anticipo();
                        item.BUKRS = row.IdSociedad;
                        item.VIAJE = row.Codigo;
                        item.CORREL = "01";
                        item.BELNR = row.AsientoContable;
                        item.GJAHR = row.AnioEjercicio;
                        fecharegistro = (DateTime)row.FechaRegistro;
                        item.CPUDT= fecharegistro.ToString("dd.MM.yyyy");
                        listarequest.Add(item);
                    }

                    IProveedorGastoWebApiService oIProveedorGastoWebApiService = new ProveedorGastoWebApiService();
                    var return_lista = await oIProveedorGastoWebApiService.postPagoAnticipo(listarequest);
                    if (return_lista.Success)
                    {
                        listaresponse = (List<Anticipo>)return_lista.ResponseObject;
                        if (listaresponse != null && listaresponse.Count > 0)
                        {
                            foreach (Anticipo item in listaresponse)
                            {
                                Solicitud solicitud = new Solicitud();
                                solicitud.Codigo = item.VIAJE;
                                solicitud.CodigoPago = item.BLNR1;

                                //NEY TANGOA 16/07/2020
                                //string dateString = "20200708";
                                string dateString = item.CPUDT;
                                
                                string n1 = dateString.Substring(0, 4);
                                string n2 = dateString.Substring(4, 2);
                                string n3 = dateString.Substring(6, 2);
                                dateString = n1 + "-" + n2 + "-" + n3;
                                DateTime FechaPagoDatetime = DateTime.Parse(dateString);
                                solicitud.FechaPago = FechaPagoDatetime;

                                //FIN CAMBIOS
                                valorRetorno = SolicitudService.UpdatePagoAnticipo(solicitud);
                            }
                        }
                    }
                    else
                    {
                        valorRetorno = 0;
                    }
                    //}
                    oIProveedorGastoWebApiService = null;
                    TraceLog.Write("Termino conección a Servicio Windows Valida Pago Anticipo", "WinServ_RFCGestionViajes");
                }
              

            } catch(Exception ex)
            {
                TraceLog.Write("[ERROR] " + ex.ToString(), "WinServ_RFCGestionViajes");
            }
        }
    }
}
