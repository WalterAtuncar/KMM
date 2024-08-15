using AutoMapper;
using Data;
using Komatsu_SistemaSeguros.STS;
using Service;
using Service.Contracts;
using Service.Services;
using System;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [STSAuthorization]
    public class PerfilController : Controller
    {
        private readonly IEstadoProveedorService _IEstadoProveedorService;

        public PerfilController(IEstadoProveedorService _IEstadoProveedorService)
        {
            this._IEstadoProveedorService = _IEstadoProveedorService;
        }

        MapperConfiguration config = new MapperConfiguration(m => { m.CreateMap<PerfilViewModel, Perfil>(); m.CreateMap<Perfil, PerfilViewModel>();
            m.CreateMap<PaginaViewModel, Pagina>(); m.CreateMap<Pagina, PaginaViewModel>();
        });

        PerfilService servicio = new PerfilService();
        PaginaService servicioPagina =new PaginaService();
        // GET: Perfil
        [STSUrl]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Registrar(int id = default(int))
        {
            var modelo = GetPerfilViewModel(id);
            return PartialView("_Formulario", modelo);
        }
        public PartialViewResult GetListPagina(int id = default(int))
        {
            var modelo = new PaginaViewModel();
            modelo.ListaPagina = servicioPagina.GetListNotPerfil(id);
            modelo.ListaPaginaPerfil = servicioPagina.GetListByPerfil(id);

            return PartialView("_ListPagina", modelo);
        }

        public JsonResult AgregarPaginaPerfil(int idPagina = default(int), int idPerfil = default(int))
        {
            var result = servicioPagina.AddPaginaPerfil(idPagina, idPerfil);
            return Json(result);
        }

        [HttpPost]
        public JsonResult RegistrarPost(PerfilViewModel modelo)
        {
            var result = new OperationResult();
            var perfil = modelo.Perfil;

            if (modelo.Perfil.ID == default(int))
            {
                var validar = ValidarPost(modelo);
                if (validar.Success)
                {
                    if ((Boolean)validar.ObjectResult == true)
                    {
                        result = validar;
                    }
                    else
                    {
                        result = servicio.Add(perfil);
                    }
                }
                else
                {
                    result = validar;
                }
            }
            else
            {
                result = servicio.Modificar(perfil);
            }
              
            return Json(result);
        }
        
        public OperationResult ValidarPost(PerfilViewModel modelo)
        {
            var perfil = modelo.Perfil;
            var result = servicio.IsValid(perfil);

            return result;
        }


        [HttpPost]
        public JsonResult DeletePaginaPerfil(int idPagina = default(int), int idPerfil = default(int))
        {
            var result = servicioPagina.DeletePafinaPerfil(idPagina, idPerfil);
            return Json(result);
        }

        private PerfilViewModel GetPerfilViewModel(int id)
        {
            var modelo = new PerfilViewModel();
            modelo.Perfil = new Data.Common.Perfil();
            modelo.ListaEstadoProveedor = _IEstadoProveedorService.GetList();
            if (id != default(int))
            {
                var perfil = servicio.GetById(id);
                modelo.Perfil = perfil;
            }

            return modelo;

        }

        public JsonResult GetListPerfil(int page, int rows)
        {
            int totalRows = 0;
            int CantidadRegistros = 0;

            var listQuery = servicio.ListarPaginado(rows, page, out totalRows, out CantidadRegistros);

            var jsonData = new
            {
                total = totalRows,
                page,
                records = CantidadRegistros,
                rows = listQuery
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }
    }
}