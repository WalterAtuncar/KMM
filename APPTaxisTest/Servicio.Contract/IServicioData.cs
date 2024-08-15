using Modelo.General;
using Modelo.Request;
using Modelo.Util;
using Modelo.XML.Transaccion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Contract
{
    public interface IServicioData
    {
        Task<int> HistorialInsert(int ID, int IdSituacion, string usuarioRegistro);
        Task<int> HistorialInsertAnular(int ID, int IdSituacion, string usuarioRegistro, string codigoServicio);
        Task<OperationService> SaveService(ServicioRequest request);
        Task<OperationService> CancelService(ServicioRequest request);
        Task<OperationService> AnularService(ServicioRequest request);
        Task<OperationService> GetEstateService(ServicioRequest request);
        Task<OperationService> GetInfoService(ServicioRequest request);
        Task<int> SaveSolicitud(ServicioTransaccionXML request);
        Task<int> PushService(ServicioTransaccionXML request);
        Task<Correo> GetSolicitudCorreo(int IdSolicitud);
        Task<Correo> GetSolicitudCorreoFinalizado(int IdServicioProveedor);
        Task<List<EmailAdjunto>> GetListEmailBySolicitud(int? IdSolicitud, int? IdServicioProveedor, int IdTipoCorreo);
        Task<List<Beneficiado>> GetListClienteByServicio(int IdSolicitud);
        Task<Token> GetToken(Token request);
        Task<List<Usuario>> GetListAprobadorByCentroCosto(string codigo);
        Task<Usuario> GetListResponsableByCentroCosto(string codigo);
        Task<List<Usuario>> GetListSoporteAdministrativo();
        Task<List<Beneficiado>> GetListBeneficiadoByServicioProveedor(int IdServicioProveedor);
        Task<Usuario> GetUsuarioRegistroBySolicitud(int id);
    }
}
