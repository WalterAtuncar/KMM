using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class TipoDestinoController : Controller
    {
        public ActionResult GetDropDown(string id, string nombre = "TipoDestino", string @default = null)
        {
            var lst = new List<Tuple<int, string>>()
            {
                new Tuple<int, string>(1, "Único"),
                new Tuple<int, string>(2, "Múltiple")
            };
            var list = lst.Select(t => new SelectListItem()
            {
                Text = t.Item2,
                Value = t.Item1.ToString(),
                Selected = t.Item1.ToString() == id.ToString()
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