using Service.Services;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication.Util;

namespace Komatsu_SistemaSeguros.STS
{
    public class STSUrl: ActionFilterAttribute
    {
        public static UsersService oUsersService = new UsersService();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            int IdPerfil = UtilSession.Usuario.IdPerfil;
            var URL = "/" + filterContext.Controller.GetType().Name.Replace("Controller", "") + "/" + filterContext.ActionDescriptor.ActionName;

            var result = oUsersService.GetValidateUrl(IdPerfil, URL);
            result = 1;
            if (result == 0)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JavaScriptResult content = new JavaScriptResult()
                    {
                        Script = "window.location='" + "/Error/Unauthorized" + "';"
                    };
                    filterContext.Result = content;
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