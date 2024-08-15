using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Logic;
using GASTO.Service.Logic.Contract;
using GASTO.Service.Services;
using Komatsu_SistemaSeguros.STS;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Areas.GASTO.Models;
using WebApplication.Util;
using BE_GASTO = GASTO.Domain;
using BL_GASTO = GASTO.BusinessLogic;

namespace WebApplication.Areas.GASTO.Controllers
{
    public class MaestrosController : BaseController
    {
        private readonly IMaestrosServiceLogic oIMaestrosServiceLogic;
        private readonly IUsuarioSucursalServiceLogic oIUsuarioSucursalServiceLogic;
        private readonly IUsuarioCajaService oIUsuarioCajaService;
        private readonly ISociedadServiceLogic oISociedadServiceLogic;
        private readonly IConfiguracionesServiceLogic oIConfiguracionesServiceLogic;
        private readonly ISociedadSucursalServiceLogic oISociedadSucursalServiceLogic;
        private readonly IUsuarioAprobadorServiceLogic oIUsuarioAprobadorServiceLogic;
        private readonly ICentroCostoServiceLogic oICentroCostoServiceLogic;
        public MaestrosController(ICentroCostoServiceLogic oICentroCostoServiceLogic, ISociedadServiceLogic oISociedadServiceLogic)
        {
            this.oIMaestrosServiceLogic = new MaestrosServiceLogic();
            this.oIUsuarioSucursalServiceLogic = new UsuarioSucursalServiceLogic();
            this.oIUsuarioCajaService = new UsuarioCajaService();
            this.oISociedadServiceLogic = oISociedadServiceLogic;
            this.oISociedadSucursalServiceLogic = new SociedadSucursalServiceLogic();
            this.oIConfiguracionesServiceLogic = new ConfiguracionesServiceLogic();
            this.oIUsuarioAprobadorServiceLogic = new UsuarioAprobadorServiceLogic();
            this.oICentroCostoServiceLogic = oICentroCostoServiceLogic;
        }
        [STSUrl]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetListMaestros(int page, int rows)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;
            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            var listQuery = oIMaestrosServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult GetDetalleMaestroLista_PV2(int page, int rows, int IdTabla)
        //{
        //    ViewModelMaestros modelo = new ViewModelMaestros();
        //    modelo.ListaUsuariosSucursal = new List<UsuarioSucursal>();
        //    string vistaPartial = "";
        //    int totalRows = 0;
        //    int CantidadRegistros = 0;
        //    var parameter = new Parameter();
        //    parameter.Collection.Add("Row", rows);
        //    parameter.Collection.Add("Page", page);
            
        //    if (IdTabla == 10)
        //    {
        //        vistaPartial = "_ListDetalleMaestro10";
        //        modelo.ListaUsuariosSucursal = oIUsuarioSucursalServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
        //    }
        //    if (IdTabla == 20)
        //    {
        //        vistaPartial = "_ListDetalleMaestro20";
        //        modelo.ListaConfiguraciones = oIConfiguracionesServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
        //    }
        //    if (IdTabla == 21)
        //    {
        //        vistaPartial = "_ListDetalleMaestro21";
        //        modelo.ListaSociedadSucursal = oISociedadSucursalServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
        //    }

        //    var view = RenderRazor.RenderRazorViewToString(this.ControllerContext, vistaPartial, modelo);
        //    var jsonData = new
        //    {
        //        total = totalRows,
        //        page,
        //        records = CantidadRegistros,
        //        partial = view
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);

        //}
        [HttpGet]
        public JsonResult GetDetalleMaestroLista_PV(int page, int rows, int IdTabla)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;
            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            var listQuery = new object();

            if (IdTabla == 10)
            {
                listQuery = oIUsuarioSucursalServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
            }
            if (IdTabla == 20)
            {
                listQuery = oIConfiguracionesServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
            }
            if (IdTabla == 21)
            {
                listQuery = oISociedadSucursalServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
            }
            if (IdTabla == 22)
            {
                listQuery = oIUsuarioAprobadorServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);
            }
            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public async Task<PartialViewResult> RegistrarUsuarioSucursal(int Id = default(int))
        {
            var modelo = await GetUsuarioSucursalViewModel(Id);
            return PartialView("_FormularioTabla10", modelo);
        }
        public async Task<PartialViewResult> RegistrarUsuarioAprobador(int Id = default(int))
        {
            var modelo = await GetUsuarioAprobadorViewModel(Id);
            return PartialView("_FormularioTabla22", modelo);
        }
        public async Task<PartialViewResult> RegistrarSociedadSucursal(int Id = default(int))
        {
            var modelo = await GetSociedadSucursalViewModel(Id);
            return PartialView("_FormularioTabla21", modelo);
        }
        public async Task<PartialViewResult> RegistrarConfiguraciones(int Id = default(int))
        {
            var modelo = await GetConfiguracionesViewModel(Id);
            return PartialView("_FormularioTabla20", modelo);
        }
        public ActionResult ListaSucursalesBySociedad_PV(string idsociedad)
        {
            var listaSucursales = oISociedadSucursalServiceLogic.ListarByIdSociedad(idsociedad);
            listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursales;
            return PartialView("_ListaSucursales");
        }
        public ActionResult ListaAprobadoresbySociedad_PV(string idsociedad)
        {
            //var listaUsuarios = oIUsuarioCajaService.Listar().OrderBy(x => x.Apellidos).Select(m => { m.ApellidosNombres = $"{m.NumeroDocumento} - {m.Apellidos}  {m.Nombres}"; return m; }).Where(x => x.CodigoSociedad == idsociedad).ToList();
            var listaUsuarios = oIUsuarioCajaService.Listar().OrderBy(x => x.Apellidos).Select(m => { m.ApellidosNombres = $"{m.NumeroDocumento} - {m.Apellidos}  {m.Nombres}"; return m; }).ToList();
            listaUsuarios.Insert(0, new Usuario() { ID = 0, ApellidosNombres = "-SELECCIONE-" });
            ViewBag.ListaUsuarios = listaUsuarios;
            return PartialView("_ListaAprobador");
        }
        public ActionResult ListaCentroCostobySociedad_PV(string idsociedad)
        {
            var modelo = new UsuarioAprobadorViewModel();
            modelo.listaCentroCosto = new List<Data.CentroCosto>();
            modelo.listaCentroCosto = GetCentrosCostos(idsociedad);
            return PartialView("_ListaCentroCosto", modelo);
        }
        private async Task<UsuarioSucursalViewModel> GetUsuarioSucursalViewModel(int Id)
        {
            var modelo = new UsuarioSucursalViewModel();
            BE_GASTO.UsuarioSucursal entUsuarioSucursal = new BE_GASTO.UsuarioSucursal();
          

            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "0", NombreCO = "-SELECCIONE-" });
            ViewBag.ListaSociedades = listaSociedad;

            var listaSucursales = new List<Sucursal>();
            listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursales;

            var listaUsuariosCaja = oIUsuarioCajaService.Listar();
            listaUsuariosCaja.Insert(0, new Usuario() { ApellidosNombres = "-SELECCIONE-" });
            ViewBag.ListaUsuariosCaja = listaUsuariosCaja;
            if (Id != default(int))
            {
                entUsuarioSucursal = await oIUsuarioSucursalServiceLogic.GetById(Id);

                var listaSucursales0 = oISociedadSucursalServiceLogic.ListarByIdSociedad(entUsuarioSucursal.CodigoSociedadFI);
                listaSucursales0.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
                ViewBag.ListaSucursales = listaSucursales0;


                modelo.ID = Id;
                modelo.IdSucursal = entUsuarioSucursal.IdSucursal;
                modelo.IdUsuario = entUsuarioSucursal.IdUsuario;
                modelo.CodigoSociedadFI = entUsuarioSucursal.CodigoSociedadFI;
                modelo.Activo = entUsuarioSucursal.Activo;
            }
            else
            {
                modelo.ID = 0;
                modelo.IdUsuario = 0;
                modelo.CodigoSociedadFI = "0";
                modelo.IdSucursal = 0;
            }
            return modelo;
        }
        private async Task<SociedadSucursalViewModel> GetSociedadSucursalViewModel(int Id)
        {
            var modelo = new SociedadSucursalViewModel();
            BE_GASTO.SociedadSucursal entSociedadSucursal = new BE_GASTO.SociedadSucursal();
            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "0", NombreCO = "-SELECCIONE-" });
            ViewBag.ListaSociedades = listaSociedad;
            var listaSucursales = new BL_GASTO.Sucursal.Sucursal().List().ToList();
            listaSucursales.Insert(0, new BE_GASTO.Sucursal() { Nombre = "-SELECCIONE-" });
            ViewBag.ListaSucursales = listaSucursales;
            if (Id != default(int))
            {
                entSociedadSucursal = await oISociedadSucursalServiceLogic.GetById(Id);
                modelo.ID = Id;
                modelo.IdSucursal = entSociedadSucursal.IdSucursal;
                modelo.CodigoSociedadFI = entSociedadSucursal.CodigoSociedadFI;
                modelo.Activo = entSociedadSucursal.Activo;
            }
            else
            {
                modelo.ID = 0;
                modelo.CodigoSociedadFI = "0";
                modelo.IdSucursal = 0;
            }
            return modelo;
        }
        public List<Data.CentroCosto> GetCentrosCostos(string codigoSociedad)
        {
            var listaCentrosCostos = oICentroCostoServiceLogic.Get().Where(m => m.Soc == codigoSociedad).ToList();
            listaCentrosCostos = listaCentrosCostos.Where(m => m.Soc == codigoSociedad).ToList().Select(t => new Data.CentroCosto()
            {
                Descripcion = t.Codigo + " - " + t.Descripcion.ToUpper(),
                Codigo = t.Codigo,
            }).ToList();
            return listaCentrosCostos;
        }
        private async Task<UsuarioAprobadorViewModel> GetUsuarioAprobadorViewModel(int Id)
        {
            var modelo = new UsuarioAprobadorViewModel();
            BE_GASTO.UsuarioAprobador entUsuarioAprobador = new BE_GASTO.UsuarioAprobador();
            modelo.listaCentroCosto = new List<Data.CentroCosto>();
            var listaSociedad = oISociedadServiceLogic.Get().ToList().ToList();
            listaSociedad.Insert(0, new Data.Common.Sociedad() { CodigoSociedadFI = "0", NombreCO = "-SELECCIONE-" });
            ViewBag.ListaSociedades = listaSociedad;
            List<Usuario> listaUsuarios = null;
            List<Data.CentroCosto> listaCentroCosto = null;
            if (Id != default(int))
            {
                entUsuarioAprobador = await oIUsuarioAprobadorServiceLogic.GetById(Id);
                //listaUsuarios = oIUsuarioCajaService.Listar().OrderBy(x => x.Apellidos).Select(m => { m.ApellidosNombres = $"{m.NumeroDocumento} - {m.Apellidos}  {m.Nombres}"; return m; }).Where(x => x.CodigoSociedad == entUsuarioAprobador.CodigoSociedadFI).ToList();
                listaUsuarios = oIUsuarioCajaService.Listar().OrderBy(x => x.Apellidos).Select(m => { m.ApellidosNombres = $"{m.NumeroDocumento} - {m.Apellidos}  {m.Nombres}"; return m; }).ToList();
                listaUsuarios.Insert(0, new Usuario() { ID = 0, ApellidosNombres = "-SELECCIONE-" });
                ViewBag.ListaUsuarios = listaUsuarios;
                modelo.ID = Id;
                modelo.IdUsuario = entUsuarioAprobador.IdUsuario;
                modelo.CodigoSociedadFI = entUsuarioAprobador.CodigoSociedadFI;
                modelo.Activo = entUsuarioAprobador.Activo;
                listaCentroCosto = GetCentrosCostos(entUsuarioAprobador.CodigoSociedadFI);
                modelo.listaCentroCosto = listaCentroCosto;
                modelo.listaCentroCostoByUsuario = oIUsuarioAprobadorServiceLogic.GetListarUsuarioAprobadorCentroCostoById(Id).Select(m=>m.CodigoCentroCosto).ToList();
                ViewBag.listaCeco = String.Join(",", modelo.listaCentroCostoByUsuario.ToArray());
            }
            else
            {
                listaUsuarios = new List<Usuario>();
                listaUsuarios.Insert(0, new Usuario() { ApellidosNombres = "-SELECCIONE-" });
                ViewBag.ListaUsuarios = listaUsuarios;
                listaCentroCosto = new List<Data.CentroCosto>();
                modelo.listaCentroCosto = listaCentroCosto;
                modelo.listaCentroCostoByUsuario = new List<string>();
                modelo.ID = 0;
                modelo.IdUsuario = 0;
                modelo.CodigoSociedadFI = "0";
            }
            return modelo;
        }
        private async Task<ConfiguracionesViewModel> GetConfiguracionesViewModel(int Id)
        {
            var modelo = new ConfiguracionesViewModel();
            BE_GASTO.Configuraciones entConfiguraciones = new BE_GASTO.Configuraciones();
            if (Id != default(int))
            {
                entConfiguraciones = await oIConfiguracionesServiceLogic.GetById(Id);
                modelo.Id= Id;
                modelo.Clave = entConfiguraciones.Clave;
                modelo.Valor1 = entConfiguraciones.Valor1;
                modelo.Valor2 = entConfiguraciones.Valor2;
                modelo.Activo = entConfiguraciones.Activo;
            }
            else
            {
                modelo.Id = 0;
                modelo.Clave = "";
                modelo.Valor1 = 0;
                modelo.Valor2 = 0;
            }
            return modelo;
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarUsuarioSucursalPost(BE_GASTO.UsuarioSucursal element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioSucursalServiceLogic.Add(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Insertar UsuarioSucursal",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarUsuarioSucursalPost(BE_GASTO.UsuarioSucursal element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioSucursalServiceLogic.Edit(element);
            var jsonData = new
            {
                Success = result>0 ? true : false,
                Message = result > 0 ? "" : "Error al Actualizar UsuarioSucursal",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarUsuarioSucursalPost(int Id)
        {
            BE_GASTO.UsuarioSucursal element = new UsuarioSucursal();
            element.ID = Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioSucursalServiceLogic.Remove(element);
            var jsonData = new
            {
                Success = result ==true ? true : false,
                Message = result ==true ? "" : "Error al Eliminar UsuarioSucursal",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarUsuarioAprobadorPost(BE_GASTO.UsuarioAprobador element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioAprobadorServiceLogic.Add(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Insertar UsuarioAprobador",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarUsuarioAprobadorPost(BE_GASTO.UsuarioAprobador element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioAprobadorServiceLogic.Edit(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Actualizar UsuarioAprobador",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarUsuarioAprobadorPost(int Id)
        {
            BE_GASTO.UsuarioAprobador element = new UsuarioAprobador();
            element.ID = Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIUsuarioAprobadorServiceLogic.Remove(element);
            var jsonData = new
            {
                Success = result == true ? true : false,
                Message = result == true ? "" : "Error al Eliminar UsuarioAprobador",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarSociedadSucursalPost(BE_GASTO.SociedadSucursal element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oISociedadSucursalServiceLogic.Add(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Insertar SociedadSucursal",
            };

            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarSociedadSucursalPost(BE_GASTO.SociedadSucursal element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oISociedadSucursalServiceLogic.Edit(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Actualizar SociedadSucursal",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarSociedadSucursalPost(int Id)
        {
            BE_GASTO.SociedadSucursal element = new SociedadSucursal();
            element.ID = Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oISociedadSucursalServiceLogic.Remove(element);
            var jsonData = new
            {
                Success = result == true ? true : false,
                Message = result == true ? "" : "Error al eliminar SociedadSucursal",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> RegistrarConfiguracionesPost(BE_GASTO.Configuraciones element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIConfiguracionesServiceLogic.Add(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Insertar Configuraciones",
            };

            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> ActualizarConfiguracionesPost(BE_GASTO.Configuraciones element)
        {
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIConfiguracionesServiceLogic.Edit(element);
            var jsonData = new
            {
                Success = result > 0 ? true : false,
                Message = result > 0 ? "" : "Error al Actualizar Configuraciones",
            };
            return Json(jsonData);
        }
        [HttpPost]
        public async Task<JsonResult> EliminarConfiguracionesPost(int Id)
        {
            BE_GASTO.Configuraciones element = new Configuraciones();
            element.Id = Id;
            element.UsuarioRegistro = UtilSession.Usuario.NombreCompleto;
            element.UserNameRegistro = UtilSession.Usuario.UserName;
            var result = await oIConfiguracionesServiceLogic.Remove(element);
            var jsonData = new
            {
                Success = result == true ? true : false,
                Message = result == true ? "" : "Error al Eliminar Configuraciones",
            };
            return Json(jsonData);
        }

    }
}