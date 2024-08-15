using Data;
using Data.Common;
using Data.Util;
using Service.Xml.Transaccion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISolicitudService 
    {
        Task<int> Add(ServicioTransaccionXML element);
        Task<int> Update(ServicioTransaccionXML element);
        Task<OperationResult> Cancel(int id, string motivoCancelacion, string usuarioRegistro);
        OperationResult UpdateCalificacion(int id, int calificacion, string comentario);
        Task<Data.Solicitud> Find(int Id);
        Task<int> Refuse(int? id, string UsuarioRegistro, string motivoRechazo);
        Task<Data.Common.Correo> GetSolicitudCorreo(int id);
        Task<Data.Common.Solicitud> GetById(int? id);
        Task<Data.Common.EstadoProveedor> GetEstadoProveedorBySolicitud(int? id);
        Task<Data.Common.CentroCosto> GetCentroCostoBySolicitud(int? id);
        Task<Data.Common.Solicitud> GetSolicitudByUsernameRegistro(string username);
        Task<Data.Common.SituacionServicio> GetSituacionServicioBySolicitud(int? id);
        Task<List<Data.Common.Beneficiado>> GetListBeneficiadoBySolicitud(int? id);
        Task<List<Data.Common.Destino>> GetGestinoBySolicitud(int? id);
        Task<List<Data.Common.SolicitudProveedorTaxi>> GetListProveedorTaxiBySolicitud(int? id);
        Task<List<Service.General.Destino>> GetListDestinoByProveedor(int idSolicitud, int idProveedor);
        Task<Data.Common.Conductor> GetConductorBySolicitud(int? id);
        Task<Data.Common.Automovil> GetAutomovilByAutomovil(int? id);
        IEnumerable<usp_Solicitud_GetByLiquidacionID_Result> GetByLiquidacionID(int LiquidacionID, int PageSize, int PageIndex, out int TotalRows, out int CantidadRegistros);
        OperationResult UpdateCentroCostoByID(int LiquidacionID, string CentroCosto);
        IEnumerable<usp_Solicitud_GetByLiquidacionIDForExcel_Result> GetByLiquidacionIDForExcel(int LiquidacionID);
        List<Data.Common.Solicitud> GetListSolicitud(Parameter parameter, out int totalRows, out int cantidadRegistros);
        List<Data.Common.Solicitud> GetListSolicitudAprobador(Parameter parameter, out int totalRows, out int cantidadRegistros);
        Task<List<Data.Common.SolicitudDetalle>> GetListSolicitudDetalleBySolicitud(int idSolicitud);
        Task<string> GetUserNameAproboBySolicitud(int? id);
        Task<List<Data.Common.GastoAdicional>> GetListGastoAdicionalBySolicitud(int idSolicitud);
        Task<string> GetRUCByServicio(int? idServicio);
        Task<List<General.Tracking>> GetTracking(int idServicio);
        Task<Data.Common.Usuario> GetUsuarioByUserName(string userName);
        List<SolicitudLiquidacion> GetListSolicitudLiquidacion(Parameter parameter, out int totalRows, out int cantidadRegistros);
        List<SolicitudLiquidacion> GetSolicitudByLiquidacionID(Parameter parameter, out int totalRows, out int cantidadRegistros);
        Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud);
    }
}
