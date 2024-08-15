using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Util;

namespace Komatsu_SistemaSeguros.STS
{
    public class STSAcceso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
      {
            int IdPerfil = UtilSession.Usuario.IdPerfil;
            if (IdPerfil == 0)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JavaScriptResult result = new JavaScriptResult()
                    {
                        Script = "window.location='" + "/Error/Unauthorized" + "';"
                    };
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Error",
                            action = "Unauthorized",
                            area = ""
                        }
                    ));
                }
            }
        }
    }
}