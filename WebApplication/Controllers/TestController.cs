using Service.Logic.Contract;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly IProveedorWebApiServiceLogic oIProveedorWebApiServiceLogic;
        public TestController(IProveedorWebApiServiceLogic oIProveedorWebApiServiceLogic)
        {
            this.oIProveedorWebApiServiceLogic = oIProveedorWebApiServiceLogic;
        }
        // GET: Test
        public ActionResult Index()
        {
            var result = oIProveedorWebApiServiceLogic.GetListTarifa(new Data.ServiceDTO.TarifaRequest());

            return View();
        }

        public ActionResult Map1()
        {
            return View();
        }

        public ActionResult WebAPI1()
        {
            return View();
        }
    }
}