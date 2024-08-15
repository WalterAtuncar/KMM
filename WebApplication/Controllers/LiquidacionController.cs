using Data.Common;
using Data.Util;
using Komatsu_SistemaSeguros.STS;
using Service.Logic.Contract;
using Service.ServiceDTO.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.AppCode;
using WebApplication.Models;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BL = Komatsu.SistemaTaxis.BusinessLogic;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class LiquidacionController : Controller
    {
        private readonly ILiquidacionServiceLogic _LiquidacionServiceLogic;
        private readonly IProveedorServiceLogic _IProveedorServiceLogic;
        private readonly ISolicitudServiceLogic _SolicitudServiceLogic;
        private readonly IProveedorWebApiServiceLogic _ProveedorWebApiServiceLogic;
        private readonly ISociedadServiceLogic _oISociedadServiceLogic;
        private readonly ICentroCostoServiceLogic _ICentroCostoServiceLogic;
        public LiquidacionController(ISociedadServiceLogic oISociedadServiceLogic,ILiquidacionServiceLogic ILiquidacionServiceLogic, 
                                     ISolicitudServiceLogic ISolicitudServiceLogic, IProveedorWebApiServiceLogic IProveedorWebApiServiceLogic,
                                     ICentroCostoServiceLogic ICentroCostoServiceLogic, IProveedorServiceLogic IProveedorServiceLogic)
        {
            _LiquidacionServiceLogic = ILiquidacionServiceLogic;
            _SolicitudServiceLogic = ISolicitudServiceLogic;
            _oISociedadServiceLogic = oISociedadServiceLogic;
            _ProveedorWebApiServiceLogic = IProveedorWebApiServiceLogic;
            _ICentroCostoServiceLogic = ICentroCostoServiceLogic;
            _IProveedorServiceLogic = IProveedorServiceLogic;
        }

        // GET: Liquidacion
        [STSUrl]
        public ActionResult Index()
        {
            return View();
        }

        [STSUrl]
        public async Task<ActionResult> Registrar()
        {
            var modelo = new LiquidacionViewModel
            {
                Liquidacion = new Data.Common.Liquidacion(),
                ListaSociedad =  _oISociedadServiceLogic.Get().ToList(),
                ListaProveedor = await _LiquidacionServiceLogic.GetListProveedorTaxi()
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetSolicitudDetalle(int idSolicitud)
        {
            var modelo = new SolicitudViewModel();
            modelo.ListaSolicitudDetalle = await _SolicitudServiceLogic.GetListSolicitudDetalleBySolicitud(idSolicitud);
            return PartialView("_SolicitudDetalle", modelo);
        }

        [HttpGet]
        public JsonResult GetListLiquidacionPaginado(int Estado, int ProveedorTaxiID, string FechaInicio, string FechaFin, int page, int rows, string sidx = "", string sord = "desc")
        {
            int totalRows = 0;
            int CantidadRegistros = 0;


            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Estado", Estado);
            parameter.Collection.Add("ProveedorTaxiID", ProveedorTaxiID);
            parameter.Collection.Add("FechaInicio", Convert.ToDateTime(FechaInicio));
            parameter.Collection.Add("FechaFin", Convert.ToDateTime(FechaFin));

            List<Data.Common.Liquidacion> listQuery = _LiquidacionServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult BuscarSolicitud()
        {
            return PartialView("_BuscarSolicitudModal");
        }

        public List<DocumentoLiquidacion> GetListDocumentoLiquidacion()
        {
            var lista = new List<DocumentoLiquidacion>
            {
                new DocumentoLiquidacion() { Codigo = "01", Nombre = "Factura" },
                new DocumentoLiquidacion() { Codigo = "SP", Nombre = "Provisión" }
            };
            return lista;
        }
        public List<IndicadorLiquidacion> GetListIndicadorLiquidacion()
        {
            var lista = new List<IndicadorLiquidacion>
            {
                new IndicadorLiquidacion() { Codigo = "S", Nombre = "Debe" },
                new IndicadorLiquidacion() { Codigo = "N", Nombre = "Haber" }
            };
            return lista;
        }

        public async Task<PartialViewResult> GetPartialCorreo(int idLiquidacion)
        {
            var modelo = new LiquidacionViewModel();
            modelo.Liquidacion = await _LiquidacionServiceLogic.GetByID(idLiquidacion);
            return PartialView("_DescargarDocumentosPopUp", modelo);
        }

        public JsonResult GetLiquidacionDetalle(int idLiquidacion = 0)
        {
            BE.LiquidacionDetalle objLiquidacionDetalle = new BE.LiquidacionDetalle();
            objLiquidacionDetalle.LiquidacionID = idLiquidacion;
            List<BE.LiquidacionDetalleLiquidar> objLista = new BL.LiquidacionDetalle().Liquidacion(objLiquidacionDetalle).ToList();
            return Json(objLista, JsonRequestBehavior.AllowGet);
        }

        public async Task<PartialViewResult> LiquidarModal(int id)
        {
            var modelo = new LiquidacionViewModel();
            string codigoSociedad = "";
            modelo.Liquidacion = new Data.Common.Liquidacion();
            modelo.Liquidacion.ListaAdjunto = new List<Adjunto>();
            modelo.Liquidacion = await _LiquidacionServiceLogic.GetByID(id);
            modelo.ProveedorTaxi = await _IProveedorServiceLogic.GetProveedorById(modelo.Liquidacion.ProveedorTaxiID);
            modelo.Liquidacion.ID = id;
            modelo.Liquidacion.LugarComercial = "K10";
            modelo.Liquidacion.Moneda = "PEN";
            modelo.Liquidacion.IndicadorIGV = "A1";
            modelo.ListaSociedad = _oISociedadServiceLogic.Get().ToList();
            modelo.ListaDocumentoLiquidacion = GetListDocumentoLiquidacion();
            modelo.ListaIndicadorLiquidacion = GetListIndicadorLiquidacion();
            modelo.ListaIndicadorIGV = await _LiquidacionServiceLogic.GetListIndicadorIGV();
            modelo.ListaCuentaMayor = await _LiquidacionServiceLogic.GetListCuentaMayor();
            #region carlos huallpa
            codigoSociedad = modelo.Liquidacion.CodigoSociedad;
            var listCentro = _ICentroCostoServiceLogic.Get();
            var lista = listCentro.ToList().Where(m => m.Soc == codigoSociedad).ToList();
            lista.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion.ToUpper()}"; return m; }).ToList();
            modelo.InputList = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_InputList", lista);
            #endregion
            return PartialView("_Liquidar", modelo);
        }

        [HttpPost]
        public async Task<ActionResult> Liquidar(LiquidacionViewModel modelo)
        {
            string sociedad = UtilSession.Usuario.IdSociedad;
            modelo.Liquidacion.UsuarioLiquido = UtilSession.Usuario.NombreCompleto;
            modelo.Liquidacion.UserNameLiquido = UtilSession.Usuario.UserName;

            for (int i = 0; i < modelo.Liquidacion.ListaSolicitud.Count; i++)
            {
                modelo.Liquidacion.ListaSolicitud[i].nro = i + 1;
            }

            var task = await ValidarOrdenServicio(modelo, sociedad);

            if (!task.Success)
            {
                return Json(task);
            }

            await _LiquidacionServiceLogic.UpdateLiquidacionDetalle(modelo.Liquidacion.ListaSolicitud);

            var detalle = await _LiquidacionServiceLogic.GetListDetalleByLiquidacion(modelo.Liquidacion.ID);
            modelo.Liquidacion.ListaSolicitud = detalle;
            modelo.Liquidacion.Impuesto = modelo.Liquidacion.CodigoDocumento == "01" ? "X" : string.Empty;
            modelo.Liquidacion.IndicadorIGV = modelo.Liquidacion.IndicadorIGV;
            modelo.Liquidacion.IndicadorCME = modelo.Liquidacion.CodigoDocumento == "01" ? string.Empty : "E";
            modelo.Liquidacion.CodigoIndicador = "S";
            modelo.Liquidacion.LugarComercial = "K10";
            modelo.Liquidacion.Moneda = "PEN";
            modelo.Liquidacion.FechaContable = Convert.ToDateTime(modelo.Liquidacion.FechaContableStr);
            modelo.Liquidacion.FechaFactura = Convert.ToDateTime(modelo.Liquidacion.FechaFacturaStr);
            modelo.Liquidacion.ListaAdjunto = (List<Adjunto>)Session["ListaAdjunto"];

            var result = await _LiquidacionServiceLogic.Liquidar(modelo.Liquidacion);
           
            return Json(result);
        }
        public ActionResult UploadFile(HttpPostedFileBase file, int idLiquidacion)
        {
            try
            {
                string absolutePath = AppCode.Util.ServerPath("~/FileLiquidaciones");
                var nombreArchivo = file.FileName.Split('.')[0];

                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }

                string alias = string.Concat(nombreArchivo, '-', idLiquidacion, Path.GetExtension(file.FileName));
                string rutaCompleta = Path.Combine(absolutePath, alias);
                file.SaveAs(rutaCompleta);

                var adjunto = new Adjunto() { Alias = alias, NombreAdjunto = file.FileName, PathAdjunto = rutaCompleta };

                var adjuntoList = new List<Adjunto>() { adjunto };

                Session["ListaAdjunto"] = adjuntoList;

                var modelo = new LiquidacionViewModel
                {
                    Liquidacion = new Liquidacion()
                };
                modelo.Liquidacion.ListaAdjunto = adjuntoList;

                var view = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_Attachment", modelo);

                return Json(new { code = 1, message = String.Empty, partial = view });
            }
            catch (Exception ex)
            {

                return Json(new { code = 0, message = ex.Message, partial = string.Empty });
            }
        }

        public ActionResult RemoveFile()
        {
            var adjuntoList = new List<Adjunto>();

            var modelo = new LiquidacionViewModel
            {
                Liquidacion = new Liquidacion()
            };
            modelo.Liquidacion.ListaAdjunto = adjuntoList;

            Session["ListaAdjunto"] = adjuntoList;

            return PartialView("_Attachment", modelo);
        }

        private async Task<Service.OperationResult> ValidarOrdenServicio(LiquidacionViewModel modelo, string sociedad)
        {
            var lista = new List<int>();
            List<OrdenServicioBatch> ListaOrdenesServicioBatch = new List<OrdenServicioBatch>();

            foreach (var detalle in modelo.Liquidacion.ListaSolicitud.Where(m => m.OrdenServicio != string.Empty && m.OrdenServicio != null))
            {
                var ordenServicioBatch = new OrdenServicioBatch
                {
                    nro = detalle.nro,
                    ordenservicio = detalle.OrdenServicio,
                    ordenservicioSinValidar = detalle.OrdenServicio,
                };

                ListaOrdenesServicioBatch.Add(ordenServicioBatch);



                //var result = await _ProveedorWebApiServiceLogic.GetOrdenServicioLiquidacion(detalle.OrdenServicio, sociedad);
                //if (!result.Success)
                //{
                //    lista.Add(detalle.ID);
                //}
            }

            var result = await _ProveedorWebApiServiceLogic.GetOrdenServicioLiquidacionBatch(ListaOrdenesServicioBatch, sociedad);

            if (result.Success)
            {
                //lista.Add(detalle.ID);
                return new Service.OperationResult() { Success = true };
            }
            else
            {
                return new Service.OperationResult() { ObjectResult = result, Success = false };
            }

             

            //if (lista.Count() > 0)
            //{
            //    return new Service.OperationResult() { ObjectResult = lista, Success = false };
            //}
            //else
            //{
            //    return new Service.OperationResult() { Success = true };
            //}
        }

        [HttpPost]
        public async Task<JsonResult> Cancelar(int idLiquidacion)
        {
            var modelo = new Data.Common.Liquidacion();
            modelo.ID = idLiquidacion;
            modelo.UserNameCancelo = UtilSession.Usuario.UserName;
            modelo.UsuarioCancelo = UtilSession.Usuario.NombreCompleto;
            var result = await _LiquidacionServiceLogic.Cancelar(modelo);
            return Json(result);
        }

        public async Task<PartialViewResult> GetNegociablePartial(int idSolicitud = 0)
        {
            var modelo = new LiquidacionViewModel();
            modelo.Solicitud = new SolicitudLiquidacion() { ID = idSolicitud };
            modelo.ListaDestino = await _SolicitudServiceLogic.GetDestinoBySolicitud(idSolicitud);
            return PartialView("_EditNegociable", modelo);
        }

        public PartialViewResult GetGastosAdicionales(int idSolicitud = 0 , SolicitudLiquidacion model = null)
        {
            var modelo = new LiquidacionViewModel();
            modelo.Solicitud = model ?? new SolicitudLiquidacion();
            return PartialView("_FormularioGasto", modelo);
        }

        #region Creado por: Carlos Huallpa
        [HttpPost]
        public JsonResult Grabar(BE.LiquidacionGrabar objLiquidacion)
        {
            int intResultado = 0;
            objLiquidacion.Estado = 1;
            objLiquidacion.Moneda = "PEN";
            objLiquidacion.Lugar_Comer = "K10";
            objLiquidacion.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            objLiquidacion.UserNameRegistro = UtilSession.Usuario.UserName;
            string strCorreoProveedor = "";
            string strRutaDocumento = "";
            string strMensaje = "Se adjunta el excel de liquidaciones";
            try
            {
                intResultado = new BL.Liquidacion().Grabar(objLiquidacion);
                byte[] bytArrayFileLiquidacion = GetReport(intResultado, out strCorreoProveedor);
                GuardarArchivo(bytArrayFileLiquidacion, "Liquidacion.xls", out strRutaDocumento);
                Mail Mailing = new Mail();
                Mailing.EnviarCorreo(strCorreoProveedor, null, "Liquidación", strMensaje, ArchivosAdjuntos: new List<string>() { strRutaDocumento });
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(intResultado, JsonRequestBehavior.AllowGet);
        }
        #endregion Creado por: Carlos Huallpa


        [HttpPost]
        public async Task<JsonResult> Guardar(LiquidacionViewModel modelo)
        {
            modelo.Liquidacion.Fecha = Convert.ToDateTime(modelo.Liquidacion.FechaStr);
            modelo.Liquidacion.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            modelo.Liquidacion.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await _LiquidacionServiceLogic.Add(modelo.Liquidacion);
            var rutaDocumento = string.Empty;
            var correoProveedor = string.Empty;
            if (result.Success)
            {
                var arrayFileLiquidacion = GetReport(result.NewID, out correoProveedor);

                GuardarArchivo(arrayFileLiquidacion, "Liquidacion.xls", out rutaDocumento);

                var Mailing = new Mail();

                string Mensaje = "Se adjunta el excel de liquidaciones";
                Mailing.EnviarCorreo(correoProveedor, null, "Liquidación", Mensaje, ArchivosAdjuntos: new List<string>() { rutaDocumento });

            }
            return Json(result);
        }
        private byte[] GetReport(int IdLiquidacion, out string CorreoProveedor)
        {
            BE.Liquidacion objLiquidacion = new BE.Liquidacion();
            objLiquidacion.ID = IdLiquidacion;
            List<BE.LiquidacionExportar> objLista = new BL.Liquidacion().ExportByID(objLiquidacion).ToList();
            CorreoProveedor = objLista.FirstOrDefault().CorreoProveedor;
            string strPath = Server.MapPath(string.Concat("~/Report/RDLC/ReportLiquidacion.rdlc"));
            string[] arrDataSet = new string[] { "ReportLiquidacionDetalle" };
            object[] arrDataSource = new object[] { objLista };
            byte[] bytFile = new Export().CreateExcel(strPath, arrDataSet, arrDataSource);
            return bytFile;
        }
        private void GuardarArchivo(byte[] arrayByte , string nombreArchivo, out string rutaDocumento)
        {
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/ReportLiquidaciones");
            if (!Directory.Exists(rutaDestinoPDF))
            {
                Directory.CreateDirectory(rutaDestinoPDF);
            }

            rutaDocumento = Path.Combine(rutaDestinoPDF, nombreArchivo);

            if (System.IO.File.Exists(rutaDocumento))
            {
                System.IO.File.Delete(rutaDocumento);
            }

            using (var fs = System.IO.File.Create(rutaDocumento))
            {
                fs.Write(arrayByte, 0, arrayByte.Length);
                fs.Dispose();
            }
        }
        public JsonResult GenerarReporte(int idLiquidacion)
        {
            string correo = string.Empty;
            var rutaDocumento = string.Empty;

            var arrayByte = GetReport(idLiquidacion, out correo);
            GuardarArchivo(arrayByte, "Liquidacion.xls", out rutaDocumento);

            var Mailing = new Mail();

            string Mensaje = "Se adjunta el excel de liquidaciones";
            var result = Mailing.EnviarCorreo(correo, null, "Liquidación", Mensaje, ArchivosAdjuntos: new List<string>() { rutaDocumento });

            return Json(result);

        }
        public JsonResult EnviarCorreoDocumentoPostJson(LiquidacionViewModel modelo)
        {
            string correo = string.Empty;
            var rutaDocumento = string.Empty;
            var result = string.Empty;

            var arrayByte = GetReport(modelo.Liquidacion.ID, out correo);
            GuardarArchivo(arrayByte, "Liquidacion.xls", out rutaDocumento);

            var Mailing = new Mail();

            string Mensaje = modelo.Liquidacion.Mensaje;

            string[] listaCorreoCopia = modelo.Liquidacion.CorreoReferencia == null ? new List<string>().ToArray(): modelo.Liquidacion.CorreoReferencia.Split(';');

            var listaCopia = new List<string>();

            foreach (var copia in listaCorreoCopia)
            {
                listaCopia.Add(copia);
            }

            if (modelo.Liquidacion.NombreFile != null)
            {
                string absolutePath = AppCode.Util.ServerPath("~/FileLiquidaciones");
                var nombreArchivoAdjunto = modelo.Liquidacion.NombreFile.Split('.')[0];
                string rutaCompletaArchivoAdjunto = Path.Combine(absolutePath, modelo.Liquidacion.NombreFile);
                result = Mailing.EnviarCorreo(correo, listaCopia, "Liquidación", Mensaje, ArchivosAdjuntos: new List<string>() { rutaDocumento, rutaCompletaArchivoAdjunto });

            }
            else
            {
                result = Mailing.EnviarCorreo(correo, listaCopia, "Liquidación", Mensaje, ArchivosAdjuntos: new List<string>() { rutaDocumento });
            }
            return Json(result);
        }
        public FileResult DowloadReporte(int idLiquidacion)
        {
            string correoProveedor = string.Empty;

            var file = GetReport(idLiquidacion, out correoProveedor);

            return File(file, MediaTypeNames.Application.Octet, "Liquidacion.xls");
        }

        public FileResult DowloadFile(string nombreFile)
        {
            string correoProveedor = string.Empty;
            string absolutePath = AppCode.Util.ServerPath("~/FileLiquidaciones");
            var nombreArchivo = nombreFile.Split('.')[0];

            string rutaCompleta = Path.Combine(absolutePath, nombreFile);

            var file = System.IO.File.ReadAllBytes(rutaCompleta);

            return File(file, MediaTypeNames.Application.Octet, nombreFile);
        }
    }
}