using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class MotivoCreacionSolicitudController : Controller
    {
        public ActionResult GetDropDown(string id, string nombre = "MotivoCreacionSolicitudID", string @default = null)
        {
            var motivoCreacionSolicitudService = new MotivoCreacionSolicitudService();
            var list = motivoCreacionSolicitudService.Get().Select(t => new SelectListItem()
            {
                Text = t.Nombre,
                Value = t.ID.ToString(),
                Selected = t.ID.ToString() == id
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