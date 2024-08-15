﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Contracts
{
    public interface ISolicitudService_Gasto
    {
        Task<int> ValidarCreacionAnticipo(int idbeneficiario, decimal ImporteSolicitud, int IdSolicitud, double tcambio);
        Task<string> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion);
        Task<GASTO.Domain.Solicitud> GetDatoSolicitudByID(int IdSolicitud);
        Task<GASTO.Domain.Solicitud> AprobarDelegacion(int IdSolicitud, string DocNewAprobador);
        Task<GASTO.Domain.TerminosCondiciones> GetTerminosCondiciones(int idsolicitud);
        Task<GASTO.Domain.TerminosCondiciones> GetTerminosCondicionesNew(int idusuario);
        Task<GASTO.Domain.Correo_GV> GetSolicitudCorreo(int id);
        Task<List<GASTO.Domain.Usuario>> GetListBeneficiadoBySolicitud(int? id);
        Task<GASTO.Domain.Proveedor> ValidarDatosProveedorExterno(string documento);

    }

}
