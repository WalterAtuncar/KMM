using Komatsu_SistemaSeguros.STS;
using System.Web.Mvc;

namespace WebApplication.Areas.GASTO.Controllers
{
    [STSAuthorization(Order = 1), STSAcceso(Order = 2)]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}