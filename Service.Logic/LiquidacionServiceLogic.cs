using Data.Common;
using Data.Util;
using Service.Contracts;
using Service.Logic.Contract;
using Service.ServiceDTO.Request;
using Service.Util;
using Service.Xml.ArchivoPlano;
using Service.Xml.Transaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Logic
{
    public class LiquidacionServiceLogic : ILiquidacionServiceLogic
    {
        private readonly ILiquidacionService _LiquidacionService;
        private readonly IProveedorWebApiServiceLogic _ProveedorWebApiServiceLogic;
        public LiquidacionServiceLogic(ILiquidacionService oILiquidacionService, IProveedorWebApiServiceLogic oIProveedorWebApiServiceLogic)
        {
            _LiquidacionService = oILiquidacionService;
            _ProveedorWebApiServiceLogic = oIProveedorWebApiServiceLogic;
        }
        public async Task<OperationResult> Add(Data.Common.Liquidacion liquidacion)
        {
            var XML = new LiquidacionTransaccionXML();
            var Transaction = ObtenerLiquidacionTransaccion(liquidacion);
            XML.LiquidacionXML = ConvertFormat.ObjectToXml(Transaction);
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _LiquidacionService.Insert(XML);
                    ts.Complete();
                    return new OperationResult() { Success = true , NewID = result.NewID };
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return new OperationResult() { Success = false, Errors = new List<string> { ex.Message } };
                }
            }
        }

        public async Task<OperationResult> Cancelar(Data.Common.Liquidacion liquidacion)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int result = await _LiquidacionService.Cancelar(liquidacion);
                    ts.Complete();
                    return new OperationResult() { Success = true, RowAffeted = result };
                }
                catch (Exception ex )
                {
                    ts.Dispose();
                    return new OperationResult() { Success = false, Errors = new List<string> { ex.Message } };
                }
            }
        }

        public async Task<Data.Common.Liquidacion> GetByID(int id)
        {
            return await _LiquidacionService.GetByID(id);
        }

        public async Task<List<LiquidacionApi>> GetListByID(int ID)
        {
            return await _LiquidacionService.GetListByID(ID);
        }

        public async Task<List<Data.Common.CuentaMayor>> GetListCuentaMayor()
        {
            return await _LiquidacionService.GetListCuentaMayor();
        }

        public async Task<List<SolicitudLiquidacion>> GetListDetalleByLiquidacion(int iD)
        {
            return await _LiquidacionService.GetListDetalleByLiquidacion(iD);
        }

        public async Task<List<Data.Common.IndicadorIGV>> GetListIndicadorIGV()
        {
            return await _LiquidacionService.GetListIndicadorIGV();
        }

        public async Task<List<Data.Common.ProveedorTaxi>> GetListProveedorTaxi()
        {
            return await _LiquidacionService.GetListProveedorTaxi();
        }

        public async Task<OperationResult> Liquidar(Data.Common.Liquidacion liquidacion)
        {
            var listaSAP = GetLiquidacionSAP(liquidacion);

            var resultadoLiquido = await _ProveedorWebApiServiceLogic.PostLiquidación(listaSAP);

            var validacion = ((List<LiquidacionApi>)resultadoLiquido.ObjectResult).FirstOrDefault().ASIENTO_CONT;
            var liquidacionSAP = ((List<LiquidacionApi>)resultadoLiquido.ObjectResult).FirstOrDefault();

            if (validacion == string.Empty || validacion == null)
            {
                return new OperationResult() { Success = false, Message = liquidacionSAP.MENSAJE};
            }
            else
            {
                liquidacion.AsientoContable = liquidacionSAP.ASIENTO_CONT;
                liquidacion.Ejercicio = liquidacionSAP.EJERCICIO;
                liquidacion.Mensaje = liquidacionSAP.MENSAJE;
                liquidacion.DiaRegistro = liquidacionSAP.DIA_REGIS;
                liquidacion.HoraRegistro = liquidacionSAP.HORA_REGIS;
                liquidacion.UsuarioRegistroSAP = liquidacionSAP.USUA_REGIS;
                liquidacion.CodigoIndicador = liquidacionSAP.INDICADOR_D_H;

                using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        int rowAffected = await _LiquidacionService.Liquidar(liquidacion);
                        ts.Complete();
                        return new OperationResult() { Success = true, RowAffeted = rowAffected };
                    }
                    catch (Exception ex)
                    {

                        ts.Dispose();
                        return new OperationResult() { Success = false, Message = ex.Message };
                    }
                }
            }
        }

        public List<LiquidacionApi> GetLiquidacionSAP (Data.Common.Liquidacion liquidacion)
        {

            var lista = new List<LiquidacionApi>();
            foreach (var liquidacionDetalle in liquidacion.ListaSolicitud)
            {
                var liquidacionSAP = new LiquidacionApi();
                liquidacionSAP.CLASE_DOC = liquidacion.CodigoDocumento;
                liquidacionSAP.FLAG_IMP = liquidacion.Impuesto;
                liquidacionSAP.INDICADOR_IGV = liquidacion.IndicadorIGV;
                liquidacionSAP.LUGAR_COMER = liquidacion.LugarComercial;
                liquidacionSAP.CTA_MAYOR = liquidacion.CuentaMayor;
                liquidacionSAP.INDICADOR_CME = liquidacion.IndicadorCME;
                liquidacionSAP.INDICADOR_D_H = liquidacion.CodigoIndicador;
                liquidacionSAP.NRO_FACTURA = liquidacion.NumeroFactura;
                liquidacionSAP.MONEDA = liquidacion.Moneda;
                liquidacionSAP.SOCIEDAD = liquidacion.CodigoSociedad;
                liquidacionSAP.PROVEEDOR = liquidacion.CodigoSapProveedor;
                liquidacionSAP.FECHA_FACTURA = (Convert.ToDateTime(liquidacion.FechaFacturaStr)).ToString("dd.MM.yyyy");
                liquidacionSAP.FECHA_CONTABLE = (Convert.ToDateTime(liquidacion.FechaContableStr)).ToString("dd.MM.yyyy");
                liquidacionSAP.IMPORTE = liquidacion.ListaSolicitud.Sum(m => m.TotalGeneral).ToString();
                liquidacionSAP.TEXTO_POS_CUEN = liquidacionDetalle.NombreCentroCostoAfecto;
                liquidacionSAP.TEXTO = liquidacion.Texto ?? string.Empty;

                liquidacionSAP.COD_ORD_SERV = liquidacionDetalle.OrdenServicio ?? string.Empty;
                liquidacionSAP.COD_CECO = liquidacionSAP.COD_ORD_SERV == string.Empty ?  liquidacionDetalle.CentroCostoAfecto : string.Empty;
                liquidacionSAP.IMPORTE_POSC = liquidacionDetalle.TotalGeneral.ToString();

                lista.Add(liquidacionSAP);
            }

            return lista;
        }

        
        private LiquidacionTransaccion ObtenerLiquidacionTransaccion(Data.Common.Liquidacion liquidacion)
        {
            var modelo = new LiquidacionTransaccion();
            modelo.ArchivoPlanoLiquidacion = new ArchivoPlanoLiquidacion();
            modelo.ArchivoPlanoLiquidacion.Fecha = liquidacion.Fecha;
            modelo.ArchivoPlanoLiquidacion.ProveedorTaxiID = liquidacion.ProveedorTaxiID;
            modelo.ArchivoPlanoLiquidacion.SociedadID = liquidacion.SociedadID;
            modelo.ArchivoPlanoLiquidacion.Total = liquidacion.ListaSolicitud.Sum(m=> m.TotalGeneral);
            modelo.ArchivoPlanoLiquidacion.UsuarioRegistro = liquidacion.UsuarioRegistro;
            modelo.ArchivoPlanoLiquidacion.UserNameRegistro = liquidacion.UserNameRegistro;
            modelo.ArchivoPlanoLiquidacion.CodigoSociedad = liquidacion.CodigoSociedad;

            modelo.ArchivoPlanoLiquidacion.Servicios = new List<ArchivoPlanoLiquidacionDetalle>();

            foreach (var item in liquidacion.ListaSolicitud)
            {
                modelo.ArchivoPlanoLiquidacion.Servicios.Add(
                    new ArchivoPlanoLiquidacionDetalle() {
                        SolicitudID = item.ID,
                        OrdenServicio = item.OrdenServicio,
                        CentroCostoAfecto = item.CentroCostoAfecto,
                        PrecioTarifa = item.PrecioTarifa,
                        PrecioDesvioRuta = item.PrecioDesvioRuta ,
                        PrecioEstacionamiento = item.PrecioEstacionamiento,
                        PrecioEspera = item.PrecioEspera ,
                        PrecioPeaje = item.PrecioPeaje ,
                        PrecioDesplazamiento = item.PrecioDesplazamiento,
                        TotalGeneral = item.TotalGeneral
                    });
            }

            return modelo;
        }

        public async Task<List<LiquidacionExport2>> GetLiquidacionExportByID(int idLiquidacion)
        {
            return await _LiquidacionService.GetLiquidacionExportByID(idLiquidacion);
        }

        public async Task<int> UpdateLiquidacionDetalle(List<SolicitudLiquidacion> listaSolicitud)
        {
            foreach (var liquidacion in listaSolicitud)
            {
                int result = await _LiquidacionService.UpdateLiquidacionDetalle(liquidacion);
            }
            return default(int);
        }

        public async Task<List<SolicitudLiquidacion>> GetListDetalleByIdLiquidacion(int idLiquidacion)
        {
            return await _LiquidacionService.GetListDetalleByIdLiquidacion(idLiquidacion);
        }

        public List<Data.Common.Liquidacion> ListarPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros)
        {
            return _LiquidacionService.ListarPaginado(parameter,out totalRows, out cantidadRegistros);
        }
    }
}
