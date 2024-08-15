using GASTO.Service.Contracts;
using GASTO.Service.Logic;
using GASTO.Service.Logic.Contract;
using GASTO.Service.ServiceDTO.Request;
using GASTO.Service.ServiceDTO.Response;
using GASTO.Service.Services;
using iTextSharp.text;
using Komatsu_SistemaSeguros.STS;
using NPOI.XWPF.UserModel;
using Service.Contracts;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Areas.GASTO.Models;
using WebApplication.Util;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using BL_GASTO = GASTO.BusinessLogic;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace WebApplication.Areas.GASTO.Controllers
{
    public class SolicitudController : BaseController
    {
        private readonly IProveedorGastoWebApiService oIProveedorGastoWebApiService;
        private readonly IProveedorGastoWebApiServiceLogic oIProveedorGastoWebApiServiceLogic;
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        private readonly ISociedadServiceLogic oISociedadServiceLogic;
        private readonly IUsersService oIUsersService;
        private readonly ISolicitudServiceLogic_Gasto oISolicitudServiceLogic_Gasto;
        private readonly IUsersGastoService oIUsersGastoService;
        private readonly IHistorialLogService oIHistorialLogService;
        private readonly ISociedadSucursalServiceLogic oISociedadSucursalServiceLogic;
        private readonly IConfiguracionesService oIConfiguracionesService;

        public SolicitudController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic, IUsersService oIUsersService)
        {
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
            this.oISociedadServiceLogic = oISociedadServiceLogic;
            this.oIUsersService = oIUsersService;
            this.oISolicitudServiceLogic_Gasto = new SolicitudServiceLogic_Gasto();
            this.oIHistorialLogService = new HistorialLogService();
            this.oIProveedorGastoWebApiService = new ProveedorGastoWebApiService();
            this.oIProveedorGastoWebApiServiceLogic = new ProveedorGastoWebApiServiceLogic();
            this.oIUsersGastoService = new UsersGastoService();
            this.oISociedadSucursalServiceLogic = new SociedadSucursalServiceLogic();
            this.oIConfiguracionesService = new ConfiguracionesService();
        }
        [STSUrl]
        public ActionResult Index()
        {
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            var modelo = new SolicitudViewModel();
            modelo.NombreCompleto = UtilSession.Usuario.NombreCompleto;
            modelo.Email = UtilSession.Usuario.Email;
            var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Apellidos} , {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
            var listaSociedad = oISociedadServiceLogic.Get().Where(m => m.CodigoSociedadFI == codigoSociedad).ToList();
            modelo.ListaSociedad = listaSociedad;
            ViewBag.CodigoSociedad = codigoSociedad;
            ViewBag.ListaBeneficiado = listaBeneficado;
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == 0).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;
            var listaTipoSolicitud = new BL_GASTO.TipoSolicitud.TipoSolicitud().List().ToList();
            listaTipoSolicitud.Insert(0, new BE_GASTO.TipoSolicitud() { Tipo = "-SELECCIONE-" });
            ViewBag.listaTipoSolicitud = listaTipoSolicitud;
            var listaConfiguracionAdjunto = oIConfiguracionesService.GetByKey("ADJUNTO").Select(m => m.Valor1).ToList();
            ViewBag.listaConfiguracionAdjunto = listaConfiguracionAdjunto[0];
            //listaConfiguracionAdjunto = listaConfiguracionAdjunto.;
            //listaConfiguracionAdjunto = listaConfiguracionAdjunto.Valor1;

            return View(modelo);
        }
        public async Task<PartialViewResult> Registrar(int IdSolicitud = default(int))
        {
            var modelo = await GetSolicitudViewModel(IdSolicitud);
            return PartialView("_Formulario", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> PagarRendicionPost(int? IdSolicitud = null, string comentario = null, int? envia = null)
        {
            try
            {
                BE_GASTO.RendicionSolicitud entRendicionSolicitud = new BE_GASTO.RendicionSolicitud();
                entRendicionSolicitud.IdSolicitud = IdSolicitud == null ? 0 : (int)IdSolicitud;
                entRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
                entRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
                entRendicionSolicitud.Comentario = comentario == null ? "" : comentario;
                entRendicionSolicitud.Envia = envia == null ? 0 : (int)envia;

                BE.OperationResult objResult = null;
                objResult = new BL_GASTO.Solicitud.Solicitud().RendicionSolicitud(entRendicionSolicitud);

                if (objResult.Success && envia == 1)
                {

                    BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
                    entSolicitud.IdSolicitud = (long)IdSolicitud;
                    entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);

                    string appSetting1;
                    string appSetting2;
                    if (entSolicitud.IdSociedad == "PE04")
                    {
                        appSetting1 = ConfigurationManager.AppSettings["correoEmisorKMA"];
                        appSetting2 = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
                    }
                    else
                    {
                        appSetting1 = ConfigurationManager.AppSettings["correoEmisor"];
                        appSetting2 = ConfigurationManager.AppSettings["contraseniaEmisor"];
                    }

                    var correo = await oISolicitudServiceLogic_Gasto.GetSolicitudCorreo((int)entSolicitud.IdSolicitud);
                    correo.Responsable = new BE_GASTO.Usuario();
                    correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                    correo.ListaBeneficiado = await oISolicitudServiceLogic_Gasto.GetListBeneficiadoBySolicitud(objResult.NewID);

                    BE_GASTO.Solicitud solicitud = new BL_GASTO.Solicitud.Solicitud().DatosEmail((int)entSolicitud.IdSolicitud);
                    string sBody = "";
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(appSetting1));
                    email.To.Add(MailboxAddress.Parse(solicitud.CorreoA));
                    email.Subject = "[Portal de Gastos] - Aprobación de " + solicitud.TipoC + " N° " + solicitud.Codigo + " - Colaborador " + solicitud.ColaboradorC;
                    sBody = "Estimado Aprobador, <br><br>Se informa sobre el " + solicitud.TipoC + " N° " + solicitud.Codigo + " del colaborador " + solicitud.ColaboradorC + " para su respectiva aprobacion. <br><br><br>Para acceder a la plataforma ingrese al link: <a href='https://ca.kmmp.com.pe/'>https://ca.kmmp.com.pe/</a><br><br>Saludos, <br>Cuentas por Pagar <br>Portal de Gastos";
                    email.Body = new TextPart(TextFormat.Html) { Text = sBody };
                    //var result = await oIProveedorGastoWebApiService.EmailService(correo);


                    using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                    {
                        smtp.Connect("smtp.office365.com", int.Parse("587"), SecureSocketOptions.StartTls);
                        smtp.Authenticate(appSetting1, appSetting2);
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        //return true;
                    }


                }

                return Json(objResult);
            }
            catch (Exception Ex)
            {
                return Json(new { Success = false, Message = "Ocurrió un error al procesar al enviar el correo." });
            }
        }
        [HttpPost]
        public async Task<JsonResult> ValidarOrdenServicio(string ordenServicio, string sociedad)
        {
            //sociedad= UtilSession.Usuario.IdSociedad;
            var result = await oIProveedorGastoWebApiServiceLogic.GetOrdenServicio(ordenServicio, sociedad);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> ValidarDatosProveedor(string sociedad, string documento)
        {
            var response = new List<ProveedorResponse>();
            if (documento != "")
            {
                var result = await oIProveedorGastoWebApiService.GetDatosProveedor(sociedad, documento);
                if (result.Success)
                {
                    response = (List<ProveedorResponse>)result.ResponseObject;
                }
            }
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> ValidarDatosProveedorExterno(string documento)
        {
            var response = new BE_GASTO.Proveedor();
            if (documento != "")
            {
                var result = await oISolicitudServiceLogic_Gasto.ValidarDatosProveedorExterno(documento);
                if (result != null)
                {
                    response = result;
                }
            }
            var jsonData = new
            {
                Success = (response != null ? true : false),
                respuesta = response
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        private string DisplayMoneda(string clave)
        {
            string moneda = "";
            if (clave == "PE") moneda = "PEN(S/.)";
            else moneda = "USD($)";
            return moneda;
        }
        private string DisplayMonedaCodigo(string clave)
        {
            string moneda = "";
            if (clave == "PE") moneda = "PEN";
            else moneda = "USD";
            return moneda;
        }
        private string DisplayCuenta(string banco, string cta, string cta_inter)
        {
            string cuenta = "";
            if (banco == "BCP") cuenta = cta;
            else cuenta = cta_inter;
            return cuenta;
        }
        [HttpPost]
        public async Task<ActionResult> ObtenerCuentasBancariasByUser_PV(string sociedad, string codigo, int idtipo_solicitud)
        {
            codigo = codigo == "" ? "0" : codigo;
            BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById(int.Parse(codigo));
            if (oUsuario.ID == 0)
            {
                oUsuario.IDAlternativo = "";
                oUsuario.NumeroDocumento = ""; //EDIAZ 10.03.2020, se pone la cadena vacia: en QAs funcionaba pero en PRD salía error.
            }
            var listaCuentasBancarias = new List<CuentaBancariaApi>();
            //EDIAZ 12.02.2020, se cambia idAlternativo por dni
            //var dict_lista = await oIProveedorGastoWebApiService.GetCuentasBancarias(sociedad, oUsuario.IDAlternativo);
            var dict_lista = await oIProveedorGastoWebApiService.GetCuentasBancarias(sociedad, oUsuario.NumeroDocumento.Trim());
            if (dict_lista.Success)
            {
                var lista = (List<CuentaBancariaApi>)dict_lista.ResponseObject;
                if (codigo != "0")
                {
                    if (lista != null && lista.Count > 0)
                    {
                        //EDIAZ 17.02.2020, se comenta para que muestre todas las cuentas bancarias
                        //if (idtipo_solicitud == 1 || idtipo_solicitud == 2 || idtipo_solicitud == 4 || idtipo_solicitud == 3)
                        //{
                        //    lista = lista.Where(m => m.BVTYP != "FFPE").ToList();
                        //}
                        //else
                        //{
                        //    lista = lista.Where(m => m.BVTYP == "FFPE").ToList();
                        //}

                        listaCuentasBancarias = lista.OrderBy(x => x.BANKL).Select(m => { m.BANKL2 = $"{DisplayMonedaCodigo(m.BVTYP.Substring(2, 2))}-{m.BANKL}-{DisplayCuenta(m.BANKL, m.BANKN, m.BKREF)}"; m.BANKL_DESCRIPCION = $" {DisplayMoneda(m.BVTYP.Substring(2, 2))} {m.BANKL} - {DisplayCuenta(m.BANKL, m.BANKN, m.BKREF)} "; return m; }).Where(m => (m.LOEVM == "" && m.SPERR == "")).ToList();
                    }
                }
            }
            listaCuentasBancarias.Insert(0, new CuentaBancariaApi { BANKL2 = "0", BANKL_DESCRIPCION = "-SELECCIONE-" });
            ViewBag.ListaCuentasBancarias = listaCuentasBancarias;
            return PartialView("_ListaCuentasBancarias");
        }
        public async Task<PartialViewResult> Terminos(int IdUsuario = default(int))
        {
            ViewBag.IdSolicitud = 0;
            BE_GASTO.TerminosCondiciones terminosCondiciones = await oISolicitudServiceLogic_Gasto.GetTerminosCondicionesNew(IdUsuario);
            ViewBag.NombreCompleto = terminosCondiciones.NombreCompleto;
            ViewBag.Documento = terminosCondiciones.Documento;
            ViewBag.Sociedad = terminosCondiciones.Sociedad;


            return PartialView("_TerminosCondiciones");
        }
        [HttpPost]
        public async Task<PartialViewResult> GetHistorialByIdSolicitud(int IdSolicitud)
        {
            var modelo = new ViewModelHistorialLogs();
            modelo.ListaHistorialLogs = await oIHistorialLogService.GetHistorialByIdSolicitud(IdSolicitud);
            return PartialView("_ListHistorialEstadosSolicitud", modelo);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarDetalleGastoPost(int id)
        {

            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = id;
            entDetRendicionSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entDetRendicionSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Eliminar(entDetRendicionSolicitud);
            return Json(objResult);
        }
        [HttpPost]
        public JsonResult EliminarSolicitudPost(int id)
        {
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = id;
            entSolicitud.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            entSolicitud.UserNameRegistro = UtilSession.Usuario.UserName;
            BE.OperationResult objResult = null;
            objResult = new BL_GASTO.Solicitud.Solicitud().Eliminar(entSolicitud);
            return Json(objResult);
        }
        public DetalleSolicitudViewModel getDetalleSolicitud(int IdSolicitud)
        {
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud;
            var modelo = new DetalleSolicitudViewModel();
            entSolicitud.IdSolicitud = IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().RecuperarDetalle(entSolicitud);
            modelo.IdSociedad = entSolicitud.IdSociedad;
            modelo.NumeroDocumento = entSolicitud.NumeroDocumento;
            modelo.FechaRegistroStr = entSolicitud.FechaRegistroStr;
            modelo.TotalRendidoStr = entSolicitud.TotalRendidoStr;
            modelo.FechaContabilidadStr = entSolicitud.FechaContabilidadStr == null ? "---" : entSolicitud.FechaContabilidadStr;
            modelo.NomApe = entSolicitud.NomApe;
            modelo.CECO = entSolicitud.CECO;
            modelo.Tipo = entSolicitud.Tipo;
            modelo.Moneda = entSolicitud.Moneda;
            modelo.Aprobador = entSolicitud.Aprobador;
            modelo.Destino = entSolicitud.Destino;
            modelo.IdSolicitud = entSolicitud.IdSolicitud;
            modelo.Sucursal = entSolicitud.Sucursal;
            modelo.IdTipo = entSolicitud.IdTipo;
            modelo.FechaInicioStr = entSolicitud.FechaInicioStr;
            modelo.FechaFinStr = entSolicitud.FechaFinStr;
            modelo.IdMoneda = entSolicitud.IdMoneda;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.IdDestino = entSolicitud.IdDestino;
            modelo.Motivo = entSolicitud.Motivo;
            modelo.Comentario = entSolicitud.Comentario;
            modelo.ImporteSolicitud = entSolicitud.ImporteSolicitud;
            modelo.ImporteSolicitudStr = entSolicitud.ImporteSolicitudStr;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            modelo.TipoReembolso = entSolicitud.TipoReembolso;
            modelo.UsuarioAprobador = entSolicitud.UsuarioAprobador;
            modelo.NombreFile = entSolicitud.NombreFile;
            modelo.CtaBancaria = entSolicitud.CtaBancaria;
            modelo.Codigo = entSolicitud.Codigo;
            modelo.NombreCO = entSolicitud.NombreCO;
            ViewBag.IdTipo = entSolicitud.IdTipo;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            ViewBag.IdDestino = entSolicitud.IdDestino;
            ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
            return modelo;
        }

        [HttpPost]
        public async Task<PartialViewResult> GetSolicitudDetalleByIdSolicitud(int IdSolicitud)
        {
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            return PartialView("_FormularioDetalle", modelo);
        }

        public List<Data.CentroCosto> GetCentrosCostos(string codigoSociedad)
        {
            var listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == codigoSociedad).ToList();
            listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == codigoSociedad).ToList().Select(t => new Data.CentroCosto()
            {
                Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                Codigo = t.Codigo,
            }).ToList();
            listaCentrosCostos.Insert(0, new Data.CentroCosto() { Codigo = "0", Descripcion = "-SELECCIONE-" });
            return listaCentrosCostos;
        }

        public ActionResult ListaCentrosCostos(string codsociedad)
        {
            var ListaCentrosCostos = GetCentrosCostos(codsociedad).ToList();
            ViewBag.ListaCentrosCostos = ListaCentrosCostos;
            return PartialView("_ListaCentroCosto");
        }



        [HttpPost]
        public async Task<PartialViewResult> RegistroDetalleGastoSolicitud(int IdDetRendicionSolicitud = default(int), int IdMoneda = default(int), int idtipo = default(int), string idsociedad = default(string))
        {
            var modelo = new DetalleSolicitudViewModel();
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud;
            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.IdDetRendicionSolicitud = 0;
            var listaTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).ToList().Where(x => x.Estado == 1).ToList();
            listaTipoDocumento.Insert(0, new BE_GASTO.TipoDocumento() { Denominacion = "-SELECCIONE-" });
            ViewBag.listaTipoDocumento = listaTipoDocumento;

            var listaTipoGastoViaje = new BL_GASTO.TipoGastosViaje.TipoGastosViaje().List().ToList().Where(x => x.Estado == "A").ToList();
            listaTipoGastoViaje.Insert(0, new BE_GASTO.TipoGastosViaje() { TipoGastoViaje = "-SELECCIONE-" });
            ViewBag.listaTipoGastoViaje = listaTipoGastoViaje;

            var listaMoneda = new BL_GASTO.TipoMoneda.TipoMoneda().List().ToList();
            ViewBag.listaMoneda = listaMoneda;

            var lstPais = new BL_GASTO.Pais.Pais().List().Where(x => x.Estado == "A" && x.IdPais != 1).ToList();
            var listaPais = from p in lstPais
                            orderby p.Descripcion ascending
                            select p;
            ViewBag.listaPais = listaPais;

            List<BE_GASTO.Tratamiento> listaTratamiento = new List<BE_GASTO.Tratamiento>();
            BE_GASTO.Tratamiento tratamiento = new BE_GASTO.Tratamiento();
            tratamiento.ID = "Empresa";
            tratamiento.Denominacion = "Empresa";
            listaTratamiento.Add(tratamiento);
            ViewBag.listaTratamiento = listaTratamiento;
            ViewBag.IsExterno = "0";


            var listaCentrosCostos = new List<Data.CentroCosto>();
            listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == idsociedad).ToList();
            listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == idsociedad).ToList().Select(t => new Data.CentroCosto()
            {
                Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                Codigo = t.Codigo,
            }).ToList();
            listaCentrosCostos.Insert(0, new Data.CentroCosto() { Codigo = "0", Descripcion = "-SELECCIONE-" });
            ViewBag.ListaCentrosCostos = listaCentrosCostos;
            modelo.DetRendicionSolicitud.CentroCostoAfectoCodigoSap = "0";
            if (IdDetRendicionSolicitud != default(int))
            {
                modelo.DetRendicionSolicitud = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalle(entDetRendicionSolicitud);
                ViewBag.IsExterno = modelo.DetRendicionSolicitud.Externo ? "1" : "0";
                if (modelo.DetRendicionSolicitud.NroComprobante != null || modelo.DetRendicionSolicitud.NroComprobante != "")
                {
                    string[] nro = modelo.DetRendicionSolicitud.NroComprobante.Split('-');
                    modelo.DetRendicionSolicitud.NroIzquierda = nro[0];
                    modelo.DetRendicionSolicitud.NroCentro = nro[1];
                    if (modelo.DetRendicionSolicitud.IdTipoDocumento != 11 && modelo.DetRendicionSolicitud.IdTipoDocumento != 27 && modelo.DetRendicionSolicitud.IdTipoDocumento != 28 && modelo.DetRendicionSolicitud.IdTipoDocumento != 29 && modelo.DetRendicionSolicitud.IdTipoDocumento != 30)
                    {
                        modelo.DetRendicionSolicitud.NroDerecha = nro[2];
                    }
                }
                modelo.DetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud;
                var lstDocumentoImpuestos = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().Where(x => x.IdTipoDocumento == (long)modelo.DetRendicionSolicitud.IdTipoDocumento).ToList();
                var lstDocumentoImpuestos2 = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().ToList();
                var lstImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
                var lstTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).Where(x => x.IdTipoDocumento == modelo.DetRendicionSolicitud.IdTipoDocumento).ToList();
                ViewBag.IdClase = "";
                if (lstTipoDocumento.Count > 0)
                {
                    ViewBag.IdClase = lstTipoDocumento[0].Clase;
                }
                var listaImpuesto = (from i in lstImpuesto
                                     join id in lstDocumentoImpuestos2
                                    on i.IdImpuesto equals id.IdImpuesto
                                     select new
                                     {
                                         i.IdImpuesto,
                                         i.DescripcionImpuesto,
                                     }).Distinct().ToList();

                listaImpuesto.Clear();
                listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();
                ViewBag.IdImpuestoFirst = modelo.DetRendicionSolicitud.IdImpuesto;
                ViewBag.listaImpuesto = listaImpuesto;
                if (modelo.DetRendicionSolicitud.NombreFile.Trim() == "")
                {
                    Session["ListaAdjuntoDG"] = null;
                }
            }
            else
            {
                Session["ListaAdjuntoDG"] = null;
                var listaImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
                ViewBag.IdImpuestoFirst = listaImpuesto.FirstOrDefault().IdImpuesto;
                ViewBag.listaImpuesto = listaImpuesto;
                modelo.DetRendicionSolicitud.BaseInafectaStr = "0";
                modelo.DetRendicionSolicitud.TipoCambio = 1;
                if (IdMoneda != default(int)) modelo.DetRendicionSolicitud.IdMoneda = IdMoneda;
            }
            return PartialView("_FormularioDetalleGasto", modelo);
        }


        [HttpPost]
        public async Task<PartialViewResult> GetDetalleRendirGastoByIdSolicitud(int IdSolicitud)
        {
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdRendicionSolicitud = IdSolicitud;
            entDetRendicionSolicitud.PageSize = 1;
            entDetRendicionSolicitud.PageIndex = 10;
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            modelo.IdSolicitud = IdSolicitud;

            BE_GASTO.RendicionSolicitud entRendicion = new BE_GASTO.RendicionSolicitud();
            entRendicion.IdSolicitud = IdSolicitud;
            modelo.RendicionSolicitud = new BL_GASTO.RendicionSolicitud.RendicionSolicitud().RecuperarRendicion(entRendicion);
            if (modelo.RendicionSolicitud == null)
            {
                modelo.RendicionSolicitud = new BE_GASTO.RendicionSolicitud();
                modelo.RendicionSolicitud.EstadoRendicion = "Pendiente Rendir";
            }
            return PartialView("_ListDetalleRendicionGastos", modelo);
        }
        [HttpPost]
        public async Task<PartialViewResult> GetDetalleRendirGastoByIdSolicitudSD(int IdSolicitud)
        {
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdRendicionSolicitud = IdSolicitud;
            entDetRendicionSolicitud.PageSize = 1;
            entDetRendicionSolicitud.PageIndex = 10;
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            modelo.IdSolicitud = IdSolicitud;
            BE_GASTO.RendicionSolicitud entRendicion = new BE_GASTO.RendicionSolicitud();
            entRendicion.IdSolicitud = IdSolicitud;
            modelo.RendicionSolicitud = new BL_GASTO.RendicionSolicitud.RendicionSolicitud().RecuperarRendicion(entRendicion);
            if (modelo.RendicionSolicitud == null)
            {
                modelo.RendicionSolicitud = new BE_GASTO.RendicionSolicitud();
                modelo.RendicionSolicitud.EstadoRendicion = "Pendiente Rendir";
            }
            return PartialView("_ListDetalleRendicionGastosSD", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerTipoCambioRFC(string fecha)
        {
            var tipoCambio = new TipoCambioApi();
            var response = new TipoCambioApi();
            tipoCambio.KURST = "M";
            tipoCambio.FCURR = "USD";
            tipoCambio.TCURR = "PEN";
            tipoCambio.GDATU = (DateTime.Parse(fecha)).ToString("dd.MM.yyyy");
            var dict_lista = await oIProveedorGastoWebApiService.PostTipoCambio(tipoCambio);
            if (dict_lista.Success)
            {
                var lista = (List<TipoCambioApi>)dict_lista.ResponseObject;
                if (lista != null && lista.Count > 0)
                {
                    response = lista[0];
                }
            }
            var jsonData = new
            {
                Success = (response != null ? true : false),
                TipoCambio = response
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public JsonResult GetListDetalleRendirGasto(int? IdSolicitud = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;
            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;
            BE_GASTO.DetRendicionSolicitud entRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entRendicionSolicitud.PageSize = rows;
            entRendicionSolicitud.PageIndex = page;
            entRendicionSolicitud.IdRendicionSolicitud = (long)IdSolicitud;
            var lista = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Paginado(entRendicionSolicitud, out intTotalRows, out intCantidadRegistros);
            var listafull = GetListDetalleRendirGastofull(IdSolicitud, page, rows);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,
                rowsfull = listafull
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListDetalleRendirGastofull(int? IdSolicitud = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.DetRendicionSolicitud entRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entRendicionSolicitud.PageSize = 999;
            entRendicionSolicitud.PageIndex = 1;
            entRendicionSolicitud.IdRendicionSolicitud = (long)IdSolicitud;

            var lista = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Paginado(entRendicionSolicitud, out intTotalRows, out intCantidadRegistros);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaSucursalesBySociedad_PV(string idsociedad)
        {
            var listaSucursales = oISociedadSucursalServiceLogic.ListarByIdSociedad(idsociedad);
            listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursales;
            return PartialView("_ListaSucursalesBySociedad");
        }

        private async Task<SolicitudViewModel> GetSolicitudViewModel(int IdSolicitud)
        {
            var modelo = new SolicitudViewModel();
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            var codigoSociedad = UtilSession.Usuario.IdSociedad;
            modelo.NombreCompleto = UtilSession.Usuario.NombreCompleto;
            modelo.Email = UtilSession.Usuario.Email;
            var listaTipoSolicitud = new BL_GASTO.TipoSolicitud.TipoSolicitud().List().ToList();
            ViewBag.listaTipoSolicitud = listaTipoSolicitud;
            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "", NombreCO = "-SELECCIONE-" });
            ViewBag.listaSociedad = listaSociedad;
            var listaAprobadores = this.GetUsuariosAprobadores(string.Empty);
            ViewBag.ListaAprobadores = listaAprobadores;
            var listaAprobadoresCaja = this.GetUsuariosAprobadoresBySucursal("", 0);
            ViewBag.ListaAprobadoresCaja = listaAprobadoresCaja;
            //var listaSucursales = new BL_GASTO.Sucursal.Sucursal().List().ToList();
            //listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            //ViewBag.ListaSucursales = listaSucursales;
            var listaSucursalesBySociedad = new List<BE_GASTO.Sucursal>();


            var listaAmbitoViaje = new BL_GASTO.AmbitoViaje.AmbitoViaje().List().ToList();
            ViewBag.listaAmbitoViaje = listaAmbitoViaje;
            var listaTipoReembolso = new BL_GASTO.TipoReembolso.TipoReembolso().List().ToList();
            ViewBag.listaTipoReembolso = listaTipoReembolso;
            var listaCentrosCostos = new List<Data.CentroCosto>();
            if (IdSolicitud != default(int))
            {

                entSolicitud.IdSolicitud = IdSolicitud;
                entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);
                codigoSociedad = entSolicitud.IdSociedad;
                var listaBeneficado = new List<Data.Common.Usuario>();
                if (entSolicitud.IdTipo != 3)
                    listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Nombres}"; return m; }).ToList();
                else
                    listaBeneficado = oIUsersService.GetList().OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Nombres}"; return m; }).ToList();


                listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
                ViewBag.CodigoSociedad = codigoSociedad;
                modelo.IdSociedad = codigoSociedad;
                ViewBag.ListaBeneficiado = listaBeneficado;
                modelo.FechaRegistroStr = entSolicitud.FechaRegistroStr;
                listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == entSolicitud.IdSociedad).ToList();
                listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == codigoSociedad).ToList().Select(t => new Data.CentroCosto()
                {
                    Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                    Codigo = t.Codigo,
                }).ToList();
                listaCentrosCostos.Insert(0, new Data.CentroCosto() { Codigo = "0", Descripcion = "-SELECCIONE-" });

                BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetById((int)entSolicitud.IdUsuario);
                if (oUsuario.ID == 0)
                {
                    oUsuario.IDAlternativo = "";
                }
                var listaCuentasBancarias = new List<CuentaBancariaApi>();
                //EDIAZ 13.02.2020, se cambia IdAlternativo por dni
                //var dict_lista = await oIProveedorGastoWebApiService.GetCuentasBancarias(modelo.IdSociedad, oUsuario.IDAlternativo);
                var dict_lista = await oIProveedorGastoWebApiService.GetCuentasBancarias(modelo.IdSociedad, oUsuario.NumeroDocumento);
                if (dict_lista.Success)
                {
                    var lista = (List<CuentaBancariaApi>)dict_lista.ResponseObject;

                    if (lista != null && lista.Count > 0)
                    {
                        if (entSolicitud.IdTipo == 1 || entSolicitud.IdTipo == 2 || entSolicitud.IdTipo == 4)
                        {
                            lista = lista.Where(m => m.BVTYP != "FFPE").ToList();
                        }
                        else
                        {
                            lista = lista.Where(m => m.BVTYP == "FFPE").ToList();
                        }
                        listaCuentasBancarias = lista.OrderBy(x => x.BANKL).Select(m => { m.BANKL2 = $"{DisplayMonedaCodigo(m.BVTYP.Substring(2, 2))}-{m.BANKL}-{DisplayCuenta(m.BANKL, m.BANKN, m.BKREF)}"; m.BANKL_DESCRIPCION = $" {DisplayMoneda(m.BVTYP.Substring(2, 2))} {m.BANKL} - {DisplayCuenta(m.BANKL, m.BANKN, m.BKREF)} "; return m; }).Where(m => (m.LOEVM == "" && m.SPERR == "")).ToList();
                    }
                }
                listaCuentasBancarias.Insert(0, new CuentaBancariaApi { BANKL2 = "0", BANKL_DESCRIPCION = "-SELECCIONE-" });
                ViewBag.ListaCuentasBancarias = listaCuentasBancarias;
                //var lista1 = oICentroCostoServiceLogic.Get();
                //modelo.ListaCentroCosto = lista1.Where(m => m.Soc == codigoSociedad).OrderBy(x => x.Descripcion).ToList();
                //modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
                if (entSolicitud.IdTipo == 1 || entSolicitud.IdTipo == 2 || entSolicitud.IdTipo == 4)
                {
                    listaAprobadores = GetUsuariosAprobadores(entSolicitud.CentroCostoAfectoCodigoSap);
                    ViewBag.ListaAprobadores = listaAprobadores;
                }
                else
                {
                    listaAprobadoresCaja = GetUsuariosAprobadoresBySucursal(entSolicitud.IdSociedad, (int)entSolicitud.IdSucursal);
                    ViewBag.ListaAprobadoresCaja = listaAprobadoresCaja;
                }




                modelo.IdUsuario = entSolicitud.IdUsuario;
                modelo.IdSolicitud = entSolicitud.IdSolicitud;
                modelo.IdSucursal = entSolicitud.IdSucursal;
                modelo.IdTipo = entSolicitud.IdTipo;
                modelo.FechaInicioStr = entSolicitud.FechaInicioStr;
                modelo.FechaFinStr = entSolicitud.FechaFinStr;
                modelo.IdMoneda = entSolicitud.IdMoneda;
                modelo.Comentario = entSolicitud.Comentario;
                modelo.IdDestino = entSolicitud.IdDestino;
                modelo.Motivo = entSolicitud.Motivo;
                modelo.Comentario = entSolicitud.Comentario;
                modelo.ImporteSolicitud = entSolicitud.ImporteSolicitud;
                modelo.IdUsuarioAprobador = entSolicitud.IdUsuarioAprobador == null ? 0 : entSolicitud.IdUsuarioAprobador;
                modelo.IdTipoReembolso = entSolicitud.IdTipoReembolso;
                modelo.IdSociedad = entSolicitud.IdSociedad;
                modelo.Codigo = entSolicitud.Codigo;
                ViewBag.CodigoSociedad = entSolicitud.IdSociedad;
                ViewBag.CodCentroCosto = entSolicitud.CentroCostoAfectoCodigoSap;
                ViewBag.IdAprobador = entSolicitud.IdAprobador;
                ViewBag.CtaBancaria = entSolicitud.CtaBancaria;
                ViewBag.IdSolicitud = IdSolicitud;
                ViewBag.IdTipo = entSolicitud.IdTipo;
                ViewBag.IdDestino = entSolicitud.IdDestino;
                ViewBag.IdTipoReembolso = entSolicitud.IdTipoReembolso;
                ViewBag.IdSucursalBySociedad = entSolicitud.IdSucursal;

            }
            else
            {

                listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == UtilSession.Usuario.IdSociedad).ToList();
                listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == codigoSociedad).ToList().Select(t => new Data.CentroCosto()
                {
                    Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                    Codigo = t.Codigo,
                }).ToList();
                listaCentrosCostos.Insert(0, new Data.CentroCosto() { Codigo = "0", Descripcion = "-SELECCIONE-" });


                var listaBeneficado = oIUsersService.GetBySociedad(codigoSociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Nombres}"; return m; }).ToList();
                listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
                ViewBag.CodigoSociedad = codigoSociedad;
                modelo.IdUsuario = 0;
                ViewBag.ListaBeneficiado = listaBeneficado;
                //var lista = oICentroCostoServiceLogic.Get();
                //modelo.ListaCentroCosto = lista.Where(m => m.Soc == codigoSociedad).ToList();
                //modelo.ListaCentroCosto.Select(m => { m.Descripcion = $"{m.Codigo} - {m.Descripcion}"; return m; }).ToList();
                modelo.FechaRegistroStr = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.CodCentroCosto = "0";
                ViewBag.IdAprobador = "0";
                modelo.Codigo = "";
                modelo.IdSociedad = codigoSociedad;
                List<CuentaBancariaApi> lista_ = new List<CuentaBancariaApi>();
                CuentaBancariaApi cta = new CuentaBancariaApi { BANKL2 = "0", BANKL_DESCRIPCION = "-SELECCIONE-" };
                lista_.Add(cta);
                ViewBag.ListaCuentasBancarias = lista_;
                ViewBag.IdSolicitud = "0";
                ViewBag.IdTipo = "1";
                ViewBag.IdDestino = "1";
                ViewBag.IdTipoReembolso = "1";
                ViewBag.CtaBancaria = "0";
                ViewBag.IdSucursalBySociedad = "0";

            }
            listaSucursalesBySociedad = oISociedadSucursalServiceLogic.ListarByIdSociedad(codigoSociedad);
            listaSucursalesBySociedad.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursalesBySociedad;
            ViewBag.ListaCentrosCostos = listaCentrosCostos;
            return modelo;
        }
        public List<BE.UsuarioCentroCosto> GetUsuariosAprobadores(string ceco)
        {
            var listaAprobadores = new BL_GASTO.Solicitud.Solicitud().List_UsuarioByCentroCosto(ceco).ToList();
            listaAprobadores.Insert(0, new BE.UsuarioCentroCosto() { NombresApellido = "-SELECCIONE-" });
            return listaAprobadores;
        }

        public List<BE.UsuarioCentroCosto> GetUsuariosAprobadoresBySucursal(string idsociedad, int idsucursal)
        {
            var listaAprobadores = new BL_GASTO.Solicitud.Solicitud().List_UsuarioBySucursal(idsociedad, idsucursal).ToList();
            listaAprobadores.Insert(0, new BE.UsuarioCentroCosto() { NombresApellido = "-SELECCIONE-" });
            return listaAprobadores;
        }

        public ActionResult ListaBeneficiarios_PV(string idsociedad, int idtipo)
        {
            var listaBeneficado = new List<Data.Common.Usuario>();
            if (idtipo != 3)
                listaBeneficado = oIUsersService.GetBySociedad(idsociedad).OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Nombres}"; return m; }).ToList();
            else
                listaBeneficado = oIUsersService.GetList().OrderBy(x => x.Apellidos).Select(m => { m.Nombres = $"{m.NumeroDocumento} - {m.Nombres}"; return m; }).ToList();
            listaBeneficado.Insert(0, new Data.Common.Usuario() { Nombres = "-SELECCIONE-" });
            ViewBag.ListaBeneficiado = listaBeneficado;
            return PartialView("_ListaBeneficiario");
        }

        public ActionResult ListaImpuestos_PV(int IdTipoDocumento, int idtipo)
        {
            var lstDocumentoImpuestos = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().Where(x => x.IdTipoDocumento == (long)IdTipoDocumento).ToList();
            var lstDocumentoImpuestos2 = new BL_GASTO.TipoDocumentoImpuesto.TipoDocumentoImpuesto().List().ToList();
            var lstImpuesto = new BL_GASTO.Impuesto.Impuesto().List().ToList().OrderBy(x => x.IndicadorImpuesto).Select(m => { m.DescripcionImpuesto = $"{m.IndicadorImpuesto} - {m.DescripcionImpuesto}"; return m; }).ToList();
            var lstTipoDocumento = new BL_GASTO.TipoDocumento.TipoDocumento().List(idtipo).Where(x => x.IdTipoDocumento == IdTipoDocumento).ToList();
            ViewBag.IdClase = "";
            if (lstTipoDocumento.Count > 0)
            {
                ViewBag.IdClase = lstTipoDocumento[0].Clase;
            }
            var listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos2
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();
            if (IdTipoDocumento != 0)
            {
                listaImpuesto.Clear();
                listaImpuesto = (from i in lstImpuesto
                                 join id in lstDocumentoImpuestos
                                on i.IdImpuesto equals id.IdImpuesto
                                 select new
                                 {
                                     i.IdImpuesto,
                                     i.DescripcionImpuesto,
                                 }).Distinct().ToList();
            }
            ViewBag.IdImpuestoFirst = listaImpuesto.FirstOrDefault().IdImpuesto;
            ViewBag.listaImpuesto = listaImpuesto;
            return PartialView("_ListaImpuestos");
        }

        public ActionResult ListaEstadosPorTipo_PV(int idtipo)
        {
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == idtipo).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;
            return PartialView("_ListaEstadosPorTipo");
        }
        public ActionResult ListaEstadosPorTipo_PVSD(int idtipo)
        {
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == idtipo).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;
            return PartialView("_ListaEstadosPorTipoSD");
        }

        public ActionResult ListaAprobadores_PV(string ceco)
        {
            var listaAprobadores = GetUsuariosAprobadores(ceco);
            ViewBag.ListaAprobadores = listaAprobadores;
            return PartialView("_ListaUsuarioCentroCosto");
        }

        public ActionResult ListaAprobadoresCaja_PV(string idsociedad, int idsucursal)
        {
            var listaAprobadoresCaja = GetUsuariosAprobadoresBySucursal(idsociedad, idsucursal);
            ViewBag.ListaAprobadoresCaja = listaAprobadoresCaja;
            return PartialView("_ListaUsuarioSucursal");
        }

        [HttpGet]
        public JsonResult GetListSolicitud(Int64? idusuario = null, int? idtipo = null, string fechaDesde = "", string fechaHasta = "", string idcentrocosto = null, int? idestado = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intCantidadRegistros = 0;

            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;


            //var Usuario = oIUsersGastoService.GetUsuarioByUserName(strUserNameRegistro);
            BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetUsuarioByUserName(strUserNameRegistro);
            string UsuarioSociedad = UtilSession.Usuario.IdSociedad;
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            //entSolicitud.IdUsuarioR = UtilSession.Usuario.Id;
            entSolicitud.IdUsuarioR = oUsuario.ID;
            entSolicitud.IdUsuario = idusuario == null ? 0 : idusuario;
            //entSolicitud.IdUsuario =  oUsuario.ID;
            entSolicitud.IdTipo = idtipo == null ? 0 : (int)idtipo;
            entSolicitud.IdSituacionServicio = idestado == null ? 0 : (int)idestado;
            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.CentroCostoAfectoCodigoSap = idcentrocosto == null ? "" : idcentrocosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;

            var lista = new BL_GASTO.Solicitud.Solicitud().Paginado(entSolicitud, UsuarioSociedad, out intTotalRows, out intCantidadRegistros);
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> ValidarCentroCosto(string codigoCentroCosto)
        {
            var result = await oICentroCostoServiceLogic.ValidarCentroCosto(codigoCentroCosto);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ValidarCreacionAnticipo(int idbeneficiario, decimal ImporteSolicitud, int IdSolicitud, string fecha, int idmoneda)
        {

            double tcambio = 1;
            if (idmoneda == 1)
            {
                var tipocambio = await GetTipoCambio(fecha);
                tcambio = tipocambio.UKURS;
            }
            var result = await oISolicitudServiceLogic_Gasto.ValidarCreacionAnticipo(idbeneficiario, ImporteSolicitud, IdSolicitud, tcambio);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ValidarNumeroComprobanteDuplicado(string ruc, int idtipodocumento, string numerocomprobante, int idrendicion)
        {
            var result = await oISolicitudServiceLogic_Gasto.ValidarNumeroComprobanteDuplicado(ruc, idtipodocumento, numerocomprobante, idrendicion);
            return Json(result);
        }

        public async Task<TipoCambioApi> GetTipoCambio(string fecha)
        {
            var tipoCambio = new TipoCambioApi();
            var response = new TipoCambioApi();
            tipoCambio.KURST = "M";
            tipoCambio.FCURR = "USD";
            tipoCambio.TCURR = "PEN";
            tipoCambio.GDATU = (DateTime.Parse(fecha)).ToString("dd.MM.yyyy");
            var dict_lista = await oIProveedorGastoWebApiService.PostTipoCambio(tipoCambio);
            if (dict_lista.Success)
            {
                var lista = (List<TipoCambioApi>)dict_lista.ResponseObject;
                if (lista != null && lista.Count > 0)
                {
                    response = lista[0];
                }
            }
            return response;
        }


        [HttpPost]
        public async Task<JsonResult> RegistrarPost(BE_GASTO.Solicitud element, int idAprobador)
        {
            try
            {
                var Usuario = UtilSession.Usuario;

                var strUserNameRegistro = UtilSession.Usuario.UserName;

                BE_GASTO.Usuario oUsuario = oIUsersGastoService.GetUsuarioByUserName(strUserNameRegistro);

                element.IdUsuarioM = 0;
                element.IdUsuarioR = oUsuario.ID;
                //element.IdUsuarioR = UtilSession.Usuario.Id;
                element.IdTipo = (element.IdTipo == 0 ? null : element.IdTipo);
                element.IdDestino = element.IdDestino;
                element.FechaSalida = Convert.ToDateTime(element.FechaDesde);
                element.FechaRetorno = (element.IdTipo == 3 || element.IdTipo == 4) ? Convert.ToDateTime(element.FechaDesde) : Convert.ToDateTime(element.FechaHasta);
                element.Comentario = element.Comentario;
                element.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
                element.ImporteSolicitud = Convert.ToDecimal(element.ImporteSolicitud == null ? default(decimal) : element.ImporteSolicitud);
                element.IdSociedad = element.IdSociedad == null ? "" : element.IdSociedad;
                element.Observaciones = element.Observaciones == "" ? null : element.Observaciones;
                element.UsuarioRegistro = Usuario.NombreCompleto;
                element.UserNameRegistro = Usuario.UserName;
                element.Motivo = element.Motivo == null ? "" : element.Motivo;
                element.Comentario = element.Comentario == null ? "" : element.Comentario;
                element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjunto"];
                element.IdOrdenServicio = element.IdOrdenServicio;

                string fecha = element.FechaRegistroStr;
                int idmoneda = element.IdMoneda == null ? 2 : (int)element.IdMoneda;
                decimal tcambio = 1;
                if (idmoneda == 1)
                {
                    var tipocambio = await GetTipoCambio(fecha);
                    tcambio = (decimal)tipocambio.UKURS;
                }
                element.TipoCambio = tcambio;
                BE.OperationResult objResult = new BL_GASTO.Solicitud.Solicitud().Grabar(element);

                if (objResult.Success)
                {
                    Session["ListaAdjunto"] = null;
                    if (!(element.IdTipo == 2 || element.IdTipo == 3 || element.IdTipo == 4))
                    {
                        string appSetting1;
                        string appSetting2;
                        if (element.IdSociedad == "PE04")
                        {
                            appSetting1 = ConfigurationManager.AppSettings["correoEmisorKMA"];
                            appSetting2 = ConfigurationManager.AppSettings["contraseniaEmisorKMA"];
                        }
                        else
                        {
                            appSetting1 = ConfigurationManager.AppSettings["correoEmisor"];
                            appSetting2 = ConfigurationManager.AppSettings["contraseniaEmisor"];
                        }

                        var correo = await oISolicitudServiceLogic_Gasto.GetSolicitudCorreo(objResult.NewID);
                        correo.Responsable = new BE_GASTO.Usuario();
                        correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                        correo.ListaBeneficiado = await oISolicitudServiceLogic_Gasto.GetListBeneficiadoBySolicitud(objResult.NewID);
                        correo.IdTipoCorreo = 1;

                        BE_GASTO.Solicitud solicitud = new BL_GASTO.Solicitud.Solicitud().DatosEmail(objResult.NewID);
                        //MailMessage message = new MailMessage();
                        //message.From = new MailAddress(appSetting1);
                        //message.To.Add(solicitud.CorreoA);
                        //message.Subject = "[Portal de Gastos] - Aprobación de " + solicitud.TipoC + " N° " + solicitud.Codigo + " - Colaborador " + solicitud.ColaboradorC;
                        //message.Body = "Estimado Aprobador, <br><br>Se informa sobre la " + solicitud.TipoC + " N° " + solicitud.Codigo + " del colaborador " + solicitud.ColaboradorC + " para su respectiva aprobacion. <br><br><br>Para acceder a la plataforma ingrese al link: <a href='https://ca.kmmp.com.pe/'>https://ca.kmmp.com.pe/</a><br><br>Saludos, <br>Cuentas por Pagar <br>Portal de Gastos";
                        //message.IsBodyHtml = true;

                        //SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                        //smtpClient.EnableSsl = true;
                        //smtpClient.Credentials = new System.Net.NetworkCredential(appSetting1, appSetting2);

                        //smtpClient.Send(message);


                        //smtpClient.Dispose(); 
                        string sBody = "";
                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse(appSetting1));
                        email.To.Add(MailboxAddress.Parse(solicitud.CorreoA));
                        email.Subject = "[Portal de Gastos] - Aprobación de " + solicitud.TipoC + " N° " + solicitud.Codigo + " - Colaborador " + solicitud.ColaboradorC;
                        sBody = "Estimado Aprobador, <br><br>Se informa sobre la " + solicitud.TipoC + " N° " + solicitud.Codigo + " del colaborador " + solicitud.ColaboradorC + " para su respectiva aprobacion. <br><br><br>Para acceder a la plataforma ingrese al link: <a href='https://ca.kmmp.com.pe/'>https://ca.kmmp.com.pe/</a><br><br>Saludos, <br>Cuentas por Pagar <br>Portal de Gastos";
                        email.Body = new TextPart(TextFormat.Html) { Text = sBody };
                        var result = await oIProveedorGastoWebApiService.EmailService(correo);


                        using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                        {
                            smtp.Connect("smtp.office365.com", int.Parse("587"), SecureSocketOptions.StartTls);
                            smtp.Authenticate(appSetting1, appSetting2);
                            smtp.Send(email);
                            smtp.Disconnect(true);
                            //return true;
                        }
                    }

                }

                return Json(objResult);

            }
            catch (Exception Ex)
            {
                return Json(new { Success = false, Message = "Ocurrió un error al procesar al enviar el correo." });
            }
        }


        [HttpPost]
        public async Task<JsonResult> ActualizarPost(BE_GASTO.Solicitud element, int idAprobador)
        {
            var Usuario = UtilSession.Usuario;
            element.IdUsuarioM = UtilSession.Usuario.Id;
            element.IdTipo = (element.IdTipo == 0 ? null : element.IdTipo);
            element.IdDestino = element.IdDestino;
            element.FechaSalida = Convert.ToDateTime(element.FechaDesde);
            element.FechaRetorno = (element.IdTipo == 3 || element.IdTipo == 4) ? Convert.ToDateTime(element.FechaDesde) : Convert.ToDateTime(element.FechaHasta);
            element.Comentario = element.Comentario;
            element.CentroCostoAfectoCodigoSap = element.CentroCostoAfectoCodigoSap;
            element.ImporteSolicitud = Convert.ToDecimal(element.ImporteSolicitud == null ? default(decimal) : element.ImporteSolicitud);
            element.IdSociedad = element.IdSociedad == null ? "" : element.IdSociedad;
            element.Observaciones = element.Observaciones == "" ? null : element.Observaciones;
            element.UsuarioRegistro = Usuario.NombreCompleto;
            element.UserNameRegistro = Usuario.UserName;
            element.Motivo = element.Motivo == null ? "" : element.Motivo;
            element.Comentario = element.Comentario == null ? "" : element.Comentario;
            element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjunto"];
            BE.OperationResult objResult = new BL_GASTO.Solicitud.Solicitud().Actualizar(element);

            if (objResult.Success)
            {
                Session["ListaAdjunto"] = null;
                //var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                //correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                //correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                //correo.Responsable = new Data.Common.Usuario();
                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                //correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                //correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                //var result = await oIProveedorWebApiService.EmailService(correo);
            }
            return Json(objResult);
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarDetRendicionSolicitudPost(BE_GASTO.DetRendicionSolicitud element)
        {
            var Usuario = UtilSession.Usuario;
            element.IdUsuarioM = UtilSession.Usuario.Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjuntoDG"];
            BE.OperationResult objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Grabar(element);
            if (objResult.Success)
            {
                Session["ListaAdjuntoDG"] = null;
                //var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                //correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                //correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                //correo.Responsable = new Data.Common.Usuario();
                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                //correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                //correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                //var result = await oIProveedorWebApiService.EmailService(correo);
            }
            return Json(objResult);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarDetRendicionSolicitudPost(BE_GASTO.DetRendicionSolicitud element)
        {
            var Usuario = UtilSession.Usuario;
            element.IdUsuarioM = UtilSession.Usuario.Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            element.ListaAdjunto = (List<BE_GASTO.Adjunto>)Session["ListaAdjuntoDG"];
            BE.OperationResult objResult = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().Actualizar(element);

            if (objResult.Success)
            {
                Session["ListaAdjuntoDG"] = null;
                //var correo = await oISolicitudService.GetSolicitudCorreo(objResult.NewID);
                //correo.IdTipoCorreo = (int)TipoCorreoEnum.Registrado;
                //correo.Mensaje = ConfigurationManager.AppSettings["mensajeRegistro"].Replace("{0}", correo.Codigo);
                //correo.Responsable = new Data.Common.Usuario();
                //correo.Responsable.Email = Util.UtilSession.Usuario.Email;
                //correo.ListaEmailAdjunto = await oIEmailService.GetListEmailBySolicitud(objResult.NewID, null, (int)TipoCorreoEnum.Registrado);
                //correo.ListaBeneficiado = await oISolicitudService.GetListBeneficiadoBySolicitud(objResult.NewID);
                //var result = await oIProveedorWebApiService.EmailService(correo);
            }
            return Json(objResult);
        }
        public ActionResult UploadFile(HttpPostedFileBase file, int IdSolicitud)
        {
            try
            {
                string absolutePath = AppCode.Util.ServerPath("~/FilesCotizacionSolicitudes");
                var nombreArchivo = file.FileName.Split('.')[0];
                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }
                string alias = string.Concat(nombreArchivo, '-', IdSolicitud, Path.GetExtension(file.FileName));
                string rutaCompleta = Path.Combine(absolutePath, alias);
                file.SaveAs(rutaCompleta);
                var adjunto = new BE_GASTO.Adjunto() { Alias = alias, NombreAdjunto = file.FileName, PathAdjunto = rutaCompleta };
                var adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };
                Session["ListaAdjunto"] = adjuntoList;
                var modelo = new SolicitudViewModel { ListaAdjunto = adjuntoList };
                var view = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_Attachment", modelo);
                return Json(new { code = 1, message = String.Empty, partial = view });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = ex.Message, partial = string.Empty });
            }
        }

        public ActionResult UploadFileDG(HttpPostedFileBase file, int IdDetRendicionSolicitud)
        {
            try
            {
                string absolutePath = AppCode.Util.ServerPath("~/FilesDetallesGastos");
                var nombreArchivo = file.FileName.Split('.')[0];
                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }
                string alias = string.Concat(nombreArchivo, '-', IdDetRendicionSolicitud, Path.GetExtension(file.FileName));
                string rutaCompleta = Path.Combine(absolutePath, alias);

                if (System.IO.File.Exists(rutaCompleta))
                {
                    return Json(new { code = 3, message = "El archivo ya existe por favor ingrese uno nuevo", partial = string.Empty });
                }
                else
                {
                    file.SaveAs(rutaCompleta);
                    var adjunto = new BE_GASTO.Adjunto() { Alias = alias, NombreAdjunto = file.FileName, PathAdjunto = rutaCompleta };
                    var adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };
                    Session["ListaAdjuntoDG"] = adjuntoList;
                    var modelo = new DetalleSolicitudViewModel();
                    modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
                    modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
                    modelo.DetRendicionSolicitud.ListaAdjunto = adjuntoList;
                    var view = RenderRazor.RenderRazorViewToString(this.ControllerContext, "_AttachmentDetalleGasto", modelo);
                    return Json(new { code = 1, message = String.Empty, partial = view });
                }



            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = ex.Message, partial = string.Empty });
            }
        }

        public ActionResult ListaAdjunto_PV(int? IdSolicitud = null)
        {
            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.IdSolicitud = IdSolicitud == null ? 0 : (int)IdSolicitud;
            entSolicitud = new BL_GASTO.Solicitud.Solicitud().Recuperar(entSolicitud);
            var modelo = new SolicitudViewModel();
            modelo.ListaAdjunto = new List<BE_GASTO.Adjunto>();
            if (entSolicitud != null)
            {
                if (entSolicitud.NombreFile != null || entSolicitud.NombreFile != "")
                {

                    BE_GASTO.Adjunto adjunto = new BE_GASTO.Adjunto() { Alias = entSolicitud.NombreFile, NombreAdjunto = entSolicitud.NombreFile, PathAdjunto = entSolicitud.RutaCompletaFile };
                    List<BE_GASTO.Adjunto> adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };

                    Session["ListaAdjunto"] = adjuntoList;

                    modelo.ListaAdjunto.Add(adjunto);
                }
            }
            return PartialView("_Attachment", modelo);
        }


        public ActionResult ListaAdjuntoDG_PV(int? IdDetRendicionSolicitud = null)
        {
            BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            entDetRendicionSolicitud.IdDetRendicionSolicitud = IdDetRendicionSolicitud == null ? 0 : (int)IdDetRendicionSolicitud;
            entDetRendicionSolicitud = new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().RecuperarDetalle(entDetRendicionSolicitud);
            var modelo = new DetalleSolicitudViewModel();
            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
            if (entDetRendicionSolicitud != null)
            {
                if (entDetRendicionSolicitud.NombreFile != null || entDetRendicionSolicitud.NombreFile != "")
                {
                    BE_GASTO.Adjunto adjunto = new BE_GASTO.Adjunto() { Alias = entDetRendicionSolicitud.NombreFile, NombreAdjunto = entDetRendicionSolicitud.NombreFile, PathAdjunto = entDetRendicionSolicitud.RutaCompletaFile };
                    List<BE_GASTO.Adjunto> adjuntoList = new List<BE_GASTO.Adjunto>() { adjunto };
                    Session["ListaAdjuntoDG"] = adjuntoList;
                    modelo.DetRendicionSolicitud.ListaAdjunto.Add(adjunto);
                }
            }
            return PartialView("_AttachmentDetalleGasto", modelo);
        }

        public ActionResult RemoveFile()
        {
            var adjuntoList = new List<BE_GASTO.Adjunto>();
            var modelo = new SolicitudViewModel() { };
            modelo.ListaAdjunto = adjuntoList;
            Session["ListaAdjunto"] = adjuntoList;
            return PartialView("_Attachment", modelo);
        }

        public ActionResult RemoveFileDG()
        {
            var adjuntoList = new List<BE_GASTO.Adjunto>();
            var modelo = new DetalleSolicitudViewModel() { };
            modelo.DetRendicionSolicitud = new BE_GASTO.DetRendicionSolicitud();
            modelo.DetRendicionSolicitud.ListaAdjunto = new List<BE_GASTO.Adjunto>();
            modelo.DetRendicionSolicitud.ListaAdjunto = adjuntoList;
            Session["ListaAdjuntoDG"] = adjuntoList;
            return PartialView("_AttachmentDetalleGasto", modelo);
        }

        public PartialViewResult ConfirmacionEliminarDetalleGasto(int id)
        {
            ViewBag.hdfIdDetRendicionSolicitudEliminar = id;
            return PartialView("_ConfirmacionDeleteDetalleGasto");
        }
        public PartialViewResult ConfirmarAnularSolicitud(int id, string cod)
        {
            ViewBag.hdfIdSolicitudEliminar = id;
            ViewBag.hdfCodigoEliminar = cod;
            return PartialView("_ConfirmacionDelete");
        }

        [HttpGet]
        public ActionResult DescargarExcel(int IdSolicitud)
        {
            var arrayFileDetalleRendicion = GetReportExcel(IdSolicitud);
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            string rutaDocumento = Path.Combine(rutaDestinoPDF, "DetalleRendicionGastoExcel.xls");
            GuardarArchivo(arrayFileDetalleRendicion, "DetalleRendicionGastoExcel.xls");
            return File(rutaDocumento, "application/xls", "DetalleRendicionGastoExcel.xls");
        }

        [HttpGet]
        public ActionResult DescargarPDF(bool? pdf, int IdSolicitud)
        {
            var arrayFileDetalleRendicion = GetReportPDF(IdSolicitud);
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            string rutaDocumento = Path.Combine(rutaDestinoPDF, "DetalleRendicionGastoPDF.pdf");
            GuardarArchivo(arrayFileDetalleRendicion, "DetalleRendicionGastoPDF.pdf");
            return File(rutaDocumento, "application/pdf", "DetalleRendicionGastoPDF.pdf");
        }
        private byte[] GetReportPDF(int IdSolicitud)
        {
            var modelo = new DetalleSolicitudViewModel();
            modelo = getDetalleSolicitud(IdSolicitud);
            List<BE_GASTO.DetRendicionSolicitud> objLista = (List<BE_GASTO.DetRendicionSolicitud>)new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().PaginadoAll(IdSolicitud);
            string strPath = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReporteDetalleRendicion.rdlc"));
            string[] arrDataSet = new string[] { "ReportDetalleRendicion" };
            object[] arrDataSource = new object[] { objLista };
            var parameters = new Dictionary<string, string>() {
                { "pUsuario", modelo.NomApe},
                { "pRUC", modelo.NumeroDocumento},
                { "pCECO", modelo.CECO=="" ? "------" : modelo.CECO},
                { "pCodigo", modelo.Codigo},
                { "pFecha", modelo.FechaRegistroStr},
                { "pGastoTotal", modelo.TotalRendidoStr},
                { "pTotalAsignado", modelo.ImporteSolicitudStr},
                { "pCodigoOS", "------"},
                { "pSociedad", modelo.NombreCO},
                { "pAprobador", modelo.Aprobador},
                { "pMotivo",  modelo.Motivo=="" ? "------" : modelo.Motivo},
                { "pFechaDel", modelo.FechaInicioStr},
                { "pFechaAl", modelo.FechaFinStr},
                { "pFechaContabilidad", modelo.FechaContabilidadStr},
                { "pSucursal", modelo.Sucursal},
                { "pTipo", modelo.Tipo}
            };
            byte[] bytFile = new Export().CreatePdf(strPath, arrDataSet, arrDataSource, parameters);
            return bytFile;
        }

        private byte[] GetReportExcel(int IdSolicitud)
        {
            //var modelo = new DetalleSolicitudViewModel();
            //modelo = getDetalleSolicitud(IdSolicitud);
            List<BE_GASTO.DetRendicionSolicitud> objLista = (List<BE_GASTO.DetRendicionSolicitud>)new BL_GASTO.DetRendicionSolicitud.DetRendicionSolicitud().PaginadoAll(IdSolicitud);
            string strPath = Server.MapPath(string.Concat("~/Areas/GASTO/Report/RDLC/ReporteDetalleRendicionXLS.rdlc"));
            string[] arrDataSet = new string[] { "ReportDetalleRendicion" };
            object[] arrDataSource = new object[] { objLista };
            byte[] bytFile = new Export().CreateExcel(strPath, arrDataSet, arrDataSource, null);
            return bytFile;
        }
        private void GuardarArchivo(byte[] arrayByte, string nombreArchivo)
        {
            string rutaDestinoPDF = AppCode.Util.ServerPath("~/FilesDetallesGastos");
            if (!Directory.Exists(rutaDestinoPDF))
            {
                Directory.CreateDirectory(rutaDestinoPDF);
            }
            string rutaDocumento = Path.Combine(rutaDestinoPDF, nombreArchivo);
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

        [HttpPost]
        public async Task<PartialViewResult> ConfirmarAprobarSolicitudDelegada()
        {
            return PartialView("_Confirmacion");
        }
        [HttpPost]
        public async Task<PartialViewResult> SolicitudesDelegadas()
        {
            var listaEstados = new BL_GASTO.Estados.Estados().List().ToList();
            listaEstados = listaEstados.Where(m => m.IdTipo == 0).ToList();
            listaEstados.Insert(0, new BE_GASTO.Estados { Estado = "-SELECCIONE-" });
            ViewBag.listaEstados = listaEstados;
            var listaTipoSolicitud = new BL_GASTO.TipoSolicitud.TipoSolicitud().List().ToList();
            listaTipoSolicitud.Insert(0, new BE_GASTO.TipoSolicitud() { Tipo = "-SELECCIONE-" });
            ViewBag.listaTipoSolicitud = listaTipoSolicitud;
            return PartialView("_ListSolicitudesDelegadas");
        }
        [HttpGet]
        public JsonResult GetListSolicitudDelegadasAprobacion(Int64? idusuario = null, int? idsituacionservicio = null, string fechaDesde = "", string fechaHasta = "", string idcentrocosto = null, string idsociedad = null, int? idsucursal = null, string anioejercicio = null, int? idtipo = null, int page = 0, int rows = 0)
        {
            int intTotalRows = 0;
            int intIdDestino = 0;
            int intCantidadRegistros = 0;
            var intIdPerfil = UtilSession.Usuario.IdPerfil;
            var strUserNameRegistro = UtilSession.Usuario.UserName;

            BE_GASTO.Solicitud entSolicitud = new BE_GASTO.Solicitud();
            entSolicitud.UserName = strUserNameRegistro;
            entSolicitud.IdUsuario = idusuario == null ? 0 : idusuario;
            entSolicitud.IdSituacionServicio = idsituacionservicio == null ? 0 : (int)idsituacionservicio;
            entSolicitud.IdSucursal = idsucursal == null ? 0 : (int)idsucursal;
            entSolicitud.IdSociedad = idsociedad == null ? "" : idsociedad;
            entSolicitud.AnioEjercicio = anioejercicio == null ? "" : anioejercicio;
            entSolicitud.IdTipo = idtipo == null ? 0 : (int)idtipo;

            entSolicitud.FechaDesde = fechaDesde;
            entSolicitud.FechaHasta = fechaHasta;
            entSolicitud.CentroCostoAfectoCodigoSap = idcentrocosto == null ? "" : idcentrocosto;
            entSolicitud.PageSize = rows;
            entSolicitud.PageIndex = page;

            var lista = new BL_GASTO.Solicitud.Solicitud().PaginadoAprobacionDelegaciones(entSolicitud, out intTotalRows, out intCantidadRegistros, out intIdDestino);
            int iddestinobag = 1;
            if (lista.Count > 0)
            {
                iddestinobag = lista.FirstOrDefault().IdDestinoBag;
            }
            var jsonData = new
            {
                total = intTotalRows,
                page,
                records = intCantidadRegistros,
                rows = lista,
                iddestino = iddestinobag
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //public async Task<PartialViewResult> ConfirmacionRechazo(int IdSolicitud, string cod)
        //{
        //    ViewBag.IdSolicitudRechazo = IdSolicitud;
        //    ViewBag.CodigoRechazo = cod;
        //    return PartialView("_ConfirmacionRechazo");
        //}
    }
}