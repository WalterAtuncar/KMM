using System.Web.Mvc;

namespace WebApiAplications.Controllers
{
    public class HomeController : Controller
    {
       //[Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
