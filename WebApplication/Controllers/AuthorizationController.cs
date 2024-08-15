using System.Configuration;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class AuthorizationController : Controller
    {
        
        public ActionResult Index()
        {
            return Redirect(ConfigurationManager.AppSettings["SERVER_WEB_SEGURIDAD"]);
        }
    }
}