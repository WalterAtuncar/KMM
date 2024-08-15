using System.Configuration;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Unauthorized()
        {
            ViewBag.UrlSeguridad = ConfigurationManager.AppSettings["SERVER_WEB_SEGURIDAD"].ToString();
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult InternalServer()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}