using Common.Util;
using Modelo.General;
using Newtonsoft.Json;
using SAP.Middleware.Connector;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiAplications.Util;

namespace WebApiAplications.Controllers
{
    //[Authorize]
    [RoutePrefix("api/integracion")]
    public class IntegracionController : ApiController
    {
        protected ECCDestinationConfig SAP_RFC_DestinationConfiguration;
        protected RfcDestination SAP_RFC_Destination;
        public IntegracionController()
        {
            string msj;
            SAP_RFC_DestinationConfiguration = new ECCDestinationConfig();

            try
            {
                RfcDestinationManager.RegisterDestinationConfiguration(SAP_RFC_DestinationConfiguration);
            }
            catch (Exception e)
            {
                msj = e.Message;
            }

            try
            {
                SAP_RFC_Destination = RfcDestinationManager.GetDestination(Constants.sapDestination);
            }
            catch (Exception e)
            {
                msj = e.Message;
            }
        }
        [Route("getOrdenServicio")]
        public IEnumerable<Dictionary<string, string>> GetOrdenServicio(string norden)
        {
            Utilitario.Logger();
            try
            {
                Utilitario.WriteInfoLog($"RFC - Orden de servicio {norden}");
                var FCT = ConfigurationManager.AppSettings["RFC_ORDENES"];
                Utilitario.WriteInfoLog($"Entrando al RfcRepository");
                RfcRepository repo = SAP_RFC_Destination.Repository;
                Utilitario.WriteInfoLog($"Saliendo del RfcRepository");
                //Log.Information($"RfcRepository Serialize: {JsonConvert.SerializeObject(repo)}");
                Utilitario.WriteInfoLog($"Entrando al IRfcFunction");
                IRfcFunction list = repo.CreateFunction(FCT);
                Utilitario.WriteInfoLog($"Saliendo del IRfcFunction");
                //Log.Information($"IRfcFunction Serialize: {JsonConvert.SerializeObject(list)}");
                Utilitario.WriteInfoLog($"Entrado al IRfcTable");
                IRfcTable rfc_tbl = list.GetTable("T_ORDEN");
                Utilitario.WriteInfoLog($"Saliendo del IRfcTable");
                //Log.Information($"IRfcTable Serialize: {JsonConvert.SerializeObject(rfc_tbl)}");
                list.SetValue("I_AUFNR", norden); // 000100000533 | 000100000502
                Utilitario.WriteInfoLog($"I_AUFNR = {norden}");
                Utilitario.WriteInfoLog($"Entrando al Invoke");
                list.Invoke(SAP_RFC_Destination);
                Utilitario.WriteInfoLog($"Saliendo del Invoke");
                //Log.Information($"SAP_RFC_Destination = {JsonConvert.SerializeObject(SAP_RFC_Destination)}");
                Utilitario.WriteInfoLog($"Entrando al sapTable");
                var sapTable = rfc_tbl._ToDataTable();
                Utilitario.WriteInfoLog($"Saliendo del sapTable");
                Log.Information($"sapTable Serialize = {JsonConvert.SerializeObject(sapTable._ToDictionaryList())}");
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (RfcAbapClassException ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
            catch (RfcAbapException ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
            catch (RfcAbapClassicException ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
            catch (RfcAbapBaseException ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }

        }
        [Route("postLiquidacion")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> GetLiquidacion(List<Liquidacion> Liquidaciones)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - Liquidacion {JsonConvert.SerializeObject(Liquidaciones)}");
            string json = JsonConvert.SerializeObject(Liquidaciones);
            var FCT = ConfigurationManager.AppSettings["RFC_LIQUIDACION"];
            RfcRepository repo = SAP_RFC_Destination.Repository;
            IRfcFunction list = repo.CreateFunction(FCT);
            IRfcTable rfc_tbl = list.GetTable("T_TABLA");

            foreach (Liquidacion dto in Liquidaciones)
            {
                rfc_tbl.Append();
                rfc_tbl.SetValue("SOCIEDAD", dto.SOCIEDAD);
                rfc_tbl.SetValue("PROVEEDOR", dto.PROVEEDOR);
                rfc_tbl.SetValue("FECHA_FACTURA", dto.FECHA_FACTURA);
                rfc_tbl.SetValue("FECHA_CONTABLE", dto.FECHA_CONTABLE);
                rfc_tbl.SetValue("NRO_FACTURA", dto.NRO_FACTURA);
                rfc_tbl.SetValue("CLASE_DOC", dto.CLASE_DOC);
                rfc_tbl.SetValue("IMPORTE", dto.IMPORTE);
                rfc_tbl.SetValue("MONEDA", dto.MONEDA);
                rfc_tbl.SetValue("FLAG_IMP", dto.FLAG_IMP);
                rfc_tbl.SetValue("INDICADOR_IGV", dto.INDICADOR_IGV);
                rfc_tbl.SetValue("LUGAR_COMER", dto.LUGAR_COMER);
                rfc_tbl.SetValue("TEXTO", dto.TEXTO);
                rfc_tbl.SetValue("INDICADOR_CME", dto.INDICADOR_CME);
                rfc_tbl.SetValue("CTA_MAYOR", dto.CTA_MAYOR);
                rfc_tbl.SetValue("INDICADOR_D_H", dto.INDICADOR_D_H);
                rfc_tbl.SetValue("IMPORTE_POSC", dto.IMPORTE_POSC);
                rfc_tbl.SetValue("COD_ORD_SERV", dto.COD_ORD_SERV);
                rfc_tbl.SetValue("COD_CECO", dto.COD_CECO);
                rfc_tbl.SetValue("TEXTO_POS_CUEN", dto.TEXTO_POS_CUEN);

                rfc_tbl.SetValue("ASIENTO_CONT", dto.ASIENTO_CONT);
                rfc_tbl.SetValue("EJERCICIO", dto.EJERCICIO);
                rfc_tbl.SetValue("DIA_REGIS", dto.DIA_REGIS);
                rfc_tbl.SetValue("USUA_REGIS", dto.USUA_REGIS);
                rfc_tbl.SetValue("HORA_REGIS", dto.HORA_REGIS);
                rfc_tbl.SetValue("MENSAJE", dto.MENSAJE);
            }

            await Task.Run(() => list.Invoke(SAP_RFC_Destination));

            var sapTable = await Task.Run(() => rfc_tbl._ToDataTable());
            Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
            Log.CloseAndFlush();

            return sapTable._ToDictionaryList();
        }


        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: RFC_DATA_MAESTRA
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("getCuentasBancarias")]
        [HttpGet]
        public async Task<IEnumerable<Dictionary<string, string>>> GetCuentasBancarias(string sociedad, string codigo)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV CuentaBancaria    Sociedad: {sociedad} Codigo: {codigo}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["RFC_DATA_MAESTRA"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);
                IRfcTable rfc_tbl = list.GetTable("T_TABLA");

                list.SetValue("I_BUKRS", sociedad);
                list.SetValue("I_LIFNR", codigo);

                await Task.Run(() => list.Invoke(SAP_RFC_Destination));

                var sapTable = await Task.Run(() => rfc_tbl._ToDataTable());


                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }

        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_PROVEEDOR
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("GetDatosProveedor")]
        [HttpGet]
        public async Task<IEnumerable<Dictionary<string, string>>> GetDatosProveedor(string sociedad ,string documento)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV DatosProveedor Sociedad. {sociedad} Doc. {documento}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_PROVEEDOR"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);
                IRfcTable rfc_tbl = list.GetTable("T_PROV_SAL");
                list.SetValue("I_BUKRS", sociedad);
                list.SetValue("I_STCD1", documento);
          
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));

                var sapTable = await Task.Run(() => rfc_tbl._ToDataTable());


                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }

        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_ANTICIPO
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postEnviarAnticipo")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postEnviarAnticipo(List<Anticipo> anticipos)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV Anticipos {JsonConvert.SerializeObject(anticipos)}");
            string json = JsonConvert.SerializeObject(anticipos);
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_ANTICIPO"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_GV_WEB");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_ANTICIPO");
                IRfcTable rfc_tbl = list.GetTable("T_TXT");

                foreach (Anticipo dto in anticipos)
                {
                    IRfcStructure structCC = metaData.CreateStructure();
                    structCC.SetValue("BUKRS", dto.BUKRS);
                    structCC.SetValue("VIAJE", dto.VIAJE);
                    structCC.SetValue("CORREL", dto.CORREL);
                    structCC.SetValue("LIFNR", dto.LIFNR);
                    structCC.SetValue("BLDAT", Convert.ToDateTime(dto.BLDAT));
                    structCC.SetValue("WAERS", dto.WAERS);
                    structCC.SetValue("WRBTR", dto.WRBTR);
                    structCC.SetValue("BUPLA", dto.BUPLA);
                    rfc_tbl.Append(structCC);
                }
                list.SetValue("T_TXT", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_WEB");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }


        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_LIQUIDACION
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postEnviarLiquidacion")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postEnviarLiquidacion(
            List<LiquidacionApi> liquidaciones
            )
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV Liquidacion {JsonConvert.SerializeObject(liquidaciones)}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_LIQUIDACION"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_LIQUIDA_RETORNO");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_LIQUIDACION");
                IRfcTable rfc_tbl = list.GetTable("T_LIQUI");

                foreach (LiquidacionApi dto in liquidaciones)
                {
                    IRfcStructure structCC = metaData.CreateStructure();
                    structCC.SetValue("BUKRS", dto.BUKRS);
                    structCC.SetValue("VIAJE", dto.VIAJE);
                    structCC.SetValue("CORREL", dto.CORREL);
                    structCC.SetValue("LIFNR", dto.LIFNR);
                    structCC.SetValue("BLDAT", Convert.ToDateTime(dto.BLDAT));
                    structCC.SetValue("BUPLA", dto.BUPLA);
                    structCC.SetValue("LIFNR2", dto.LIFNR2);
                    structCC.SetValue("NAME1", dto.NAME1);
                    structCC.SetValue("NAME2", dto.NAME2);
                    structCC.SetValue("NAME3", dto.NAME3);
                    structCC.SetValue("NAME4", dto.NAME4);
                    structCC.SetValue("STCD1", dto.STCD1);
                    structCC.SetValue("BLART", dto.BLART);
                    structCC.SetValue("BLDAT2", Convert.ToDateTime(dto.BLDAT2));
                    structCC.SetValue("XBLNR", dto.XBLNR);
                    structCC.SetValue("WAERS", dto.WAERS);
                    structCC.SetValue("WRBTR", dto.WRBTR);
                    structCC.SetValue("MWSKZ", dto.MWSKZ);
                    structCC.SetValue("ZE_CLASGTO", dto.ZE_CLASGTO);
                    structCC.SetValue("HKONT", dto.HKONT);
                    structCC.SetValue("KOSTL", dto.KOSTL);
                    structCC.SetValue("AUFNR", dto.AUFNR);
                    structCC.SetValue("GLOSA", dto.GLOSA);
                    structCC.SetValue("STKZN", dto.STKZN);
                    structCC.SetValue("ZE_AOR", dto.ZE_AOR);
                    structCC.SetValue("SGTXT", dto.SGTXT);
                    structCC.SetValue("TRATA", dto.TRATA);
                    structCC.SetValue("CALLE", dto.CALLE);
                    structCC.SetValue("POBLA", dto.POBLA);
                    structCC.SetValue("PAIS", dto.PAIS);
                    structCC.SetValue("BINAF", dto.BINAF);
                    rfc_tbl.Append(structCC);
                }
                list.SetValue("T_LIQUI", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_LIQUI_RETOR");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();

            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }


        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_CAJAS
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postEnviarCaja")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postEnviarCaja(
           List<CajaApi> cajas
           )
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV Cajas {JsonConvert.SerializeObject(cajas)}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_CAJAS"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_CAJAS_RETORNO");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_CAJAS");
                IRfcTable rfc_tbl = list.GetTable("T_CAJA");
                foreach (CajaApi dto in cajas)
                {
                    IRfcStructure structCC = metaData.CreateStructure();

                    structCC.SetValue("BUKRS", dto.BUKRS);
                    structCC.SetValue("FONDNR", dto.FONDNR);
                    structCC.SetValue("CORREL", dto.CORREL);
                    structCC.SetValue("LIFNR_A", dto.LIFNR_A);
                    structCC.SetValue("FECLIQ", Convert.ToDateTime(dto.FECLIQ));
                    structCC.SetValue("BUPLA", dto.BUPLA);
                    structCC.SetValue("LIFNR_P", dto.LIFNR_P);
                    structCC.SetValue("NAME1", dto.NAME1);
                    structCC.SetValue("NAME2", dto.NAME2);
                    structCC.SetValue("NAME3", dto.NAME3);
                    structCC.SetValue("NAME4", dto.NAME4);
                    structCC.SetValue("STCD1", dto.STCD1);
                    structCC.SetValue("BLART", dto.BLART);
                    structCC.SetValue("BLDAT", Convert.ToDateTime(dto.BLDAT));
                    structCC.SetValue("XBLNR", dto.XBLNR);
                    structCC.SetValue("WAERS", dto.WAERS);
                    structCC.SetValue("WRBTR", dto.WRBTR);
                    structCC.SetValue("BINAF", dto.BINAF);
                    structCC.SetValue("MWSKZ", dto.MWSKZ);
                    structCC.SetValue("CLASGTO", dto.CLASGTO);
                    structCC.SetValue("HKONT", dto.HKONT);
                    structCC.SetValue("KOSTL", dto.KOSTL);
                    structCC.SetValue("AUFNR", dto.AUFNR);
                    structCC.SetValue("GLOSA", dto.GLOSA);
                    structCC.SetValue("STKZN", dto.STKZN);
                    structCC.SetValue("AOR", dto.AOR);
                    //structCC.SetValue("SGTXT", dto.SGTXT);
                    //structCC.SetValue("TRATA", dto.TRATA);
                    //structCC.SetValue("CALLE", dto.CALLE);
                    //structCC.SetValue("POBLA", dto.POBLA);
                    //structCC.SetValue("PAIS", dto.PAIS);
                    rfc_tbl.Append(structCC);
                }

                list.SetValue("T_CAJA", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_CAJA_RETORNO");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }



        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_TARJETA
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postEnviarTarjeta")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postEnviarTarjeta(
           List<TarjetaApi> tarjetas
    )
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV Tarjetas {JsonConvert.SerializeObject(tarjetas)}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_TARJETA"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_TARJETA_SALIDA");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_TARJETA");
                IRfcTable rfc_tbl = list.GetTable("T_TARJ");

                foreach (TarjetaApi dto in tarjetas)
                {

                    IRfcStructure structCC = metaData.CreateStructure();

                    structCC.SetValue("BUKRS", dto.BUKRS);
                    structCC.SetValue("TCRENR", dto.TCRENR);
                    structCC.SetValue("CORREL", dto.CORREL);
                    structCC.SetValue("LIFNR_A", dto.LIFNR_A);
                    structCC.SetValue("FECLIQ", Convert.ToDateTime(dto.FECLIQ));
                    structCC.SetValue("BUPLA", dto.BUPLA);
                    structCC.SetValue("LIFNR_P", dto.LIFNR_P);
                    structCC.SetValue("NAME1", dto.NAME1);
                    structCC.SetValue("NAME2", dto.NAME2);
                    structCC.SetValue("NAME3", dto.NAME3);
                    structCC.SetValue("NAME4", dto.NAME4);
                    structCC.SetValue("STCD1", dto.STCD1);
                    structCC.SetValue("BLART", dto.BLART);
                    structCC.SetValue("BLDAT", Convert.ToDateTime(dto.BLDAT));
                    structCC.SetValue("XBLNR", dto.XBLNR);
                    structCC.SetValue("WAERS", dto.WAERS);
                    structCC.SetValue("WRBTR", dto.WRBTR);
                    structCC.SetValue("BINAF", dto.BINAF);
                    structCC.SetValue("MWSKZ", dto.MWSKZ);
                    structCC.SetValue("CLASGTO", dto.CLASGTO);
                    structCC.SetValue("HKONT", dto.HKONT);
                    structCC.SetValue("KOSTL", dto.KOSTL);
                    structCC.SetValue("AUFNR", dto.AUFNR);
                    structCC.SetValue("GLOSA", dto.GLOSA);
                    structCC.SetValue("STKZN", dto.STKZN);
                    structCC.SetValue("AOR", dto.AOR);
                    structCC.SetValue("SGTXT", dto.SGTXT);
                    structCC.SetValue("TRATA", dto.TRATA);
                    structCC.SetValue("CALLE", dto.CALLE);
                    structCC.SetValue("POBLA", dto.POBLA);
                    structCC.SetValue("PAIS", dto.PAIS);

                    rfc_tbl.Append(structCC);
                }

                list.SetValue("T_TARJ", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_TARJ_SALIDA");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }

        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_PAGOS_ANTICIPOS
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postPagoAnticipo")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postPagoAnticipo(List<Anticipo> anticipos)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV Pago Anticipos {JsonConvert.SerializeObject(anticipos)}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_PAGOS_ANTICIPOS"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_PAGOS_RETORNO");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFI_PAGOS_ANTICIPOS");
                IRfcTable rfc_tbl = list.GetTable("T_PAGOS_ANTICIPO");

                foreach (Anticipo dto in anticipos)
                {
                    IRfcStructure structCC = metaData.CreateStructure();
                    structCC.SetValue("BUKRS", dto.BUKRS);
                    structCC.SetValue("ZE_VIAJE", dto.VIAJE);
                    structCC.SetValue("ZE_CORREL", dto.CORREL);
                    structCC.SetValue("BELNR", dto.BELNR);
                    structCC.SetValue("GJAHR", dto.GJAHR);
                    structCC.SetValue("CPUDT", Convert.ToDateTime(dto.CPUDT));
                    rfc_tbl.Append(structCC);
                }
                list.SetValue("T_PAGOS_ANTICIPO", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_PAGOS_RETORNO");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }

        //metodo servicio web api 
        //creador: juan carlos espinoza
        //RFC: ZFITG_GV_TCAMBIO
        //empresa: GYS
        //fecha: 10/11/2018
        [Route("postTipoCambio")]
        [HttpPost]
        public async Task<IEnumerable<Dictionary<string, string>>> postTipoCambio(TipoCambio tipocambio)
        {
            Utilitario.Logger();
            Utilitario.WriteInfoLog($"RFC - GV TCambio {JsonConvert.SerializeObject(tipocambio)}");
            try
            {
                var FCT = ConfigurationManager.AppSettings["ZFITG_GV_TCAMBIO"];
                RfcRepository repo = SAP_RFC_Destination.Repository;
                IRfcFunction list = repo.CreateFunction(FCT);

                RfcStructureMetadata metaData1 = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_TCAMBIO_RETORNO");
                RfcStructureMetadata metaData = SAP_RFC_Destination.Repository.GetStructureMetadata("ZFITG_GV_TCAMBIO");
                IRfcTable rfc_tbl = list.GetTable("T_TCAMBIO");

               
                IRfcStructure structCC = metaData.CreateStructure();
                structCC.SetValue("KURST", tipocambio.KURST);
                structCC.SetValue("FCURR", tipocambio.FCURR);
                structCC.SetValue("TCURR", tipocambio.TCURR);
                structCC.SetValue("GDATU", Convert.ToDateTime(tipocambio.GDATU));
                rfc_tbl.Append(structCC);
                
                list.SetValue("T_TCAMBIO", rfc_tbl);
                await Task.Run(() => list.Invoke(SAP_RFC_Destination));
                IRfcTable rfc_tbl2 = list.GetTable("T_TCAMBIO_RETORNO");
                var sapTable = await Task.Run(() => rfc_tbl2._ToDataTable());
                Utilitario.WriteInfoLog(JsonConvert.SerializeObject(sapTable._ToDictionaryList()));
                Log.CloseAndFlush();

                return sapTable._ToDictionaryList();
            }
            catch (Exception ex)
            {
                Utilitario.WriteErrorLog(ex.Message, "Error");
                Log.CloseAndFlush();
                throw ex;
            }
        }
    }
}
