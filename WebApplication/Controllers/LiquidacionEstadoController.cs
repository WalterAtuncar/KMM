using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class LiquidacionEstadoController : Controller
    {
        LiquidacionEstadoService LiquidacionEstadoService = new LiquidacionEstadoService();
        
        public ActionResult GetDropDown(string id, string nombre = "LiquidacionEstadoDropDown", string @default = null)
        {
            var list = LiquidacionEstadoService.Get().Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.ID.ToString(),
                Selected = p.ID.ToString() == id
            }).ToList();

            return View("_DropDown", Tuple.Create<IEnumerable<SelectListItem>, string>(list, nombre));
        }
    }
}