using GASTO.Domain;
using GASTO.Service.Contracts;
using GASTO.Service.Logic.Contract;
using GASTO.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASTO.Service.Logic
{
    public class SolicitudServiceLogic_Gasto : ISolicitudServiceLogic_Gasto
    {
        private readonly ISolicitudService_Gasto oISolicitudService_Gasto;
        public SolicitudServiceLogic_Gasto()
        {
            this.oISolicitudService_Gasto = new SolicitudService_Gasto();
        }

        public Task<List<Usuario>> GetListBeneficiadoBySolicitud(int? id)
        {
            return oISolicitudService_Gasto.GetListBeneficiadoBySolicitud(id);
        }

        public Task<Correo_GV> GetSolicitudCorreo(int id)
        {
            return oISolicitudService_Gasto.GetSolicitudCorreo(id);
        }

        public Task<TerminosCondiciones> GetTerminosCondiciones(int idsolicitud)
        {
            return oISolicitudService_Gasto.GetTerminosCondiciones(idsolicitud);
        }
        public Task<TerminosCondiciones> GetTerminosCondicionesNew(int idusuario)
        {
            return oISolicitudService_Gasto.GetTerminosCondicionesNew(idusuario);
        }

        public async Task<OperationResult> ValidarCreacionAnticipo(int idbeneficiario, decimal ImporteSolicitud, int IdSolicitud, double tcambio)
        {
            try
            {
                int result = await oISolicitudService_Gasto.ValidarCreacionAnticipo(idbeneficiario, ImporteSolicitud, IdSolicitud, tcambio);
                if (result == 1)
                {
                    return new OperationResult() { Success = true, Valor= result };
                }
                else
                {
                    return new OperationResult() { Success = false, Valor= result };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
        }
        public async Task<OperationResult> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion)
        {
            try
            {
                var result = await oISolicitudService_Gasto.ValidarNumeroComprobanteDuplicado(ruc,idtipodocumento,numerocomprobante,idrendicion);
                //if (result == 1)
                if (result != null)
                {
                    return new OperationResult() { Success = true, Message = result };
                }
                else
                {
                    return new OperationResult() { Success = false, Message = result };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
            }
        }
        public Task<Proveedor> ValidarDatosProveedorExterno(string documento)
        {
            return oISolicitudService_Gasto.ValidarDatosProveedorExterno(documento);
        }


    }
}
