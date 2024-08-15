//using Data;
//using Data.Common;
//using Data.Util;
using Data.Common;
using Modelo.Request;
using Modelo.Util;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface ISolicitudServiceLogic
    {
        //Task<OperationResult> Add(Data.Solicitud element, List<Service.General.Destino> ListaDestino, List<TarifaResponse> ListaTarifa,string usuarioRegistro);
        //Task<OperationResult> Update(Data.Solicitud element, List<General.Destino> ListaDestino, List<TarifaResponse> ListaTarifa, string usuarioRegistro);
        //OperationResult UpdateCalificacion(int id, int calificacion, string comentario);
        Task<Data.Common.Solicitud> GetById(int? id);
        //Task<List<Data.Common.Beneficiado>> GetListBeneficiadoBySolicitud(int id);
        //Task<List<Data.Common.SolicitudProveedorTaxi>> GetListProveedorTaxiBySolicitud(int id);
        Task<ServicioRequest> GetServicioRequestBySolicitud(int id, int idProveedor);
        Task<OperationResult> Rechazar(SolicitudRechazo entSolicitud);
        //List<Data.Common.Solicitud> GetListSolicitud();
        //Task<Data.Common.Solicitud> GetSolicitudByUsernameRegistro(string username);
        //IEnumerable<usp_Solicitud_GetByLiquidacionID_Result> GetSolicitudByLiquidacionID(int LiquidacionID, int PageSize, int PageIndex, out int TotalRows, out int CantidadRegistros);
        //OperationResult UpdateCentroCosto(int ID, string CentroCosto);
        //IEnumerable<usp_Solicitud_GetByLiquidacionIDForExcel_Result>  GetSolicitudByLiquidacionIDForExcel(int LiquidacionID);
        //List<Data.Common.Solicitud> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
        //List<Data.Common.Solicitud> GetListPaginadoAprobador(Parameter parameter, out int totalRows, out int cantidadRegistros);
        //Task<List<Data.Common.SolicitudDetalle>> GetListSolicitudDetalleBySolicitud(int idSolicitud);
        //Task<List<Data.Common.GastoAdicional>> GetListGastoAdicionalBySolicitud(int idSolicitud);
        //Task<string> GetRUCByServicio(int idServicio);
        //List<SolicitudLiquidacion> GetListSolicitudLiquidacion(Parameter parameter, out int totalRows, out int cantidadRegistros);
        //List<SolicitudLiquidacion> GetSolicitudByLiquidacionID(Parameter parameter, out int totalRows, out int cantidadRegistros);
        //Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud);
    }
}
