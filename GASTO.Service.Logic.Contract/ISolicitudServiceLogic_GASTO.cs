using System.Collections.Generic;
using System.Threading.Tasks;

namespace GASTO.Service.Logic.Contract
{
    public interface ISolicitudServiceLogic_Gasto
    {
        Task<OperationResult> ValidarCreacionAnticipo(int idbeneficiario, decimal ImporteSolicitud, int IdSolicitud,double tcambio);
        Task<OperationResult> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion);
        Task<GASTO.Domain.TerminosCondiciones> GetTerminosCondiciones(int idsolicitud);
        Task<GASTO.Domain.TerminosCondiciones> GetTerminosCondicionesNew(int idusuario);
        Task<GASTO.Domain.Correo_GV> GetSolicitudCorreo(int id);
        Task<List<GASTO.Domain.Usuario>> GetListBeneficiadoBySolicitud(int? id);
        Task<GASTO.Domain.Proveedor> ValidarDatosProveedorExterno(string documento);
    }
}
