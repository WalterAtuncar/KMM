using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Service.Services;

namespace WebApplication.Controllers
{
    public class SociedadController : Controller
    {
        SociedadService SociedadService = new SociedadService();

        public ActionResult GetDropDown(string id, string nombre = "SociedadDropDown", string @default = null)
        {
            var list = SociedadService.Get().Select(p => new SelectListItem()
            {
                Text = p.NombreCO.ToUpper(),
                Value = p.CodigoSociedadFI.ToString(),
                Selected = p.CodigoSociedadFI.ToString() == id
            }).ToList();
            if (@default != null)
                list.Insert(0, new SelectListItem()
                {
                    Selected = id == "",
                    Value = "0",
                    Text = @default
                });
            return View("_DropDown", Tuple.Create<IEnumerable<SelectListItem>, string>(list, nombre));
        }
    }
}