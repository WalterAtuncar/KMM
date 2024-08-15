using Modelo.Request;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Service.Contracts;
using System.Linq;
using Service.Logic.Contract;
using Service.Contracts.Proveedor;
using System.Transactions;
using Modelo.Util;
using System;

using DA = Komatsu.SistemaTaxis.DataAccess;
//using Data.Util;
//using Service.Contracts;
//using Service.Contracts.Proveedor;
//using Service.Logic.Contract;
//using Service.ServiceDTO;
//using Service.Util;
//using Service.Xml.ArchivoPlano;
//using Service.Xml.Transaccion;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Transactions;

namespace Service.Logic
{
    public class SolicitudServiceLogic : ISolicitudServiceLogic
    {
        private readonly ISolicitudService oISolicitudService;
        private readonly IProveedorService oIProveedorService;
        //private readonly IProveedorWebApiService oIProveedorWebApiService;
        //private readonly IEmailService oIEmailService;

        //public SolicitudServiceLogic(ISolicitudService oISolicitudService, IProveedorService oIProveedorService, IProveedorWebApiService oIProveedorWebApiService, IEmailService oIEmailService)
        public SolicitudServiceLogic(ISolicitudService oISolicitudService, IProveedorService oIProveedorService)
        {
            this.oISolicitudService = oISolicitudService;
            this.oIProveedorService = oIProveedorService;
            //this.oIProveedorWebApiService = oIProveedorWebApiService;
            //this.oIEmailService = oIEmailService;
        }

        public async Task<OperationResult> Rechazar(SolicitudRechazo entSolicitud)
        {
            int intResultado = 0;
            int intId = 0;
            OperationResult objResult = new OperationResult();
            objResult.Success = true;
            SolicitudDetalle objSolicitudDetalle = new SolicitudDetalle();

            //var lisSolicitud = await oISolicitudService.Recuperar(entSolicitud);
            SolicitudRechazo lisSolicitud = new DA.Solicitud().Recuperar(entSolicitud);

            if (lisSolicitud.IdSituacionServicio == 4)
            {
                return new OperationResult() { Success = false, Message = "La solicitud ya fue rechazada." };
            }
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await oISolicitudService.Actualizar(entSolicitud);
                    intResultado = (int)entSolicitud.ID;

                    objSolicitudDetalle.IdSolicitud = intResultado;
                    intId = new DA.SolicitudDetalle().Grabar(entSolicitud.SolicitudDetalle);
                }
                catch (Exception e)
                {
                    ts.Dispose();
                    return new OperationResult() { Success = false, Message = e.Message };
                }
                ts.Complete();
            }
            return objResult;
        }

        //public async Task<OperationResult> Update(Data.Solicitud element, List<General.Destino> ListaDestino, List<TarifaResponse> ListaTarifa, string usuarioModifico)
        //{
        //    var xml = new ServicioTransaccionXML();
        //    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            var transaction = ObtenerDatosUpdateServiceXML(element, ListaDestino, ListaTarifa, usuarioModifico);
        //            xml.ServicioXML = ConvertFormat.ObjectToXml(transaction);

        //            var newId = await oISolicitudService.Update(xml);
        //            ts.Complete();
        //            return new OperationResult() { Success = true, NewID = newId };
        //        }
        //        catch (Exception ex)
        //        {
        //            TraceHelper.Write(ex.ToString());
        //            ts.Dispose();
        //            return new OperationResult() { Success = false, Errors = new List<string> { ex.Message } };
        //        }
        //    }
        //}

        //private ServicioTransaccion ObtenerDatosUpdateServiceXML(Data.Solicitud element, List<General.Destino> ListaDestino, List<TarifaResponse> ListaTarifa, string usuarioModifico)
        //{
        //    var modelo = new ServicioTransaccion();
        //    modelo.ArchivoPlanoServicio = new ArchivoPlanoServicio();
        //    modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino = new List<ArchivoPlanoDestino>();
        //    modelo.ArchivoPlanoServicio.ListaArchivoPlanoSolicitudProveedorTaxi = new List<ArchivoPlanoSolicitudProveedorTaxi>();
        //    modelo.ArchivoPlanoServicio.TipoServicio = element.TipoServicio;
        //    modelo.ArchivoPlanoServicio.TipoDestino = element.TipoDestino;
        //    modelo.ArchivoPlanoServicio.DistanciaKilometro = element.DistanciaKilometro;
        //    modelo.ArchivoPlanoServicio.FechaServicio = Convert.ToDateTime(element.FechaServicio);
        //    modelo.ArchivoPlanoServicio.Sociedad = element.Sociedad;
        //    modelo.ArchivoPlanoServicio.ID = element.ID;
        //    modelo.ArchivoPlanoServicio.Observaciones = element.Observaciones;
        //    modelo.ArchivoPlanoServicio.CantidadHoras = element.CantidadHoras == null ? default(int) : element.CantidadHoras.Value;
        //    modelo.ArchivoPlanoServicio.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
        //    modelo.ArchivoPlanoServicio.OrdenServicio = element.NroOrdenServicio;
        //    modelo.ArchivoPlanoServicio.TotalServicio = element.TotalServicio == null ? default(decimal) : element.TotalServicio.Value;
        //    modelo.ArchivoPlanoServicio.IdTipoServicio = element.IdTipoServicio == null ? default(int) : element.IdTipoServicio.Value;
        //    modelo.ArchivoPlanoServicio.IdTipoDestino = element.IdTipoDestino == null ? default(int) : element.IdTipoDestino.Value;
        //    modelo.ArchivoPlanoServicio.IdSociedad = element.IdSociedad == null ? default(int) : element.IdSociedad.Value;
        //    modelo.ArchivoPlanoServicio.IdMotivoCreacionSolicitud = element.IdMotivoCreacionSolicitud == null ? default(int) : element.IdMotivoCreacionSolicitud.Value;
        //    modelo.ArchivoPlanoServicio.UsuarioModifico = usuarioModifico;
        //    modelo.ArchivoPlanoServicio.UserNameRegistro = element.UserNameRegistro;

        //    //Nuevo
        //    modelo.ArchivoPlanoServicio.IdTipoPago = element.IdTipoPago.Value;
        //    modelo.ArchivoPlanoServicio.IdEstadoServicioProveedor = element.IdEstadoServicioProveedor.Value;
        //    modelo.ArchivoPlanoServicio.IdSituacionServicio = element.IdSituacionServicio.Value;

        //    int count = 1;
        //    int idProveedor = ListaDestino.OrderBy(m => m.IdProveedor).ToList().FirstOrDefault().IdProveedor;
        //    foreach (var destino in ListaDestino)
        //    {
        //        var destinoPlano = new ArchivoPlanoDestino();
        //        destinoPlano.DireccionOrigen = destino.DireccionOrigen;
        //        destinoPlano.DireccionDestino = destino.DireccionDestino;
        //        destinoPlano.NumeroDireccionDestino = destino.NumeroDireccionDestino;
        //        destinoPlano.NumeroDireccionOrigen = destino.NumeroDireccionOrigen;
        //        destinoPlano.LatitudOrigen = destino.LatitudOrigen;
        //        destinoPlano.LatitudDestino = destino.LatitudDestino;
        //        destinoPlano.LongitudOrigen = destino.LongitudOrigen;
        //        destinoPlano.LongitudDestino = destino.LongitudDestino;
        //        destinoPlano.ZonaOrigen = destino.ZonaOrigen;
        //        destinoPlano.ZonaDestino = destino.ZonaDestino;
        //        destinoPlano.DistanciaDestinoKilometro = destino.DistanciaDestinoKilometro;
        //        destinoPlano.Precio = destino.Precio;
        //        destinoPlano.IdProveedor = destino.IdProveedor;
        //        destinoPlano.Negociado = destino.Negociado;
        //        if (idProveedor == destino.IdProveedor)
        //        {
        //            destinoPlano.Orden = count++;
        //        }
        //        else
        //        {
        //            count = 1;
        //            idProveedor = destino.IdProveedor;
        //            destinoPlano.Orden = count++;
        //        }
        //        modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino.Add(destinoPlano);

        //    }

        //    foreach (var proveedor in element.SolicitudProveedorTaxi)
        //    {
        //        var proveedorPlano = new ArchivoPlanoSolicitudProveedorTaxi();
        //        proveedorPlano.IdProveedorTaxi = proveedor.ProveedorTaxiID;
        //        proveedorPlano.PrecioServicio = proveedor.PrecioServicio.Value;

        //        if (ListaTarifa.Where(m => m.FlagMenor = true).FirstOrDefault().IdProveedor == proveedorPlano.IdProveedorTaxi)
        //        {
        //            proveedorPlano.Seleccionado = 1;
        //        }
        //        else
        //        {
        //            proveedorPlano.Seleccionado = 0;
        //        }
        //        modelo.ArchivoPlanoServicio.ListaArchivoPlanoSolicitudProveedorTaxi.Add(proveedorPlano);
        //    }

        //    return modelo;
        //}

        //private ServicioTransaccion ObtenerDatosServiceXML(Data.Solicitud element, List<General.Destino> ListaDestino, List<TarifaResponse> ListaTarifa, string usuarioRegistro)
        //{
        //    var modelo = new ServicioTransaccion();
        //    modelo.ArchivoPlanoServicio = new ArchivoPlanoServicio();
        //    modelo.ArchivoPlanoServicio.ListaArchivoPlanoBeneficiario = new List<ArchivoPlanoBeneficiario>();
        //    modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino = new List<ArchivoPlanoDestino>();
        //    modelo.ArchivoPlanoServicio.ListaArchivoPlanoSolicitudProveedorTaxi = new List<ArchivoPlanoSolicitudProveedorTaxi>();
        //    modelo.ArchivoPlanoServicio.TipoServicio = element.TipoServicio;
        //    modelo.ArchivoPlanoServicio.TipoDestino = element.TipoDestino;
        //    modelo.ArchivoPlanoServicio.DistanciaKilometro = element.DistanciaKilometro;
        //    modelo.ArchivoPlanoServicio.FechaServicio = Convert.ToDateTime(element.FechaServicio);
        //    modelo.ArchivoPlanoServicio.Sociedad = element.Sociedad;
        //    modelo.ArchivoPlanoServicio.Observaciones = element.Observaciones;
        //    modelo.ArchivoPlanoServicio.CantidadHoras = element.CantidadHoras == null ? default(int) : element.CantidadHoras.Value;
        //    modelo.ArchivoPlanoServicio.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
        //    modelo.ArchivoPlanoServicio.OrdenServicio = element.NroOrdenServicio == null ? string.Empty : element.NroOrdenServicio.ToString();
        //    modelo.ArchivoPlanoServicio.TotalServicio = element.TotalServicio == null ? default(decimal) : element.TotalServicio.Value;
        //    modelo.ArchivoPlanoServicio.IdTipoServicio = element.IdTipoServicio == null ? default(int) : element.IdTipoServicio.Value;
        //    modelo.ArchivoPlanoServicio.IdTipoDestino = element.IdTipoDestino == null ? default(int) : element.IdTipoDestino.Value;
        //    modelo.ArchivoPlanoServicio.IdSociedad = element.IdSociedad == null ? default(int) : element.IdSociedad.Value;
        //    modelo.ArchivoPlanoServicio.IdMotivoCreacionSolicitud = element.IdMotivoCreacionSolicitud == null ? default(int) : element.IdMotivoCreacionSolicitud.Value;
        //    modelo.ArchivoPlanoServicio.UsuarioRegistro = usuarioRegistro;
        //    modelo.ArchivoPlanoServicio.UserNameRegistro = element.UserNameRegistro;

        //    int count = default(int);
        //    foreach (var beneficiado in element.Beneficiado)
        //    {
        //        var beneficiadoPlano = new ArchivoPlanoBeneficiario();
        //        beneficiadoPlano.ID = beneficiado.UsersID.Value;
        //        beneficiadoPlano.FlagPrincipal = (count != default(int)) ? false : true;
        //        beneficiadoPlano.Telefono = beneficiado.Telefono;
        //        count = count + 1;
        //        modelo.ArchivoPlanoServicio.ListaArchivoPlanoBeneficiario.Add(beneficiadoPlano);
        //    }

        //    modelo.ArchivoPlanoServicio.TelefonoPrincipal = modelo.ArchivoPlanoServicio.ListaArchivoPlanoBeneficiario.Where(m => m.FlagPrincipal = true).FirstOrDefault().Telefono;

        //    count = 1;
        //    int idProveedor = ListaDestino.OrderBy(m => m.IdProveedor).ToList().FirstOrDefault().IdProveedor;
        //    foreach (var destino in ListaDestino.OrderBy(m=>m.IdProveedor).ToList())
        //    {
        //        var destinoPlano = new ArchivoPlanoDestino();
        //        destinoPlano.DireccionOrigen = destino.DireccionOrigen;
        //        destinoPlano.DireccionDestino = destino.DireccionDestino;
        //        destinoPlano.NumeroDireccionDestino = destino.NumeroDireccionDestino;
        //        destinoPlano.NumeroDireccionOrigen = destino.NumeroDireccionOrigen;
        //        destinoPlano.LatitudOrigen = destino.LatitudOrigen;
        //        destinoPlano.LatitudDestino = destino.LatitudDestino;
        //        destinoPlano.LongitudOrigen = destino.LongitudOrigen;
        //        destinoPlano.LongitudDestino = destino.LongitudDestino;
        //        destinoPlano.ZonaOrigen = destino.ZonaOrigen;
        //        destinoPlano.ZonaDestino = destino.ZonaDestino;
        //        destinoPlano.DistanciaDestinoKilometro = destino.DistanciaDestinoKilometro;
        //        destinoPlano.Precio = destino.Precio;
        //        destinoPlano.IdProveedor = destino.IdProveedor;
        //        destinoPlano.Negociado = destino.Negociado;
        //        if(idProveedor == destino.IdProveedor)
        //        {
        //            destinoPlano.Orden = count++;
        //        }
        //        else
        //        {
        //            count = 1;
        //            idProveedor = destino.IdProveedor;
        //            destinoPlano.Orden = count++;
        //        }
        //        modelo.ArchivoPlanoServicio.ListaArchivoPlanoDestino.Add(destinoPlano);

        //    }

        //    foreach (var proveedor in element.SolicitudProveedorTaxi)
        //    {
        //        var proveedorPlano = new ArchivoPlanoSolicitudProveedorTaxi();
        //        proveedorPlano.IdProveedorTaxi = proveedor.ProveedorTaxiID;
        //        proveedorPlano.PrecioServicio = proveedor.PrecioServicio.Value;

        //        var IdProveedor = 0;

        //        foreach(var tarifa in ListaTarifa)
        //        {
        //            if(tarifa.FlagMenor == true)
        //            {
        //                IdProveedor = tarifa.IdProveedor;
        //            }
        //        }

        //        if (proveedorPlano.IdProveedorTaxi == IdProveedor)
        //        {
        //            proveedorPlano.Seleccionado = 1;
        //        }
        //        else
        //        {
        //            proveedorPlano.Seleccionado = 0;
        //        }
        //        modelo.ArchivoPlanoServicio.ListaArchivoPlanoSolicitudProveedorTaxi.Add(proveedorPlano);
        //    }

        //    return modelo;
        //}

        public async Task<Data.Common.Solicitud> GetById(int? id)
        {
            var result = await oISolicitudService.GetById(id).ConfigureAwait(false);
            Data.Common.CentroCosto oCentroCosto = await oISolicitudService.GetCentroCostoBySolicitud(id).ConfigureAwait(false);
            string userNameAprobo = await oISolicitudService.GetUserNameAproboBySolicitud(id).ConfigureAwait(false);
            result.EstadoProveedor = await oISolicitudService.GetEstadoProveedorBySolicitud(id).ConfigureAwait(false);
            result.SituacionServicio = await oISolicitudService.GetSituacionServicioBySolicitud(id).ConfigureAwait(false);
            result.ListaSolicitudProveedorTaxi = await oISolicitudService.GetListProveedorTaxiBySolicitud(id).ConfigureAwait(false);
            if (oCentroCosto != null) result.CentroCosto = oCentroCosto;
            else result.CentroCosto = new Data.Common.CentroCosto();

            result.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(id).ConfigureAwait(false);
            result.Conductor = await oISolicitudService.GetConductorBySolicitud(id).ConfigureAwait(false);
            result.Automovil = await oISolicitudService.GetAutomovilByAutomovil(id).ConfigureAwait(false);
            result.ListaDestino = await oISolicitudService.GetGestinoBySolicitud(id).ConfigureAwait(false);
            result.ProveedorTaxi = await oIProveedorService.GetProveedorById(result.IdProveedorTaxi).ConfigureAwait(false);
            result.Usuario = await oISolicitudService.GetUsuarioByUserName(userNameAprobo).ConfigureAwait(false);
            return result;
        }

        //public async Task<List<Data.Common.Beneficiado>> GetListBeneficiadoBySolicitud(int id)
        //{
        //    var result = await oISolicitudService.GetListBeneficiadoBySolicitud(id);
        //    return result;
        //}

        //public async Task<List<Data.Common.SolicitudProveedorTaxi>> GetListProveedorTaxiBySolicitud(int id)
        //{
        //    var result = await oISolicitudService.GetListProveedorTaxiBySolicitud(id);
        //    return result;
        //}

        //public List<Data.Common.Solicitud> GetListSolicitud()
        //{
        //    return new List<Data.Common.Solicitud>();
        //}

        public async Task<ServicioRequest> GetServicioRequestBySolicitud(int id, int idProveedor)
        {
            var solicitud = await GetById(id);
            var modelo = new ServicioRequest();
            modelo.ID = solicitud.ID;
            modelo.ListaDestino = new List<Modelo.Request.Destino>();
            modelo.ListaCliente = new List<Cliente>();
            modelo.FechaServicio = solicitud.FechaServicio.ToString("dd/MM/yyyy HH:mm:ss");
            modelo.CodigoCentroCosto = solicitud.CentroCostoAfectoCodigoSap == null ? string.Empty : solicitud.CentroCostoAfectoCodigoSap; ;
            modelo.IdEstado = solicitud.IdSituacionServicio;
            modelo.IdTipoPago = solicitud.IdTipoPago;
            modelo.IdTipoServicio = solicitud.IdTipoServicio;
            modelo.Observacion = solicitud.Observaciones == null ? string.Empty : solicitud.Observaciones;
            modelo.OrdenServicio = solicitud.NroOrdenServicio == null ? string.Empty : solicitud.NroOrdenServicio.ToString();
            modelo.RUC = solicitud.ListaBeneficiado.Where(m => m.FlagPrincipal == true).FirstOrDefault().RUC;
            modelo.Telefono = solicitud.Telefono == null ? "" : solicitud.Telefono;
            var proveedorSeleccionado = solicitud.ListaSolicitudProveedorTaxi.Where(m => m.ProveedorTaxiID == idProveedor).FirstOrDefault();
            modelo.Total = proveedorSeleccionado.PrecioServicio;
            modelo.TotalServicio = proveedorSeleccionado.PrecioServicio;
            var prov = await oISolicitudService.GetListDestinoByProveedor(id, idProveedor);
            modelo.RUCProveedor = prov.FirstOrDefault().RUCProveedor;

            modelo.ListaDestino = await oISolicitudService.GetListDestinoByProveedor(id, idProveedor);

            foreach (var item in solicitud.ListaBeneficiado)
            {
                var cliente = new Cliente();
                cliente.ApellidosCliente = $"{item.Apellidos}, {item.Nombre}";
                cliente.FlagPrincipal = item.FlagPrincipal;
                cliente.NombreCliente = item.Nombre;
                cliente.ApellidosCliente = item.Apellidos;
                cliente.NumeroDocumentoCliente = item.NumeroDocumento;
                cliente.TipoDocumentoCliente = item.IdTipoDocumento.ToString();
                cliente.Telefono = item.Telefono;
                modelo.ListaCliente.Add(cliente);
            }

            return modelo;
        }

        //public IEnumerable<usp_Solicitud_GetByLiquidacionID_Result> GetSolicitudByLiquidacionID(int LiquidacionID, int PageSize, int PageIndex, out int TotalRows, out int CantidadRegistros)
        //{
        //    return oISolicitudService.GetByLiquidacionID(LiquidacionID, PageSize, PageIndex, out TotalRows, out CantidadRegistros);
        //}

        //public OperationResult UpdateCentroCosto(int ID, string CentroCosto)
        //{
        //    return oISolicitudService.UpdateCentroCostoByID(ID, CentroCosto);
        //}

        //public IEnumerable<usp_Solicitud_GetByLiquidacionIDForExcel_Result> GetSolicitudByLiquidacionIDForExcel(int LiquidacionID)
        //{
        //    return oISolicitudService.GetByLiquidacionIDForExcel(LiquidacionID);
        //}

        //public async Task<Data.Common.Solicitud> GetSolicitudByUsernameRegistro(string username)
        //{
        //    return await oISolicitudService.GetSolicitudByUsernameRegistro(username);
        //}

        //public List<Data.Common.Solicitud> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    return oISolicitudService.GetListSolicitud(parameter, out totalRows, out cantidadRegistros);
        //}

        //public async Task<List<Data.Common.SolicitudDetalle>> GetListSolicitudDetalleBySolicitud(int idSolicitud)
        //{
        //    return await oISolicitudService.GetListSolicitudDetalleBySolicitud(idSolicitud);
        //}

        //public async Task<List<Data.Common.GastoAdicional>> GetListGastoAdicionalBySolicitud(int idSolicitud)
        //{
        //    return await oISolicitudService.GetListGastoAdicionalBySolicitud(idSolicitud);
        //}

        //public async Task<string> GetRUCByServicio(int idServicio)
        //{
        //    return await oISolicitudService.GetRUCByServicio(idServicio);
        //}
        //public async Task<Data.Common.Usuario> GetUsuarioByUserName(string userName)
        //{
        //    return await oISolicitudService.GetUsuarioByUserName(userName);
        //}

        //public List<SolicitudLiquidacion> GetListSolicitudLiquidacion(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    return oISolicitudService.GetListSolicitudLiquidacion(parameter, out totalRows, out cantidadRegistros);
        //}

        //public OperationResult UpdateCalificacion(int id, int calificacion, string comentario)
        //{
        //    return oISolicitudService.UpdateCalificacion(id, calificacion, comentario);
        //}

        //public List<SolicitudLiquidacion> GetSolicitudByLiquidacionID(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    return oISolicitudService.GetSolicitudByLiquidacionID(parameter, out totalRows, out cantidadRegistros);
        //}

        //public async Task<List<Data.Common.Destino>> GetDestinoBySolicitud(int idSolicitud)
        //{
        //    return await oISolicitudService.GetDestinoBySolicitud(idSolicitud);
        //}

        //public List<Data.Common.Solicitud> GetListPaginadoAprobador(Parameter parameter, out int totalRows, out int cantidadRegistros)
        //{
        //    return oISolicitudService.GetListSolicitudAprobador(parameter, out totalRows, out cantidadRegistros);
        //}
    }
}
