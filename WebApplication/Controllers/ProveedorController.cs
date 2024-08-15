using Data.Util;
using Komatsu_SistemaSeguros.STS;
using Service;
using Service.Contracts;
using Service.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class ProveedorController :Controller
    {
        private readonly IProveedorServiceLogic _IProveedorServiceLogic;
        private readonly ILiquidacionServiceLogic _LiquidacionServiceLogic;
        private readonly IEstadoProveedorService _IEstadoProveedorService;
        private readonly IServicioProveedorTaxiService _IServicioProveedorTaxiService;

        public ProveedorController(IProveedorServiceLogic _IProveedorServiceLogic, ILiquidacionServiceLogic _LiquidacionServiceLogic, 
                                   IEstadoProveedorService _IEstadoProveedorService , IServicioProveedorTaxiService _IServicioProveedorTaxiService
                                  )
        {
            this._IProveedorServiceLogic = _IProveedorServiceLogic;
            this._LiquidacionServiceLogic = _LiquidacionServiceLogic;
            this._IEstadoProveedorService = _IEstadoProveedorService;
            this._IServicioProveedorTaxiService = _IServicioProveedorTaxiService;
        }


        // GET: Proveedor
        [STSUrl]
        public ActionResult Index()
        {
            
            return View();
        }

        public async Task<PartialViewResult> Registrar(int id = default(int))
        {
            var modelo = await GetProveedorViewModel(id);
            return PartialView("_Formulario", modelo);
        }

        public async Task<PartialViewResult> Credenciales(int id = default(int))
        {
            var modelo = new ProveedorTaxiModel();
            modelo.Credenciales = await _IProveedorServiceLogic.GetCredencialesById(id);
            if(modelo.Credenciales == null)
            {
                modelo.Credenciales = new Data.CredencialesProveedorTaxi();
            }
            modelo.Credenciales.IdProveedor = id;
            return PartialView("_Credenciales", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarPost(ProveedorTaxiModel modelo)
        {
            var result = new OperationResult();

            var validacion = await _IProveedorServiceLogic.ValidarProveedor(modelo.Proveedor.RUC, modelo.Proveedor.ID);

            if (!validacion)
            {
                result = new OperationResult() { Success = false, Message = "Ya existe un proveedor con el mismo RUC" };
            }
            else
            {
                if (modelo.Proveedor.ID == default(int))
                {
                    result = await _IProveedorServiceLogic.Add(modelo.Proveedor);
                }
                else
                {
                    result = await _IProveedorServiceLogic.Modificar(modelo.Proveedor);
                }
            }

            return Json(result);
        }

        private async Task<ProveedorTaxiModel> GetProveedorViewModel(int id)
        {
            var modelo = new ProveedorTaxiModel();
            modelo.Proveedor = new Data.Common.ProveedorTaxi();
            modelo.ListaEstadoProveedor =  _IEstadoProveedorService.GetList();
            var listaIndicadorIGV = await _LiquidacionServiceLogic.GetListIndicadorIGV();
            modelo.ListaIndicadorIGVFactura = listaIndicadorIGV;
            modelo.ListaIndicadorIGVProvision = listaIndicadorIGV;
            modelo.Proveedor.IdIndicadorIGVFactura = 1;
            modelo.Proveedor.IdIndicadorIGVFactura = 2;

            if (id != default(int))
            {
                modelo.Proveedor = await _IProveedorServiceLogic.GetProveedorById(id);
            }
            return modelo;

        }

        public async Task<JsonResult> RegistrarCredenciales(ProveedorTaxiModel modelo)
        {
            var result = new OperationResult();
            if (modelo.Credenciales.ID == default(int))
            {
                result = await _IProveedorServiceLogic.AddCredencial(modelo.Credenciales);
            }
            else
            {
                result = await _IProveedorServiceLogic.ModificarCredencial(modelo.Credenciales);
            }
            return Json(result);
        }

        public PartialViewResult GetListMethod(int id = default(int))
        {
            var modelo = new ProveedorTaxiModel();
            modelo.Proveedor = new Data.Common.ProveedorTaxi();
            modelo.Proveedor.ID = id;
            modelo.ListaMetodos = _IServicioProveedorTaxiService.GetListMethod(id);

            return PartialView("_ListMethod", modelo);
        }
        [HttpPost]
        public JsonResult RegistrarMethodPost(ProveedorTaxiModel modelo)
        {
            var result = _IProveedorServiceLogic.AddMethodService(modelo.ListaMetodos, modelo.Proveedor.ID);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetListProveedor(int page , int rows, string Ruc = "", string RazonSocial = "", string CodigoSap = "")
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var parameter = new Parameter();
            parameter.Collection.Add("Row", rows);
            parameter.Collection.Add("Page", page);
            parameter.Collection.Add("Ruc", Ruc);
            parameter.Collection.Add("RazonSocial", RazonSocial);
            parameter.Collection.Add("CodigoSap", CodigoSap);


            var listQuery = _IProveedorServiceLogic.ListarPaginado(parameter, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDropDown(string id, string nombre = "ProveedorDropDown", string @default = null)
        {
            var listaProveedor = _IProveedorServiceLogic.Listar();
            var list = listaProveedor.Select(p => new SelectListItem()
            {
                Text = p.RazonSocial,
                Value = p.ID.ToString(),
                Selected = p.ID.ToString() == id
            }).ToList();
            if (@default != null)
                list.Insert(0, new SelectListItem()
                {
                    Selected = id == "",
                    Value = "",
                    Text = @default
                });
            return View("_DropDown", Tuple.Create<IEnumerable<SelectListItem>, string>(list, nombre));
        }
    }
}